﻿namespace Core.Common;

public class Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }
    protected Result(bool isSuccess, Error error)
    {
        if (IsSuccess && error == Error.None || !IsSuccess && error != Error.None)
            throw new InvalidOperationException();
        IsSuccess = isSuccess;
        Error = error;
    }
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}

public class Result<T> : Result
{
    public T? Value { get; }
    private Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        Value = value;
    }
    public static Result<T> Success(T value) => new(value, true, Error.None);
    public static new Result<T> Failure(Error error) => new(default, false, error);
}
