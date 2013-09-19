using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Application : System.Windows.Application
    {
        public static new Application Current
        {
            get
            {
                return (Application)System.Windows.Application.Current;
            }
        }

        public AppThemeDictionary ThemeResources
        {
            get { return _themeResources; }
            set
            {
                if (_themeResources != null)
                {
                    _themeResources.CollectionChanged -= OnThemeResourcesChanged;
                    _themeResources.PropertyChanged -= OnThemeSourceChanged;
                }
                _themeResources = value;
                if (_themeResources != null)
                {
                    if (_isStarted)
                    {
                        TryApply();
                    }
                    _themeResources.PropertyChanged += OnThemeSourceChanged;
                    _themeResources.CollectionChanged += OnThemeResourcesChanged;
                }
                else
                {
                    if (_isStarted)
                    {
                        TryRemove();
                    }
                }
            }
        }

        private AppThemeDictionary _themeResources;

        #region Overrides of Application

        private bool _isStarted;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _isStarted = true;
            if (ThemeResources != null)
            {
                TryApply();
            }
            else
            {
                TryRemove();
            }
        }

        #endregion

        private void OnThemeSourceChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Source" && _isStarted)
            {
                TryApply();
            }
        }

        private void OnThemeResourcesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_isStarted)
            {
                TryApply();
            }
        }

        private void TryApply()
        {
            if (!ThemeResources.IsAsynchronous)
            {
                Apply();
            }
            else
            {
#if NETFX4
                BeginApply();
#elif NETFX45
                ApplyAsync();
#endif
            }
        }

        protected virtual void Apply()
        {
            ThemeOperationsRestrictions();
            Dispatcher.Invoke(() => Manager.ApplyInternal(this, _themeResources), DispatcherPriority.Send);
        }

#if NETFX4
        protected virtual DispatcherOperation BeginApply()
        {
            ThemeOperationsRestrictions();
            return Dispatcher.BeginInvoke(() => Manager.ApplyInternal(this, _themeResources), DispatcherPriority.Send);
        }
#elif NETFX45
        protected virtual async Task ApplyAsync()
        {
            ThemeOperationsRestrictions();
            await Dispatcher.InvokeAsync(() => Manager.ApplyInternal(this, _themeResources), DispatcherPriority.Send);
        }
#endif

        internal const string UseThemeResources = "Use ThemeResources property instead.";

        public static void Apply(System.Windows.Application application, AppThemeDictionary themeResources)
        {
            ValidationHelper.NotNull(application, "application");
            ValidationHelper.NotOfType(application, "application", typeof(Application), UseThemeResources);
            ValidationHelper.NotNull(themeResources, "themeResources");

            application.Dispatcher.Invoke(() => Manager.ApplyInternal(application, themeResources), DispatcherPriority.Send);
        }

#if NETFX4
        public static DispatcherOperation BeginApply(System.Windows.Application application, AppThemeDictionary themeResources)
        {
            ValidationHelper.NotNull(application, "application");
            ValidationHelper.NotOfType(application, "application", typeof(Application), UseThemeResources);
            ValidationHelper.NotNull(themeResources, "themeResources");

            return application.Dispatcher.BeginInvoke(() => Manager.ApplyInternal(application, themeResources), DispatcherPriority.Send);
        }
#elif NETFX45
        public static async Task ApplyAsync(System.Windows.Application application, AppThemeDictionary themeResources)
        {
            ValidationHelper.NotNull(application, "application");
            ValidationHelper.NotOfType(application, "application", typeof(Application), UseThemeResources);
            ValidationHelper.NotNull(themeResources, "themeResources");

            await application.Dispatcher.InvokeAsync(() => Manager.ApplyInternal(application, themeResources), DispatcherPriority.Send);
        }
#endif

        private void TryRemove()
        {
            if (!ThemeResources.IsAsynchronous)
            {
                Remove();
            }
            else
            {
#if NETFX4
                BeginRemove();
#elif NETFX45
                RemoveAsync();
#endif
            }
        }

        protected virtual void Remove()
        {
            ThemeOperationsRestrictions();
            Dispatcher.Invoke(() => Manager.RemoveInternal(this), DispatcherPriority.Send);
        }

#if NETFX4
        protected virtual DispatcherOperation BeginRemove()
        {
            ThemeOperationsRestrictions();
            return Dispatcher.BeginInvoke(() => Manager.RemoveInternal(this), DispatcherPriority.Send);
        }
#elif NETFX45
        protected virtual async Task RemoveAsync()
        {
            ThemeOperationsRestrictions();
            await Dispatcher.InvokeAsync(() => Manager.RemoveInternal(this), DispatcherPriority.Send);
        }
#endif

        internal const string MustHaveTheme = "Elysium.Application must have theme.";

#if NETFX4
        [PublicAPI]
        public static DispatcherOperation BeginRemove(System.Windows.Application application)
        {
            ValidationHelper.NotNull(application, "application");
            ValidationHelper.NotOfType(application, "application", typeof(Application), MustHaveTheme);

            return application.Dispatcher.BeginInvoke(() => Manager.RemoveInternal(application), DispatcherPriority.Send);
        }
#elif NETFX45
        [PublicAPI]
        public static async Task RemoveAsync(System.Windows.Application application)
        {
            ValidationHelper.NotNull(application, "application");
            ValidationHelper.NotOfType(application, "application", typeof(Application), MustHaveTheme);

            await application.Dispatcher.InvokeAsync(() => Manager.RemoveInternal(application), DispatcherPriority.Send);
        }
#endif

        public static void Remove(System.Windows.Application application)
        {
            ValidationHelper.NotNull(application, "application");
            ValidationHelper.NotOfType(application, "application", typeof(Application), MustHaveTheme);

            application.Dispatcher.Invoke(() => Manager.RemoveInternal(application), DispatcherPriority.Send);
        }

        private void ThemeOperationsRestrictions()
        {
            if (!_isStarted)
            {
                throw new InvalidOperationException("Theme can be applied only after application startup.");
            }
        }
    }
}