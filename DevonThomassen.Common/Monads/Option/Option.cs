using System.Diagnostics.CodeAnalysis;

namespace DevonThomassen.Common.Monads.Option;

public readonly partial record struct Option<TValue>
{
    private readonly TValue? _value;

    [MemberNotNullWhen(true, nameof(_value))]
    [MemberNotNullWhen(true, nameof(Value))]
    public bool HasValue { get; } = false;

    private Option(TValue? value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _value = value;
        HasValue = true;
    }

    public TValue Value => _value ?? throw new InvalidOperationException("Option does not have a value.");
    public TValue? ValueOrDefault => _value;

    public bool TryGetValue(
        [NotNullWhen(true), MaybeNullWhen(false)]
        out TValue value)
    {
        value = _value;
        return HasValue;
    }

    public static Option<TValue> Some(TValue value)
        => new(value);

    public static implicit operator Option<TValue>(TValue value)
        => new(value);

    public static Option<TValue> None
        => default;

    public override int GetHashCode()
    {
        return HasValue ? _value.GetHashCode() : 0;
    }
}