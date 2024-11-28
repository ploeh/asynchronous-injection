using System;
using System.Threading.Tasks;

namespace Ploeh.Samples.BookingApi
{
    public static class TaskMaybe
    {
        public static async Task<TResult> Match<T, TResult>(
            this Task<Maybe<T>> source,
            TResult nothing,
            Func<T, TResult> just)
        {
            var m = await source;
            return m.Match(nothing: nothing, just: just);
        }

        public static async Task<Maybe<TResult>> Select<T, TResult>(
            this Task<Maybe<T>> source,
            Func<T, TResult> selector)
        {
            var m = await source;
            return m.Select(selector);
        }

        public static async Task<Maybe<TResult>> SelectMany<T, TResult>(
            this Task<Maybe<T>> source,
            Func<T, Task<Maybe<TResult>>> selector)
        {
            var m = await source;
            return await m.Match(
                nothing: Task.FromResult(new Maybe<TResult>()),
                just: x => selector(x));
        }

        public static Task<Maybe<TResult>> Traverse<T, TResult>(
            this Maybe<T> source,
            Func<T, Task<TResult>> selector)
        {
            return source.Match(
                nothing: Task.FromResult(new Maybe<TResult>()),
                just: async x => new Maybe<TResult>(await selector(x)));
        }
    }
}
