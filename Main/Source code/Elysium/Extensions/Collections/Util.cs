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
    internal static class Util
    {
        [Conditional("CONTRACTS_FULL")]
        [ContractAbbreviator]
        internal static void EnsureNotNull<T>()
        {
// ReSharper disable CompareNonConstrainedGenericWithNull
            Contract.Ensures(Contract.Result<T>() != null);
// ReSharper restore CompareNonConstrainedGenericWithNull
        }

        [Conditional("CONTRACTS_FULL")]
        [ContractAbbreviator]
        internal static void EnsureOfType<T>(Type type)
        {
            Contract.Ensures(type.IsInstanceOfType(Contract.Result<T>()));
        }
    }
}