
using System.Collections.Generic;
using System.Linq;

namespace CSharpBigQueryETLDemo.ETL
{
    public static class Transformer
    {
        public static List<Customer> Transform(List<Customer> data)
        {
            return data
                .Where(c => !string.IsNullOrWhiteSpace(c.CustomerId))
                .ToList();
        }
    }
}
