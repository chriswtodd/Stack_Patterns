using System;
using System.Runtime.CompilerServices;

namespace ConditionalWeakTableExtensions 
{
    public static class ConditionalWeakTableExtensions 
    {
        public static TValue GetValueOrCompute<TKey, TValue>(this ConditionalWeakTable<TKey, TValue> table, TKey key, Func<TKey, TValue> value)         
            where TKey : class
            where TValue : class
            => table.TryGetValue(key, out var ret) ? ret : value(key);
    }
}