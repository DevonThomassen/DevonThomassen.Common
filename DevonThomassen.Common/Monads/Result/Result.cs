﻿using System.Diagnostics.CodeAnalysis;
using DevonThomassen.Common;

namespace DevonThomassen.Common.Monads.Result;

public readonly partial record struct Result<TValue>
{
    private readonly TValue? _value;
    private readonly List<Error>? _errors;

    private Result(TValue value)
    {
        ArgumentNullException.ThrowIfNull(nameof(value));
        _value = value;
    }

    private Result(Error error)
    {
        _errors = [error];
    }

    private Result(List<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        if (errors.Count == 0)
        {
            throw new ArgumentNullException();
        }

        _errors = errors;
    }

    [MemberNotNullWhen(true, nameof(_value))]
    [MemberNotNullWhen(false, nameof(_errors))]
    public bool IsSuccess => _errors is null;

    public bool IsError => _errors is not null;

    public TValue Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("Result is not successful.");

    public TValue? ValueOrDefault => IsSuccess
        ? _value
        : default;

    public IReadOnlyList<Error> Errors => !IsSuccess
        ? _errors.AsReadOnly()
        : throw new InvalidCastException(
            "The Errors property cannot be accessed when no errors have been recorded. Check IsError before accessing Errors.");

    public IReadOnlyList<Error> ErrorsOrEmptyList => !IsSuccess
        ? _errors.AsReadOnly()
        : new List<Error>().AsReadOnly();

    public Error FirstError => !IsSuccess
        ? Errors[0]
        : throw new InvalidOperationException();

    public static Result<TValue> Success(TValue result)
        => new(result);

    public static implicit operator Result<TValue>(TValue result)
        => new(result);

    public static Result<TValue> Error(Error error)
        => new(error);

    public static implicit operator Result<TValue>(Error error)
        => new(error);

    public static Result<TValue> Error(List<Error> errors)
        => new(errors);

    public static implicit operator Result<TValue>(List<Error> errors)
        => new(errors);
}