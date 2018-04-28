using System;
using System.Collections.Generic;

internal static class YourOwnLinq
{
    public static IEnumerable<TSource> MyWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> func)
    {
        var result = source.GetEnumerator();
        while (result.MoveNext())
        {
            var itme = result.Current;
            if (func(itme))
            {
                yield return itme;
            }
        }
    }

    public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, TResult> selector)
    {
        foreach (var urlItem in urls)
        {
            yield return selector(urlItem);
        }
    }

    public static IEnumerable<TSource> MySkip<TSource>(this IEnumerable<TSource> sources, int topN)
    {
        int count = 0;
        foreach (var source in sources)
        {
            if (count >= topN)
            {
                yield return source;
            }
            count++;
        }
    }

    public static IEnumerable<TSource> MySkipWhile<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> selector, int skipNum)
    {
        var enumerator = sources.GetEnumerator();
        var count = 0;
        while (enumerator.MoveNext())
        {
            if (count < skipNum && selector(enumerator.Current))
            {
                count++;
            }
            else
            {
                yield return enumerator.Current;
            }
        }
    }

    public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, int, TResult> selector)
    {
        var count = 0;
        foreach (var urlItem in urls)
        {
            yield return selector(urlItem, count);
            count++;
        }
    }

    public static IEnumerable<TSource> MyTake<TSource>(this IEnumerable<TSource> sources, int topN)
    {
        var enumerator = sources.GetEnumerator();
        var count = 0;
        while (enumerator.MoveNext())
        {
            if (count < topN)
            {
                yield return enumerator.Current;
            }
            else
            {
                yield break;
            }
            count++;
        }
    }

    public static IEnumerable<TSource> MyTakeWhile<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> selector, int takeNum)
    {
        var enumerator = sources.GetEnumerator();
        var count = 0;
        while (enumerator.MoveNext())
        {
            if (selector(enumerator.Current) && count < takeNum)
            {
                yield return enumerator.Current;
                count++;
            }
        }
    }
}