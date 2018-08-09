using System;
using System.Collections.Generic;
using System.Text;

namespace Ploeh.Samples.BookingApi
{
    public sealed class Maybe<T>
    {
        private readonly bool hasItem;
        private readonly T item;

        public Maybe()
        {
            hasItem = false;
        }

        public Maybe(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            hasItem = true;
            this.item = item;
        }

        public TResult Match<TResult>(TResult nothing, Func<T, TResult> just)
        {
            if (nothing == null)
                throw new ArgumentNullException(nameof(nothing));
            if (just == null)
                throw new ArgumentNullException(nameof(just));

            return hasItem ? just(item) : nothing;
        }

        public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return Match(
                nothing: new Maybe<TResult>(),
                just: x => new Maybe<TResult>(selector(x)));
        }

        public Maybe<TResult> SelectMany<TResult>(
            Func<T, Maybe<TResult>> selector)
        {
            return Match(nothing: new Maybe<TResult>(), just: selector);
        }

        public bool IsNothing => Match(nothing: true, just: _ => false);

        public bool IsJust => !IsNothing;

        public override bool Equals(object obj)
        {
            if (!(obj is Maybe<T> other))
                return false;

            return Match(
                nothing: !other.hasItem,
                just: x => other.Match(
                    nothing: !hasItem,
                    just: y => Equals(x, y)));

        }

        public override int GetHashCode()
        {
            return Match(nothing: 0, just: x => x.GetHashCode());
        }
    }
}
