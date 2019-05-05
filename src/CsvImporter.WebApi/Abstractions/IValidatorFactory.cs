namespace CsvImporter.WebApi.Abstractions
{
    public interface IValidatorFactory
    {
        IValidator CreateValidator();
    }
}