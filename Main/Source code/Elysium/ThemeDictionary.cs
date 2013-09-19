using System;

using JetBrains.Annotations;

namespace Elysium
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ThemeDictionary : ThemeDictionaryBase
    {
        public bool IsDesignTime { get; set; }

        protected internal WeakReference Control { get; set; }
    }
}