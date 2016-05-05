namespace ESurvey.UIModels
{
    public class Result
    {
        
        public bool HadError { get; set; }
        public string ErrorMessage { get; set; }

        public Result()
        {
                
        }

        public Result(string errorMessage)
        {
            HadError = true;
            ErrorMessage = errorMessage;
        }
    }
}