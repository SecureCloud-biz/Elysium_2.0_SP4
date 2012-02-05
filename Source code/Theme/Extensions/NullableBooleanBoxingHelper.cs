namespace Elysium.Theme.Extensions
{
    public class NullableBooleanBoxingHelper
    {
        internal static object Box(bool? value)
        {
            return value.HasValue ? BooleanBoxingHelper.Box(value.Value) : null;
        }

        internal static bool? Unbox(object value)
        {
            return value == null ? new bool?() : BooleanBoxingHelper.Unbox(value);
        }
    }
} ;