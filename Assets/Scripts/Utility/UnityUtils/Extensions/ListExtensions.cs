using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace UnityUtils {
    public static class ListExtensions {
        static Random rng;
        
        /// <summary>
        /// Determines whether a collection is null or has no elements
        /// without having to enumerate the entire collection to get a count.
        ///
        /// Uses LINQ's Any() method to determine if the collection is empty,
        /// so there is some GC overhead.
        /// </summary>
        /// <param name="list">List to evaluate</param>
        public static bool IsNullOrEmpty<T>(this IList<T> list) {
            return list == null || !list.Any();
        }

        /// <summary>
        /// Creates a new list that is a copy of the original list.
        /// </summary>
        /// <param name="list">The original list to be copied.</param>
        /// <returns>A new list that is a copy of the original list.</returns>
        public static List<T> Clone<T>(this IList<T> list) {
            List<T> newList = new List<T>();
            foreach (T item in list) {
                newList.Add(item);
            }

            return newList;
        }

        /// <summary>
        /// Swaps two elements in the list at the specified indices.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="indexA">The index of the first element.</param>
        /// <param name="indexB">The index of the second element.</param>
        public static void Swap<T>(this IList<T> list, int indexA, int indexB) {
            (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        }

        /// <summary>
        /// Shuffles the elements in the list using the Durstenfeld implementation of the Fisher-Yates algorithm.
        /// This method modifies the input list in-place, ensuring each permutation is equally likely, and returns the list for method chaining.
        /// Reference: http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
        /// </summary>
        /// <param name="list">The list to be shuffled.</param>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <returns>The shuffled list.</returns>
        public static IList<T> Shuffle<T>(this IList<T> list) {
            if (rng == null) rng = new Random();
            int count = list.Count;
            while (count > 1) {
                --count;
                int index = rng.Next(count + 1);
                (list[index], list[count]) = (list[count], list[index]);
            }
            return list;
        }

        /// <summary>
        /// Filters a collection based on a predicate and returns a new list
        /// containing the elements that match the specified condition.
        /// </summary>
        /// <param name="source">The collection to filter.</param>
        /// <param name="predicate">The condition that each element is tested against.</param>
        /// <returns>A new list containing elements that satisfy the predicate.</returns>
        public static IList<T> Filter<T>(this IList<T> source, Predicate<T> predicate) {
            List<T> list = new List<T>();
            foreach (T item in source) {
                if (predicate(item)) {
                    list.Add(item);
                }
            }
            return list;
        }
        
        public static T RemoveAndGetItem<T>(this IList<T> list, int indexToRemove) {
            var item = list[indexToRemove];
            list.RemoveAt(indexToRemove);
            return item;
        }

        public static List<T> RemoveAndGetItems<T>(this List<T> list, int count) {
            var items = list.GetRange(0, count);
            list.RemoveRange(0, items.Count);
            return items;
        }

        public static bool IsNullOrEmpty<T>(this List<T> list) {
            return list == null || list.Count == 0;
        }

        public static bool IsNullOrEmpty<T>(this T[] array) {
            return array == null || array.Length == 0;
        }

        public static bool IsNullOrEmpty<T>(this IReadOnlyList<T> values) {
            return values == null || values.Count == 0;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> values) {
            return values == null || !values.Any();
        }

        public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> dict) {
            return dict == null || dict.Count == 0;
        }

        public static int IndexOf<T>(this T[] array, Predicate<T> predicate) {
            return Array.FindIndex(array, predicate);
        }

        public static void UpdateItem<T>(this List<T> list, T item, T newItem) {
            var itemIndex = list.IndexOf(item);
            if (itemIndex != -1) {
                list[itemIndex] = newItem;
            }
        }

        public static int SumArrayValues(this int[] array, int stopAtIndex) {
            stopAtIndex = Mathf.Min(array.Length, stopAtIndex);
            var sum = 0;
            for (int i = 0; i <= stopAtIndex; i++) {
                sum += array[i];
            }

            return sum;
        }

        public static T GetNextItem<T>(this IList<T> list, T prevItem) {
            var nextItemIndex = list.IndexOf(prevItem) + 1;
            return nextItemIndex < list.Count ? list[nextItemIndex] : list[0];
        }

        public static T GetPrevItem<T>(this IList<T> list, T nextItem) {
            var prevItemIndex = list.IndexOf(nextItem) - 1;
            return prevItemIndex >= 0 ? list[prevItemIndex] : list[^1];
        }

        public static void Shuffle<T>(this List<T> list) {
            var n = list.Count;
            var rng = new System.Random(Guid.NewGuid().GetHashCode());
            while (n > 1) {
                n--;
                var k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static bool Contains<T>(this T[] array, T item) {
            return Array.Exists(array, element => EqualityComparer<T>.Default.Equals(element, item));
        }

        public static List<T> GetRandomElements<T>(this List<T> list, int elementsCount) {
            return list.OrderBy(_ => Guid.NewGuid()).Take(elementsCount).ToList();
        }
    }
}
