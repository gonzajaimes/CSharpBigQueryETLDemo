
# CSharpBigQueryETLDemo

A simple ETL pipeline in C# that reads data from a CSV file, transforms it, and loads it into Google BigQuery.

## Features
- Extracts data from CSV
- Filters and transforms records
- Loads to BigQuery using `Google.Cloud.BigQuery.V2`

## How to Run
1. Update `appsettings.json` with your GCP project details.
2. Set the environment variable: `GOOGLE_APPLICATION_CREDENTIALS` with your service account path.
3. Place `customers.csv` in a `data/` folder.
4. Run the app: `dotnet run`

## Dependencies
- .NET 6+
- Google.Cloud.BigQuery.V2 NuGet package

## License
MIT
