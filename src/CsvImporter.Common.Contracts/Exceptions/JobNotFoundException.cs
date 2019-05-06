using System;

namespace CsvImporter.Common.Contracts.Exceptions
{
    public class JobNotFoundException : BaseException
    {
        public JobNotFoundException(Guid jobId) 
            : base(string.Format("JobId: {0} not found.", jobId))
        {
        }

        public override int StatusCode => 404;
    }
}