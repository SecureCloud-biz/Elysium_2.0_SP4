using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Windows;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [DebuggerNonUserCode]
    [System.Diagnostics.Contracts.Pure]
    internal static class AnimationUtil
    {
        [JetBrains.Annotations.Pure]
        internal static bool IsValid(UIElement obj, Animation value)
        {
            return value == Animation.Custom || Parameters.Animation.GetSupported(obj).HasFlag(value);
        }

        [Conditional("CONTRACTS_FULL")]
        [ContractAbbreviator]
        internal static void EnsureValid(UIElement obj)
        {
            Contract.Ensures(IsValid(obj, Contract.Result<Animation>()));
        }

        [JetBrains.Annotations.Pure]
        internal static object CoerceValid(DependencyObject obj, object baseValue)
        {
            ValidationHelper.NotNull(obj, "obj");
            ValidationHelper.OfType(obj, "obj", typeof(UIElement));
            var value = BoxingHelper<Animation>.Unbox(baseValue);
            var instance = (UIElement)obj;
            return IsValid(instance, value) ? value : Animation.Custom;
        }
    }
}