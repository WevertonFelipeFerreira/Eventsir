namespace Eventsir.Services.Events.SharedKernel.Result
{
    public class Result<TValue>
    {
        public TValue? Value { get; private set; }
        public bool IsValid { get; private set; }
        public EResultType ResultType { get; private set; }
        public string? ErrorMessage { get; private set; }
        private Result(TValue? value, bool isValid, EResultType resultType, string? errorMessage = null)
        {
            Value = value;
            IsValid = isValid;
            ResultType = resultType;
            ErrorMessage = errorMessage;
        }

        public static Result<TValue> CreateSuccess(TValue value, EResultType resultType = EResultType.Success) => new Result<TValue>(value, true, resultType);

        public static Result<TValue> CreateError(string errorMessage, EResultType resultType = EResultType.BadRequest) => new Result<TValue>(default, false, resultType, errorMessage);
    }
}
