using System;

namespace CsvImporter.WebJob.JsonService
{
    public class Program
    {
        private static void Main(string[] args)
        {
            new Bootstrapper().InitializeApp();

            Console.ReadKey();
        }
    }
}
