using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security;
#if NETFX45
using System.Threading.Tasks;
#endif
using System.Windows;
using System.Windows.Threading;

using Elysium.Extensions;
using Elysium.Markup;
using Elysium.Parameters;

using JetBrains.Annotations;

using Window = System.Windows.Window;

namespace Elysium
{
    [PublicAPI]
    public static class Manager
    {
        #region  URIs

        internal static readonly Uri GenericDictionaryUri = new Uri("pack://application:,,,/Elysium;component/Themes/Generic.xaml", UriKind.Absolute);
        internal static readonly Uri LightBrushesDictionaryUri = new Uri("pack://application:,,,/Elysium;component/Themes/LightBrushes.xaml", UriKind.Absolute);
        internal static readonly Uri LightGrayBrushesDictionaryUri = new Uri("pack://application:,,,/Elysium;component/Themes/LightGrayBrushes.xaml", UriKind.Absolute);
        internal static readonly Uri DarkGrayBrushesDictionaryUri = new Uri("pack://application:,,,/Elysium;component/Themes/DarkGrayBrushes.xaml", UriKind.Absolute);
        internal static readonly Uri DarkBrushesDictionaryUri = new Uri("pack://application:,,,/Elysium;component/Themes/DarkBrushes.xaml", UriKind.Absolute);
        internal static readonly Uri ResourcesUri = new Uri("pack://application:,,,/Elysium;component/Themes/Resources.xaml", UriKind.Absolute);

        #endregion

        #region Constractors

        [SecuritySafeCritical]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Couldn't initialize inline security critical static field.")]
        static Manager()
        {
            Cache = new WeakCache<FrameworkElement>();
        }

        #endregion

        #region Public members

        [SecurityCritical]
        private static readonly WeakCache<FrameworkElement> Cache;

        #region Application

        [SecuritySafeCritical]
        internal static void ApplyInternal(System.Windows.Application application, AppThemeDictionary themeResources)
        {
            ValidationHelper.NotNull(application, "application");
            ValidationHelper.NotNull(themeResources, "themeResources");

            ApplyCore(application.Resources, themeResources);

            Cache.Iterate(cachedControl => ApplyInternal(cachedControl, General.GetThemeResources(cachedControl)));
        }

        [SecuritySafeCritical]
        internal static void RemoveInternal(System.Windows.Application application)
        {
            ValidationHelper.NotNull(application, "application");
            ValidationHelper.NotOfType(application, "application", typeof(Application), Application.MustHaveTheme);

            RemoveCore(application.Resources);

            Cache.Iterate(cachedControl => ApplyInternal(cachedControl, General.GetThemeResources(cachedControl)));
        }

        #endregion

        #region Control

        [SecuritySafeCritical]
        internal static void ApplyInternal(FrameworkElement control, ThemeDictionary themeResources)
        {
            ValidationHelper.NotNull(control, "control");

            if (themeResources.Source == ThemeResources.Inherited)
            {
                themeResources.Control = new WeakReference(control);
            }
            control.Dispatcher.Invoke(() => ApplyCore(control.Resources, themeResources), DispatcherPriority.Render);

            Cache.Iterate(cachedControl => ApplyInternal(cachedControl, General.GetThemeResources(cachedControl)));
            Cache.Add(control);
        }

        [SecuritySafeCritical]
        internal static void RemoveInternal(FrameworkElement control)
        {
            ValidationHelper.NotNull(control, "control");
            
            control.Dispatcher.Invoke(() => RemoveCore(control.Resources), DispatcherPriority.Render);

            Cache.Remove(control);
            Cache.Iterate(cachedControl => ApplyInternal(cachedControl, General.GetThemeResources(cachedControl)));
        }

        #endregion

        #endregion

        #region Private members

        [SecurityCritical]
        internal static void ApplyCore(System.Windows.ResourceDictionary resources, ThemeDictionaryBase themeResources)
        {
            ValidationHelper.NotNull(resources, "resources");
            ValidationHelper.NotNull(themeResources, "themeResources");

            // Bug in WPF 4: http://connect.microsoft.com/VisualStudio/feedback/details/555322/global-wpf-styles-are-not-shown-when-using-2-levels-of-references
            if (resources.Keys.Count == 0)
            {
                resources.Add(typeof(Window), new Style(typeof(Window)));
            }

            var genericDictionary = new System.Windows.ResourceDictionary { Source = GenericDictionaryUri };
            genericDictionary.MergedDictionaries.Clear();
            genericDictionary.MergedDictionaries.Add(ThemeDictionaryConverter.Convert(themeResources));
            resources.SafeInject(genericDictionary);
        }

        [SecurityCritical]
        internal static void RemoveCore(System.Windows.ResourceDictionary resources)
        {
            ValidationHelper.NotNull(resources, "resources");

            var genericDictionaries = resources.MergedDictionaries.Where(d => d.Source == GenericDictionaryUri).ToList();
            if (genericDictionaries.Count != 1)
            {
                return;
            }
            resources.MergedDictionaries.Remove(genericDictionaries[0]);
        }

        #endregion
    }
}