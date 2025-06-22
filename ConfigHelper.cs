using Microsoft.Extensions.Configuration;
using System.IO;

public class ConfigHelper
{
  public static IConfigurationRoot GetConfig()
  {
    return new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false)
        .Build();
  }
}
