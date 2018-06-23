﻿using System;
using System.Collections.Generic;

internal static class YourOwnLinq
{
    public static bool ContainsBall<TSource>(this IEnumerable<TSource> colorBalls, TSource colorBall, IEqualityComparer<TSource> equalityComparer)
    {
        var enumerator = colorBalls.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (equalityComparer.Equals(enumerator.Current, colorBall))
            {
                return true;
            }
        }
        return false;
    }

    public static IEnumerable<int> GroupSum<TSource>(this IEnumerable<TSource> sources, int groupSize, Func<TSource, int> func)
    {
        var enumerator = sources.GetEnumerator();
        var count = 1;
        int sum = 0;
        while (enumerator.MoveNext())
        {
            sum += func(enumerator.Current);

            if (count % groupSize == 0)
            {
                yield return sum;
                sum = 0;
            }
            count++;
        }
        if (sum != 0)
        {
            yield return sum;
        }
    }

    public static bool IsAnyEngeerAbove45<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var item in source)
        {
            if (predicate(item))
            {
                return true;
            }
        }
        return false;
    }

    public static TSource IsMyFirst<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, bool> predicate)
    {
        var enumerator = enumerable.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                return enumerator.Current;
            }
        }
        return default(TSource);
    }

    public static bool MyAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (!predicate(enumerator.Current))
            {
                return false;
            }
        }
        return true;
    }

    public static bool MyAny<TSource>(this IEnumerable<TSource> sources)
    {
        return sources.GetEnumerator().MoveNext();
    }

    public static IEnumerable<TSource> MyDistinct<TSource>(this IEnumerable<TSource> sources)
    {
        var enumerator = sources.GetEnumerator();
        var hashSet = new HashSet<TSource>();
        while (enumerator.MoveNext())
        {
            var canAdd = hashSet.Add(enumerator.Current);
            if (canAdd)
            {
                yield return enumerator.Current;
            }
        }
    }

    public static TSource MyLast<TSource>(this IEnumerable<TSource> colorBalls, Func<TSource, bool> predicate)
    {
        var enumerator = colorBalls.GetEnumerator();
        var ball = default(TSource);
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                ball = enumerator.Current;
            }
        }
        return ball;
    }

    public static IEnumerable<TSource> MyRealSkipWhile<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> selector)
    {
        var enumerator = sources.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (!selector(enumerator.Current))
            {
                yield return enumerator.Current;
            }
        }
    }

    public static IEnumerable<TSource> MyRealTakeWhile<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> predicate)
    {
        var enumerator = sources.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                yield return enumerator.Current;
            }
            else
            {
                yield break;
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

    public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, int, TResult> selector)
    {
        var count = 0;
        foreach (var urlItem in urls)
        {
            yield return selector(urlItem, count);
            count++;
        }
    }

    public static TSource MySingle<TSource>(this IEnumerable<TSource> colorBalls, Func<TSource, bool> predicate)
    {
        var enumerator = colorBalls.GetEnumerator();
        var ball = default(TSource);
        var isMatch = false;

        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                if (isMatch)
                {
                    throw new InvalidOperationException();
                }
                isMatch = true;
                ball = enumerator.Current;
            }
        }
        return isMatch ? ball : throw new InvalidOperationException();
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

    public static int MySum<TSource>(this IEnumerable<TSource> sources, Func<TSource, int> func)
    {
        var enumerator = sources.GetEnumerator();
        int sum = 0;
        while (enumerator.MoveNext())
        {
            sum += func(enumerator.Current);
        }
        return sum;
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
}