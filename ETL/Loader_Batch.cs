
using Google.Cloud.BigQuery.V2;
using Google.Cloud.Storage.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CSharpBigQueryETLDemo.ETL;

public class Loader_Batch
{
    public static async Task LoadToBigQueryBatch(List<Customer> data)
    {
        var config = ConfigHelper.GetConfig();
        string projectId = config["BigQuery:ProjectId"];
        string datasetId = config["BigQuery:DatasetId"];
        string tableId = config["BigQuery:TableId"];
        string bucketName = "carwash-etl-bucket"; // Update if necessary
        string localJsonPath = Path.Combine("data", "customer_visits.json");
        string objectName = $"uploads/customer_visits_{DateTime.UtcNow:yyyyMMdd_HHmmss}.json";

        // 1. Serialize data to JSON
        Directory.CreateDirectory("data");
        //File.WriteAllText(localJsonPath, JsonConvert.SerializeObject(data));
        using (var writer = new StreamWriter(localJsonPath))
        {
            foreach (var record in data)
            {
                var jsonLine = JsonConvert.SerializeObject(record);
                writer.WriteLine(jsonLine);
            }
        }

        // 2. Upload JSON to Cloud Storage
        var storageClient = await StorageClient.CreateAsync();
        using (var fileStream = File.OpenRead(localJsonPath))
        {
            await storageClient.UploadObjectAsync(bucketName, objectName, "application/json", fileStream);
            Console.WriteLine($"📤 Uploaded to gs://{bucketName}/{objectName}");
        }

        // 3. Load to BigQuery from GCS (batch job)
        var bigQueryClient = await BigQueryClient.CreateAsync(projectId);
        var tableRef = bigQueryClient.GetTableReference(projectId, datasetId, tableId);

        var jobOptions = new CreateLoadJobOptions
        {
            SourceFormat = FileFormat.NewlineDelimitedJson,
            WriteDisposition = WriteDisposition.WriteAppend
        };

        var loadJob = bigQueryClient.CreateLoadJob(
            sourceUri: $"gs://{bucketName}/{objectName}",
            destination: tableRef,
            schema: null, // BigQuery infiere el esquema desde el archivo JSON
            options: jobOptions
        );

        loadJob.PollUntilCompleted();

        Console.WriteLine("✅ BigQuery batch load completed.");
    }
}
