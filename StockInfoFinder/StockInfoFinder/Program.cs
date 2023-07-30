using System.Globalization;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace StockInfoFinder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string apiKey = "7JK2ZHRJASAD3H7Y";
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Get the info of a company by typing it's ticker: ");
                    string symbol = Console.ReadLine();

                    // Construct the API endpoint URL with the required query parameters
                    string apiUrl = $"https://www.alphavantage.co/query?function=OVERVIEW&symbol={symbol}&apikey={apiKey}";
                    Uri queryUri = new Uri(apiUrl);

                    // Create a new instance of WebClient
                    using (WebClient webClient = new WebClient())
                    {
                        // Download the JSON response from the API
                        string jsonResponse = webClient.DownloadString(apiUrl);

                        // Deserialize Json data
                        dynamic json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(webClient.DownloadString(queryUri));

                        // Process the JSON response to extract company overview data

                        var companyOverviewData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                        Console.WriteLine($"Company Name: {companyOverviewData.Name}");
                        Console.WriteLine($"Sector: {companyOverviewData.Sector}");
                        Console.WriteLine($"Description: {companyOverviewData.Description}");
                        Console.ReadLine();
                    }

                }
            }
            catch (WebException ex)
            {
                // Handle any exceptions that occurred during the WebClient request
                Console.WriteLine($"Error while making the API call: {ex.Message}");
            }
        }
    }
}