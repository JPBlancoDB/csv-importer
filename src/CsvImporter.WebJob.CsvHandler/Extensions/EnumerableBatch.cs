using System.Collections.Generic;

namespace CsvImporter.WebJob.CsvHandler.Extensions
{
    public static class EnumerableBatchExtension
    {
        public static IEnumerable<List<T>> Batch<T>(this IEnumerable<T> collection, int batchSize)
        {
            var list = new List<T>(batchSize);
            
            foreach (var item in collection)
            {
                list.Add(item);

                if (list.Count != batchSize) continue;
                
                yield return list;
                list = new List<T>(batchSize);
            }
            
            if (list.Count > 0)
                yield return list;
        }
    }
}