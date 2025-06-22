
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CSharpBigQueryETLDemo.ETL
{
    public class Customer
    {
        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("visit_date")]
        public string VisitDate { get; set; }

        [JsonProperty("visit_count")]
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
