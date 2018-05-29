﻿using System;
using System.Collections.Generic;
using NullGuard;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for <see cref="IDictionary{T, K}"/>.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Gets element by specified key or sets new value, if one doesn't exist.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="key">Key of element to get.</param>
        /// <param name="value">Function that creates new value.</param>
        /// <typeparam name="TKey">Type of keys in dictionary.</typeparam>
        /// <typeparam name="TValue">Type of values in dictionary.</typeparam>
        /// <returns>Element or newly created value with specified key.</returns>
        public static TValue GetOrNew<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, Func<TValue> value) =>
            self.TryGetValue(key, out var result) ? result : self[key] = value();
    
        /// <summary>
        ///     Gets element by specified key or default value, if one doesn't exist.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="key">Key of element to get.</param>
        /// <typeparam name="TKey">Type of keys in dictionary.</typeparam>
        /// <typeparam name="TValue">Type of values in dictionary.</typeparam>
        /// <returns>Element with specified key or default value for type <typeparamref name="TValue"/>.</returns>
        [return: AllowNull] public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key) =>
            self.TryGetValue(key, out var result) ? result : default;
    }
}