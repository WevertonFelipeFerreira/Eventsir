using Flunt.Notifications;

namespace SharedKernel.Result
{
    public class Result<TValue>
    {
        public IEnumerable<Notification> Notifications { get; private set; }
        public TValue? Value { get; private set; }
        public bool IsValid { get; private set; }
        public EResultType ResultType { get; private set; }
        public string? ErrorMessage { get; private set; }
        private Result(TValue? value, bool isValid, EResultType resultType, string? errorMessage = null, IEnumerable<Notification> notifications = null)
        {
            Value = value;
            IsValid = isValid;
            ResultType = resultType;
            ErrorMessage = errorMessage;
            Notifications ??= new List<Notification>();
        }

        public static Result<TValue> CreateSuccess(TValue value, EResultType resultType = EResultType.Success) => new Result<TValue>(value, true, resultType);

        public static Result<TValue> CreateError(string errorMessage, EResultType resultType = EResultType.BadRequest) => new Result<TValue>(default, false, resultType, errorMessage);
       // public static Result<TValue> CreateErrors(EResultType resultType = EResultType.BadRequest, IEnumerable<Notification> notifications) => new Result<TValue>(default, false, resultType, null, notifications);
    }
}