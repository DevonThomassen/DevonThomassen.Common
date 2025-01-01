namespace DevonThomassen.Common.Monads.Option;

public readonly partial record struct Option<TValue>
{
    public TResult Match<TResult>(Func<TValue, TResult> some, Func<TResult> none)
    {
        return HasValue ? some(_value) : none();
    }
}