using System;

namespace CsvImporter.Common.Contracts.DTOs
{
    public class JobDto
    {
        public Guid JobId { get; set; }

        public string JobStatus { get; set; }

        public string FileName { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateLastModified { get; set; }
    }
}