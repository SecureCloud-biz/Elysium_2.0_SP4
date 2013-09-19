using System.Diagnostics;
using System.Windows;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [DebuggerNonUserCode]
    [System.Diagnostics.Contracts.Pure]
    internal static class CornerRadiusUtil
    {
        [Pure]
        internal static bool IsUniform(CornerRadius cornerRadius)
        {
            if (!DoubleUtil.AreClose(cornerRadius.BottomLeft, cornerRadius.BottomRight))
            {
                return false;
            }

            if (!DoubleUtil.AreClose(cornerRadius.TopLeft, cornerRadius.TopRight))
            {
                return false;
            }

            if (!DoubleUtil.AreClose(cornerRadius.BottomLeft, cornerRadius.TopRight))
            {
                return false;
            }

            return true;
        }
    }
}