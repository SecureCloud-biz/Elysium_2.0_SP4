using System;
using System.Diagnostics.Contracts;
using System.Windows;

namespace Elysium.Test
{
    public static class Extensions
    {
        [Pure]
        [JetBrains.Annotations.Pure]
        internal static T AsFrozen<T>(this T freezable)
            where T : Freezable
        {
            if (freezable == null)
            {
                throw new ArgumentNullException("freezable");
            }
            return !freezable.IsFrozen && freezable.CanFreeze ? (T)freezable.GetAsFrozen() : freezable;
        }
    }
}