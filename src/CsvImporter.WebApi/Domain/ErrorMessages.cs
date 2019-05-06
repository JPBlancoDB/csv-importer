namespace CsvImporter.WebApi.Domain
{
    public class ErrorMessages
    {
        public const string CsvFileIsMandatory = "CSV file must be provided.";
        public const string ErrorCsvHeader = "The csv has invalid headers.";
        public const string OnlyOneFile = "Only one file per request is allowed.";
        public const string ContentTypeInvalid = "The content type of the file is invalid. Must be csv.";
        public const string ConfigurationNotFoundException = "Missing {0} configuration.";
    }
}