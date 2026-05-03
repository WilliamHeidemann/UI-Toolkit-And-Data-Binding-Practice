using System;

namespace Core
{
    public readonly struct Option<T>
    {
        public static Option<T> None => default;
        public static Option<T> Some(T value) => new(value);

        private readonly bool _isSome;
        private readonly T _value;

        private Option(T value)
        {
            _value = value;
            _isSome = _value is not null;
        }

        public bool IsSome(out T value)
        {
            value = _value;
            return _isSome;
        }

        public TResult Match<TResult>(Func<T, TResult> some, TResult none)
        {
            return IsSome(out T value) ? some(value) : none;
        }

        public T MatchDefault()
        {
            return IsSome(out T value) ? value : default;
        }
    }
}