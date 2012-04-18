using System.Diagnostics.Contracts;

namespace Elysium.Extensions
{
    internal static class BoxingHelper<T>
        where T : struct
    {
        [Pure]
        [JetBrains.Annotations.Pure]
        internal static T Unbox(object value)
        {
            Contract.Assume(value is T);
            return (T)value;
        }
    }
} ;