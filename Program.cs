
using System;
using CSharpBigQueryETLDemo.ETL;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting ETL process...");

        var data = Extractor.Extract("data/customers.csv");
        var transformed = Transformer.Transform(data);
        await Loader.LoadToBigQuery(transformed);

        Console.WriteLine("ETL process completed.");
    }
}
