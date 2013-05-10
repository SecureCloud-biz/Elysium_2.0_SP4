using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [DebuggerNonUserCode]
    [System.Diagnostics.Contracts.Pure]
    internal static class DesignUtil
    {
        [Pure]
        internal static bool IsInDesignMode()
        {
            return BooleanBoxingHelper.Unbox(DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement)).Metadata.DefaultValue);
        }
    }
}