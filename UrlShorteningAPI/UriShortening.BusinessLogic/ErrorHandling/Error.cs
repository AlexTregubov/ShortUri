namespace UriShortening.BusinessLogic.ErrorHandling
{
    using Enums;

    public static class Error
    {
        public static void If(bool predicate, ErrorCode code, string format, params object[] args)
        {
            if (predicate)
            {
                throw new BusinessException(code, format, args);
            }
        }

        public static void IfNull(object value, ErrorCode code, string format, params object[] args)
        {
            If(value == null, code, format, args);
        }

        public static void Throw(ErrorCode code, string format, params object[] args)
        {
            throw new BusinessException(code, format, args);
        }

        public static void IfNotNull(object value, ErrorCode code, string format, params object[] args)
        {
            If(value != null, code, format, args);
        }
    }
}
