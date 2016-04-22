namespace ESurvey.UIModels
{
    public class DataResult<TData> : Result where TData : class
    {
        public DataResult(string errorMessage)
        {
            HadError = true;
            ErrorMessage = errorMessage;
        }

        public DataResult(TData data)
        {
            HadError = false;
            Data = data;
        }
        public TData Data { get; set; }
    }
}