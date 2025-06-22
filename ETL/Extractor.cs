
using System.Collections.Generic;
using System.IO;

namespace CSharpBigQueryETLDemo.ETL
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string VisitDate { get; set; }
        public int VisitCount { get; set; }
    }

    public static class Extractor
    {
        public static List<Customer> Extract(string filePath)
        {
            var customers = new List<Customer>();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines[1..]) // Skip header
            {
                var parts = line.Split(',');
                customers.Add(new Customer
                {
                    CustomerId = parts[0],
                    VisitDate = parts[1],
                    VisitCount = int.Parse(parts[2])
                });
            }
            return customers;
        }
    }
}
