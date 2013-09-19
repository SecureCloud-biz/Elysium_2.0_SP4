using System;
using System.Reflection;
using System.Runtime;

using JetBrains.Annotations;

namespace Elysium
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [AttributeUsageAttribute(AttributeTargets.Assembly)]
    public sealed class ResourceInfoAttribute : Attribute
    {
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public ResourceInfoAttribute(ResourceMode mode)
        {
            _mode = mode;
        }

        public ResourceMode Mode
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get { return _mode; }
        }

        private ResourceMode _mode;

        internal static ResourceInfoAttribute FromAssembly(Assembly assembly)
        {
            return GetCustomAttribute(assembly, typeof(ResourceInfoAttribute)) as ResourceInfoAttribute;
        }
    }
}