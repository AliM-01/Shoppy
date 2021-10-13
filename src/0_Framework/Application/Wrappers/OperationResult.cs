using _0_Framework.Application.ErrorMessages;

namespace _0_Framework.Application.Wrappers
{
    public class OperationResult
    {
        public bool IsSucceed { get; set; }

        public string Message { get; set; }

        public OperationResult()
        {
            IsSucceed = false;
            Message = "عملیات با خطا مواجه شد";
        }

        public OperationResult Succedded(string message = ApplicationErrorMessage.OperationSucceddedMessage)
        {
            IsSucceed = true;
            Message = message;
            return this;
        }

        public OperationResult Failed(string message = ApplicationErrorMessage.OperationFailedMessage)
        {
            IsSucceed = false;
            Message = message;
            return this;
        }
    }

    public class OperationResult<T>
    {
        public bool IsSucceed { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public OperationResult()
        {
            IsSucceed = false;
            Message = "عملیات با خطا مواجه شد";
        }

        public OperationResult<T> Succedded(T data, string message = ApplicationErrorMessage.OperationSucceddedMessage)
        {
            IsSucceed = true;
            Message = message;
            Data = data;
            return this;
        }

        public OperationResult<T> Failed(string message = ApplicationErrorMessage.OperationFailedMessage)
        {
            IsSucceed = false;
            Message = message;
            return this;
        }
    }
}