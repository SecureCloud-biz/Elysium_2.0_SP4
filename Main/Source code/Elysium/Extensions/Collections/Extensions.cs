using System.Collections.Generic;
using System.Diagnostics;

using Elysium.Extensions;

using JetBrains.Annotations;

// ReSharper disable CheckNamespace
namespace System.Linq
{
    [DebuggerStepThrough]
    internal static class Extensions
    {
        internal static bool SafeGet<TKey, TValue>([NotNull] this IDictionary<TKey, WeakReference> dictionary, [NotNull] TKey key, out TValue value)
        {
            ValidationHelper.NotNull(dictionary, "dictionary");
            ValidationHelper.NotNull(key, "key");

            value = default(TValue);
            if (!dictionary.ContainsKey(key))
            {
                return false;
            }
            var temp = dictionary[key];
            if (!(temp.IsAlive && temp.Target is TValue))
            {
                return false;
            }
            value = (TValue)temp.Target;
            return true;
        }

        internal static void SafeSet<TKey, TValue>([NotNull] this IDictionary<TKey, WeakReference> dictionary, [NotNull] TKey key, TValue value)
        {
            ValidationHelper.NotNull(dictionary, "dictionary");
            ValidationHelper.NotNull(key, "key");

            if (dictionary.ContainsKey(key))
            {
                if (!dictionary[key].IsAlive)
                {
                    // Set value, if key exist
                    dictionary[key] = new WeakReference(value);
                }
            }
            else
            {
                // Add key and value, if key doesn't exist
                dictionary.Add(key, new WeakReference(value));
            }
        }

        internal static void Collect<TKey>([NotNull] this IDictionary<TKey, WeakReference> dictionary)
        {
            ValidationHelper.NotNull(dictionary, "dictionary");

            foreach (var pair in dictionary.Where(pair => !pair.Value.IsAlive))
            {
                dictionary.Remove(pair.Key);
            }
        }
    }
}
// ReSharper restore CheckNamespace