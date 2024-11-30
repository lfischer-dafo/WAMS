using Newtonsoft.Json;

namespace WAMSDataImport
{
    public class WebUntisScraper
    {
        static readonly HttpClient client = new HttpClient();
        static readonly DateTime currentDateTime = DateTime.Now;
        static string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd");

        static private async Task Main()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "school", "oskar-von-miller-kassel" },
                    { "j_username", "Test-User-FI" },
                    { "j_password", Environment.GetEnvironmentVariable("WEBUNTIS_PASSWORT") ?? "" },
                    { "token", "" }
            };

            try
            {
                // TODO: Add handling in case the requests dont work
                
                HttpResponseMessage response = await client.GetAsync(
                        $"https://mese.webuntis.com/WebUntis/j_spring_security_check" +
                        $"?school={queryParameters["school"]}" +
                        $"&j_username={queryParameters["j_username"]}" +
                        $"&j_password={queryParameters["j_password"]}" +
                        $"&token"
                        );

                if (response.IsSuccessStatusCode)
                {
                    HttpResponseMessage timetableResponse = await client.GetAsync(
                            $"https://mese.webuntis.com/WebUntis/api/public/timetable/weekly/data" +
                            $"?elementType=1" +
                            $"&elementId=5357" +
                            $"&date={formattedDateTime}" +
                            $"&formatId=1" + 
                            $"&filter.departmentId=-1"
                            );

                    if (timetableResponse.IsSuccessStatusCode)
                    {
                        string content = await timetableResponse.Content.ReadAsStringAsync();

                        dynamic data = JsonConvert.DeserializeObject<dynamic>(content)!;

                        Console.WriteLine($"{content}");
                    }
                    else
                    {
                        Console.WriteLine("An Error happened while fetching data");
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
