using System.IO;
using System.Threading.Tasks;

namespace CsvImporter.WebApi.Abstractions
{
    public interface ICloudBlockBlob
    {
        Task UploadFromStreamAsync(Stream stream);
    }
}