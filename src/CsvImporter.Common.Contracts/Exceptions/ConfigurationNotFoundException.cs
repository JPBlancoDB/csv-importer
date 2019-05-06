namespace CsvImporter.Common.Contracts.Exceptions
{
    public class ConfigurationNotFoundException : BaseException
    {
        public ConfigurationNotFoundException(string configurationKey) 
            : base(string.Format("Missing {0} configuration.", configurationKey))
        {
        }

        public override int StatusCode => 400;
    }
}