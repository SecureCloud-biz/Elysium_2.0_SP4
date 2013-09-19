using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
using System.Xaml;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium.Markup
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [MarkupExtensionReturnType(typeof(object))]
    [TypeConverter(typeof(ThemeResourceExtensionConverter))]
    public class ThemeResourceExtension : MarkupExtension
    {
        [ConstructorArgument("resourceKey")]
        public ThemeResource ResourceKey
        {
            get { return _resourceKey; }
            set
            {
                ValidationHelper.NotNull(value, "value");
                _resourceKey = value;
            }
        }

        private ThemeResource _resourceKey;

        [NotNull]
        private StaticResourceExtension StaticResource
        {
            get { return _staticResource ?? (_staticResource = new StaticResourceExtension(_resourceKey)); }
        }

        private StaticResourceExtension _staticResource;

        [NotNull]
        private DynamicResourceExtension DynamicResource
        {
            get { return _dynamicResource ?? (_dynamicResource = new DynamicResourceExtension(_resourceKey)); }
        }

        private DynamicResourceExtension _dynamicResource;

        public ThemeResourceExtension()
        {
        }

        public ThemeResourceExtension(ThemeResource resourceKey)
        {
            ValidationHelper.NotNull(resourceKey, "resourceKey");
            _resourceKey = resourceKey;
        }

        #region Overrides of MarkupExtension

        private const string ServiceException = "Markup extension '{0}' requires '{1}' be implemented in the IServiceProvider for ProvideValue.";

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            ValidationHelper.NotNull(serviceProvider, "serviceProvider");
            var rootObjectProvider = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
            if (rootObjectProvider == null)
            {
                throw new InvalidOperationException(string.Format(ServiceException, GetType().Name, "IRootObjectProvider"));
            }
            if (rootObjectProvider.RootObject is ResourceDictionary)
            {
                var ret1 = StaticResource.ProvideValue(serviceProvider);
                return ret1;
            }
            var themeInfo = ResourceInfoAttribute.FromAssembly(rootObjectProvider.RootObject.GetType().Assembly);
            if (themeInfo != null)
            {
                switch (themeInfo.Mode)
                {
                    case ResourceMode.Static:
                        var ret2 = StaticResource.ProvideValue(serviceProvider);
                        return ret2;
                    case ResourceMode.Dynamic:
                        var ret3 = DynamicResource.ProvideValue(serviceProvider);
                        return ret3;
                }
            }
            throw new InvalidOperationException("You must mark your assembly with Elysium.ResourceInfo attribute or use Elysium.ResourceDictionary");
        }

        #endregion
    }
}