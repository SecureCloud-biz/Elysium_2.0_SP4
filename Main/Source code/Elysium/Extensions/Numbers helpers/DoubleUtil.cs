using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Windows;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [DebuggerNonUserCode]
    [System.Diagnostics.Contracts.Pure]
    internal static class DoubleUtil
    {
        private const double Epsilon = 2.2204460492503131e-016;

        [JetBrains.Annotations.Pure]
        internal static bool AreClose(double first, double second)
        {
// ReSharper disable CompareOfFloatsByEqualityOperator
            if (first == second)
// ReSharper restore CompareOfFloatsByEqualityOperator
            {
                return true;
            }
            var eps = (Math.Abs(first) + Math.Abs(second) + 10.0) * Epsilon;
            var delta = first - second;
            return (-eps < delta) && (eps > delta);
        }

        [JetBrains.Annotations.Pure]
        internal static bool IsNonNegative(double value)
        {
            return !double.IsNaN(value) && !double.IsInfinity(value) && value > 0d;
        }

        [Conditional("CONTRACTS_FULL")]
        [ContractAbbreviator]
        internal static void EnsureNonNegative()
        {
            Contract.Ensures(IsNonNegative(Contract.Result<double>()));
        }

        [JetBrains.Annotations.Pure]
        internal static object CoerceNonNegative(DependencyObject obj, object baseValue)
        {
            ValidationHelper.NotNull(obj, "obj");
            var value = BoxingHelper<double>.Unbox(baseValue);
            return IsNonNegative(value) ? value : 0d;
        }
    }
}
