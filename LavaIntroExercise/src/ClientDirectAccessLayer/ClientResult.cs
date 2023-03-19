namespace LavaIntroExercise.ClientDirectAccessLayer
{
    public class ClientResult
    {
        // Determine and hold information about request statuses and failures.

        protected ClientResult(bool success, string error, object[] stringArgs)
        {
            Success = success;
            ErrorMessage = error;
            StringArgs = stringArgs;
        }

        public ClientResult(string error)
        {
            Success = string.IsNullOrWhiteSpace(error);
            ErrorMessage = error;
        }

        public bool Success { get; }
        public string ErrorMessage { get; }
        public object[] StringArgs { get; }

        public bool IsFailure => !Success;

        public static ClientResult Fail(string message, object[] stringArgs = null)
        {
            return new ClientResult(false, message, stringArgs);
        }

        public static ClientResult<T> Fail<T>(string message, object[] stringArgs = null)
        {
            return new ClientResult<T>(default, false, message, stringArgs);
        }

        public static ClientResult Ok()
        {
            return new ClientResult(true, string.Empty, null);
        }

        public static ClientResult<T> Ok<T>(T value)
        {
            return new ClientResult<T>(value, true, string.Empty, null);
        }
    }

    public class ClientResult<T> : ClientResult
    {
        protected internal ClientResult(T value, bool success, string error, object[] stringArgs)
            : base(success, error, stringArgs)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}
