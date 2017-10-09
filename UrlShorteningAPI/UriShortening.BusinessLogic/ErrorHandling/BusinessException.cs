namespace UriShortening.BusinessLogic.ErrorHandling
{
    using System;
    using Enums;

    public class BusinessException : Exception
    {
        public BusinessException(ErrorCode code, string format, params object[] args)
            : base(string.Format(format, args))
        {
            Code = code;
        }

        public BusinessException(Exception innerException, ErrorCode code, string format, params object[] args)
            : base(string.Format(format, args), innerException)
        {
            Code = code;
        }

        public ErrorCode Code { get; }
    }
}
