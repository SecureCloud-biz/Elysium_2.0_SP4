using System.Diagnostics.CodeAnalysis;
using System.Security;

using Elysium.Extensions;

using JetBrains.Annotations;

namespace Elysium
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ResourceDictionary : System.Windows.ResourceDictionary
    {
        [SecurityCritical]
        private static readonly WeakCache<ResourceDictionary> Cache = new WeakCache<ResourceDictionary>();

        [SecuritySafeCritical]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Couldn't initialize inline security critical static field.")]
        static ResourceDictionary()
        {
            Cache = new WeakCache<ResourceDictionary>();
        }

        [SecuritySafeCritical]
        public ResourceDictionary()
        {
            Cache.Add(this);
        }
    }
}