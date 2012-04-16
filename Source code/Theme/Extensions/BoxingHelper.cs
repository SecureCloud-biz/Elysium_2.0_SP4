using System.Diagnostics.Contracts;

namespace Elysium.Extensions
{
    internal static class BoxingHelper<T>
        where T : struct
    {
        [JetBrains.Annotations.Pure]
        [System.Diagnostics.Contracts.Pure]
        internal static T Unbox(object value)
        {
            Contract.Assume(value is T);
            return (T)value;
        }
    }
} ;