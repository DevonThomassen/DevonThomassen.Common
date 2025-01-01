using System.Diagnostics.CodeAnalysis;

namespace DevonThomassen.Common.Monads.Either;

/// <summary>
/// Represents a discriminated union type that 
/// </summary>
/// <typeparam name="TLeft">The type of the left value, often used for errors or failures/</typeparam>
/// <typeparam name="TRight">The type of the right value, typically used for valid results</typeparam>
public readonly partial record struct Either<TLeft, TRight>
{
    private readonly TLeft? _leftValue;
    private readonly TRight? _rightValue;

    /// <summary>
    /// Gets a value indicating whether this instance contains a "left" value.
    /// When <c>true</c>, the instance contains a left value and <see cref="RightValue"/> is not accessible.
    /// </summary>
    [MemberNotNullWhen(true, nameof(_leftValue))]
    [MemberNotNullWhen(false, nameof(_rightValue))]
    public bool IsLeft { get; } = false;

    /// <summary>
    /// Gets a value indicating whether this instance contains a "right" value.
    /// When <c>true</c>, the instance contains a right value and <see cref="LeftValue"/> is not accessible.
    /// </summary>
    [MemberNotNullWhen(true, nameof(_rightValue))]
    [MemberNotNullWhen(false, nameof(_leftValue))]
    public bool IsRight => !IsLeft;

    private Either(TLeft left)
    {
        _leftValue = left;
        IsLeft = true;
    }

    private Either(TRight right)
    {
        _rightValue = right;
    }

    /// <summary>
    /// Get the "left" value if the instance contains one.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the instance does not contain a left value.</exception>
    public TLeft LeftValue => IsLeft
        ? _leftValue
        : throw new InvalidOperationException("Either is not left");

    /// <summary>
    /// Gets the "left" value if the instance contains one, or the default value of <typeparamref name="TLeft"/> if it does not.
    /// </summary>
    public TLeft? LeftValueOrDefault => IsLeft
        ? _leftValue
        : default;

    /// <summary>
    /// Tries to retrieve the "left" value from the <see cref="Either{TLeft, TRight}"/>.
    /// </summary>
    /// <param name="value">The "left" value that will be set if the <see cref="Either{TLeft, TRight}"/> contains a <c>Left</c>; otherwise, it will be the default value.</param>
    /// <returns><c>true</c> if the <see cref="Either{TLeft, TRight}"/> contains a <c>Left</c>, otherwise <c>false</c>.</returns>
    /// <remarks>
    /// This method sets the <paramref name="value"/> parameter to the "left" value if the <see cref="Either{TLeft, TRight}"/> is a <c>Left</c>.
    /// If the <see cref="Either{TLeft, TRight}"/> is a <c>Right</c>, the <paramref name="value"/> parameter is set to the default value.
    /// </remarks>
    /// <returns></returns>
    public bool TryGetLeft([MaybeNullWhen(false)] out TLeft value)
    {
        if (IsLeft)
        {
            value = _leftValue;
            return true;
        }

        value = default;
        return false;
    }

    /// <summary>
    /// Gets the "right" value if the instance contains one.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the instance does not contain a right value.</exception>
    public TRight RightValue => IsRight
        ? _rightValue
        : throw new InvalidOperationException("Either is not right");

    /// <summary>
    /// Gets the "right" value if the instance contains one, or the default value of <typeparamref name="TRight"/> if it does not.
    /// </summary>
    public TRight? RightValueOrDefault => IsRight
        ? _rightValue
        : default;

    /// <summary>
    /// Tries to retrieve the "right" value from the <see cref="Either{TLeft, TRight}"/>.
    /// </summary>
    /// <param name="value">The "right" value that will be set if the <see cref="Either{TLeft, TRight}"/> contains a <c>Right</c>; otherwise, it will be the default value.</param>
    /// <returns><c>true</c> if the <see cref="Either{TLeft, TRight}"/> contains a <c>Right</c>, otherwise <c>false</c>.</returns>
    /// <remarks>
    /// This method sets the <paramref name="value"/> parameter to the "right" value if the <see cref="Either{TLeft, TRight}"/> is a <c>Right</c>.
    /// If the <see cref="Either{TLeft, TRight}"/> is a <c>Left</c>, the <paramref name="value"/> parameter is set to the default value.
    /// </remarks>
    /// <returns></returns>
    public bool TryGetRight([MaybeNullWhen(false)] out TRight value)
    {
        if (IsRight)
        {
            value = _rightValue;
            return true;
        }

        value = default;
        return false;
    }
    
    public static Either<TLeft, TRight> Left(TLeft left)
        => new(left);

    public static implicit operator Either<TLeft, TRight>(TLeft left)
        => new(left);

    public static Either<TLeft, TRight> Right(TRight right)
        => new(right);

    public static implicit operator Either<TLeft, TRight>(TRight right)
        => new(right);

    /// <summary>
    /// Returns the hash code for this instance, based on the encapsulated value.
    /// </summary>
    /// <returns>The hash code of the encapsulated value.</returns>
    public override int GetHashCode()
    {
        return IsLeft
            ? _leftValue.GetHashCode()
            : _rightValue.GetHashCode();
    }
}