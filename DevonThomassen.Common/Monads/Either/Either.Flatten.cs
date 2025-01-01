namespace DevonThomassen.Common.Monads.Either;

public readonly partial record struct Either<TLeft, TRight>
{
    public Either<TLeft, TRight> Flatten()
    {
        if (IsRight && _rightValue is Either<TLeft, TRight> nested)
        {
            return nested;
        }
        return this;
    }
}