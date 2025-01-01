namespace DevonThomassen.Common.Monads.Either;

public readonly partial record struct Either<TLeft, TRight>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rightFunc"></param>
    /// <returns></returns>
    public Either<TLeft, TRight> Bind(Func<TRight, Either<TLeft, TRight>> rightFunc)
    {
        ArgumentNullException.ThrowIfNull(rightFunc);
        
        return IsRight 
            ? rightFunc(_rightValue) 
            : new Either<TLeft, TRight>(_leftValue!);
    }
}