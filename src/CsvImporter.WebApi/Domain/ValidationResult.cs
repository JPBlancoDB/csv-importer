namespace CsvImporter.WebApi.Domain
{
    public class ValidationResult
    {
        public bool Success
        {
            get { return  string.IsNullOrEmpty(ErrorMessage); }
        }

        public string ErrorMessage { get; set; }
    }
}