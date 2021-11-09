using _0_Framework.Application.ErrorMessages;
using System.Globalization;

namespace _0_Framework.Application.Exceptions;

public class NotFoundApiException : Exception
{
    public NotFoundApiException() : base() { }

    public NotFoundApiException(string message = ApplicationErrorMessage.RecordNotFoundMessage) : base(message) { }

    public NotFoundApiException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}