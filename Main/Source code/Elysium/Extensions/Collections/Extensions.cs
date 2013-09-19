using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;

using Elysium.Extensions;

using JetBrains.Annotations;

// ReSharper disable CheckNamespace
namespace System.Linq
{
    [DebuggerStepThrough]
    internal static class Extensions
    {
        internal static bool SafeGet<TKey, TValue>([NotNull] this IDictionary dictionary, [NotNull] TKey key, out TValue value)
        {
            ValidationHelper.NotNull(dictionary, "dictionary");
            ValidationHelper.NotNull(key, "key");

            value = default(TValue);
            if (!dictionary.Contains(key))
            {
                return false;
            }
            var temp = dictionary[key];
            if (!(temp is TValue))
            {
                return false;
            }
            value = (TValue)temp;
            return true;
        }

        internal static void SafeSet<TKey, TValue>([NotNull] this IDictionary dictionary, [NotNull] TKey key, TValue value)
        {
            ValidationHelper.NotNull(dictionary, "dictionary");
            ValidationHelper.NotNull(key, "key");

            if (dictionary.Contains(key))
            {
                // Set value, if key exist
                dictionary[key] = value;
            }
            else
            {
                // Add key and value, if key doesn't exist
                dictionary.Add(key, value);
            }
        }

        internal static void SafeSet<TKey, TValue>([NotNull] this IDictionary destinationDictionary, [NotNull] IDictionary<TKey, TValue> sourceDictionary)
        {
            ValidationHelper.NotNull(destinationDictionary, "destinationDictionary");
            ValidationHelper.NotNull(sourceDictionary, "sourceDictionary");

            foreach (var key in sourceDictionary.Keys)
            {
                destinationDictionary.SafeSet(key, sourceDictionary[key]);
            }
        }

        internal static void SafeSet([NotNull] this IDictionary destinationDictionary, [NotNull] IDictionary sourceDictionary)
        {
            ValidationHelper.NotNull(destinationDictionary, "destinationDictionary");
            ValidationHelper.NotNull(sourceDictionary, "sourceDictionary");

            foreach (var key in sourceDictionary.Keys)
            {
                destinationDictionary.SafeSet(key, sourceDictionary[key]);
            }
        }

        internal static void SafeInject(this System.Windows.ResourceDictionary resources, System.Windows.ResourceDictionary dictionary)
        {
            var dictionaries = resources.MergedDictionaries.Where(d => d.Source == dictionary.Source).ToList();
            var lastIndex = dictionaries.Select(d => resources.MergedDictionaries.IndexOf(d)).Concat(new[] { 0 }).Max();

            // NOTE: lastIndex always greater than or equal to zero
            Contract.Assume(lastIndex >= 0);

            // Remove previous dictionaries
            foreach (var d in dictionaries)
            {
                resources.MergedDictionaries.Remove(d);
            }

            // Add new dictionary
            if (dictionary != null)
            {
                resources.MergedDictionaries.Insert(lastIndex, dictionary);
            }
        }

        internal static void SafeRemove<TKey>([NotNull] this IDictionary dictionary, [NotNull] TKey key)
        {
            ValidationHelper.NotNull(dictionary, "dictionary");
            ValidationHelper.NotNull(key, "key");

            if (dictionary.Contains(key))
            {
                dictionary.Remove(key);
            }
        }

        internal static void SafeRemove<TKey, TValue>([NotNull] this IDictionary destinationDictionary, [NotNull] IDictionary<TKey, TValue> sourceDictionary)
        {
            ValidationHelper.NotNull(destinationDictionary, "destinationDictionary");
            ValidationHelper.NotNull(sourceDictionary, "sourceDictionary");

            foreach (var key in sourceDictionary.Keys)
            {
                destinationDictionary.SafeRemove(key);
            }
        }
    }
}
// ReSharper restore CheckNamespace