using System;
using System.Diagnostics.Contracts;

using Elysium.Markup;

namespace Elysium
{
    public class AppThemeDictionary : ThemeDictionaryBase
    {
        #region Overrides of ThemeDictionaryBase

        public override ThemeResources Source
        {
            get { return base.Source; }
            set
            {
                Restrictions(value, "value");
                base.Source = value;
            }
        }

        #endregion

        [ContractArgumentValidator]
        internal static void Restrictions(ThemeResources argument, string parameterName)
        {
            if (argument == ThemeResources.Inherited)
            {
                throw new ArgumentException("AppThemeDictionary can't have Inherited source at application level", parameterName);
            }
            Contract.EndContractBlock();
        }
    }
}