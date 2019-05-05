using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsvImporter.Common.Contracts.Entities
{
    [Table("Jobs")]
    public class JobEntity
    {
        public int Id { get; set; }

        public Guid JobId { get; set; }

        public JobStatus JobStatus { get; set; }

        public string FileName { get; set; }

        public string OriginalFileName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }
        
        public DateTime? DateStarted { get; set; }

        public DateTime? DateLastModified { get; set; }
    }
}
