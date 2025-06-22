
using Google.Cloud.BigQuery.V2;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpBigQueryETLDemo.ETL
{
    public static class Loader
    {
        public static async Task LoadToBigQuery(List<Customer> data)
        {
            var config = ConfigHelper.GetConfig();
            string projectId = config["BigQuery:ProjectId"];
            string datasetId = config["BigQuery:DatasetId"];
            string tableId = config["BigQuery:TableId"];

            var client = BigQueryClient.Create(projectId);
            var rows = new List<BigQueryInsertRow>();

            foreach (var item in data)
            {
                rows.Add(new BigQueryInsertRow
                {
                    { "customer_id", item.CustomerId },
                    { "visit_date", item.VisitDate },
                    { "visit_count", item.VisitCount }
                });
            }

            await client.InsertRowsAsync(datasetId, tableId, rows);
        }
    }
}
