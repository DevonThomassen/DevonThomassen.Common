namespace DevonThomassen.Common.Monads.Either;

public readonly partial record struct Either<TLeft, TRight>
{
    public Either<TLeft, TRight> Filter(Func<TRight, bool> predicate, TLeft leftValue)
    {
        return IsRight && predicate(_rightValue)
            ? this
            : leftValue;
    }
}