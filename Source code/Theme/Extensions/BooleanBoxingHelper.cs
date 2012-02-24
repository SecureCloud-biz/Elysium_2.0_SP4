using System.Diagnostics.Contracts;

namespace Elysium.Theme.Extensions
{
    internal static class BooleanBoxingHelper
    {
        internal static readonly object FalseBox = false;
        internal static readonly object TrueBox = true;

        internal static object Box(bool value)
        {
            Contract.Ensures(Contract.Result<object>() != null);
            return value ? TrueBox : FalseBox;
        }

        internal static bool Unbox(object value)
        {
            return BoxingHelper<bool>.Unbox(value);
        }
    }
} ;