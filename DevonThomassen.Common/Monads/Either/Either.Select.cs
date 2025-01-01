namespace DevonThomassen.Common.Monads.Either;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TLeft"></typeparam>
/// <typeparam name="TRight"></typeparam>
public readonly partial record struct Either<TLeft, TRight>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="selector"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Either<TLeft, TResult> Select<TResult>(Func<TRight, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);
        
        return IsRight
            ? selector(_rightValue)
            : _leftValue;
    }

    public Either<TLeft, TResult> SelectMany<TResult>(Func<TRight, Either<TLeft, TResult>> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return IsRight
            ? selector(_rightValue)
            : new Either<TLeft, TResult>(_leftValue);
    }
}