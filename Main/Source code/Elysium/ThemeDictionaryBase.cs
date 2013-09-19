using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

using Elysium.Markup;

using JetBrains.Annotations;

namespace Elysium
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [Localizability(LocalizationCategory.Ignore)]
    [TypeConverter(typeof(ThemeDictionaryConverter))]
    public abstract class ThemeDictionaryBase : ObservableDictionary<ThemeResource, object>
    {
        protected ThemeDictionaryBase() : base(10)
        {
        }

        public virtual ThemeResources Source
        {
            get { return _source; }
            set
            {
                _source = value;
                OnPropertyChanged("Source");
            }
        }

        private ThemeResources _source = ThemeResources.Inherited;

        public bool IsAsynchronous { get; set; }
    }
}