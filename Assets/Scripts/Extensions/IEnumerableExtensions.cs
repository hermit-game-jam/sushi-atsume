using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensions
{
    static readonly Random random = new Random();

    public static T Random<T>(this IEnumerable<T> source)
    {
        var index = random.Next(0, source.Count());
        return source.ElementAtOrDefault(index);
    }
}
