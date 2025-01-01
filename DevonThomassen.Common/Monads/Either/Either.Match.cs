namespace DevonThomassen.Common.Monads.Either;

public readonly partial record struct Either<TLeft, TRight>
{
    public TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc)
    {
        return IsLeft
            ? leftFunc(_leftValue!)
            : rightFunc(_rightValue!);
    }
}