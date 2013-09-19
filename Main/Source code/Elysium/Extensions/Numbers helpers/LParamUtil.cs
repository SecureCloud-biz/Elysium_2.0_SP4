using System;
using System.Diagnostics;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [DebuggerNonUserCode]
    [System.Diagnostics.Contracts.Pure]
    public static class LParamUtil
    {
        /// <summary>
        /// Gets low word of LParam
        /// </summary>
        /// <param name="value">LParam value</param>
        /// <returns>Low word of LParam</returns>
        [Pure]
        public static int LowWord(this IntPtr value)
        {
            return (short)(value.ToInt64() & 0xFFFFL);
        }

        /// <summary>
        /// Gets hi word of LParam
        /// </summary>
        /// <param name="value">LParam value</param>
        /// <returns>Hi word of LParam</returns>
        [Pure]
        public static int HiWord(this IntPtr value)
        {
            return (short)((value.ToInt64() >> 16) & 0xFFFFL);
        }

        [Pure]
        public static IntPtr ConvertFrom(int lo, int hi)
        {
            return (IntPtr)(((short)hi << 16) | (lo & 0xFFFF));
        }
    }
}