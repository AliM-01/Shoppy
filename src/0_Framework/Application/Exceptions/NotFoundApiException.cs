﻿using _0_Framework.Application.ErrorMessages;
using System.Globalization;

namespace _0_Framework.Application.Exceptions;

public class NotFoundApiException : Exception
{
    public NotFoundApiException(string message = ApplicationErrorMessage.RecordNotFound) : base(message) { }

    public NotFoundApiException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }

    public static void ThrowIfNull(object? data)
    {
        if (data is null)
            throw new NotFoundApiException();
    }
}