using Newtonsoft.Json;

namespace WAMSDataImport
{
    public struct TimetableData {
        public int startTime;
        public int endTime;
        public int date;
        public string klasse;
        public string lehrer;
        public string fach;
        public string raum;
    }

    public class WebUntisScraper
    {
        static readonly HttpClient client = new HttpClient();
        static readonly DateTime currentDateTime = DateTime.Now;
        static string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd");

        static string elementId = "5357";

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
                /* TODO: Add handling in case the requests dont work 
                 *  Maybe resend it a couple of times and if it doesn't work send a message to someone?
                 *  */

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
                            $"&elementId={elementId}" +
                            $"&date={formattedDateTime}" +
                            $"&formatId=1" + 
                            $"&filter.departmentId=-1"
                            );

                    if (timetableResponse.IsSuccessStatusCode)
                    {
                        string content = await timetableResponse.Content.ReadAsStringAsync();

                        dynamic data = JsonConvert.DeserializeObject<dynamic>(content)!;

                        var mappedData = MapData(data.data.result.data);

                        Console.WriteLine($"{mappedData}");
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

        static private string MapData(dynamic content)
        {
            List<TimetableData> timetableList = new List<TimetableData>();

            // Put everything together for the timetable
            foreach (var data in content["elementPeriods"][elementId])
            {
                TimetableData timetable = new TimetableData
                {
                    startTime = data["startTime"].ToObject<int>(),
                    endTime = data["endTime"].ToObject<int>(),
                    date = data["date"].ToObject<int>(),
                };

                foreach (var element in data["elements"])
                {
                    var value = FindName(content["elements"], element.type, element.id);
                    if (value != null)
                    {
                        switch (Convert.ToInt32(element.type))
                        {
                            case 1:
                                // klasse
                                timetable.klasse = Convert.ToString(value);
                                break;
                            case 2:
                                // lehrer
                                timetable.lehrer = Convert.ToString(value);
                                break;
                            case 3:
                                // fach
                                timetable.fach = Convert.ToString(value);
                                break;
                            case 4:
                                // raum
                                timetable.raum = Convert.ToString(value);
                                break;
                            default:
                                Console.WriteLine($"Es existiert kein gültiger Eintrag: {value}");
                                break;
                        }
                    }
                }
                timetableList.Add(timetable);
            }
            var groupedTimetable = timetableList
                .GroupBy(t => t.date)
                .ToDictionary(g => g.Key, g => g.ToList());

            List<List<TimetableData>> groupedTimetableList = new List<List<TimetableData>>();

            foreach (var group in groupedTimetable)
            {
                var sortedGroup = group.Value.OrderBy(t => t.startTime).ToList();
                groupedTimetableList.Add(sortedGroup);
            }

            var sortedGroupedTimetableList = groupedTimetableList
                .OrderBy(list => list.First().date)
                .ToList();

            string mappedJson = JsonConvert.SerializeObject(sortedGroupedTimetableList, Formatting.Indented);

            return mappedJson;
        }

        static private dynamic FindName(dynamic content, dynamic type, dynamic id)
        {
            var name = "";
            foreach (var data in content)
            {
                if (data.type == type && data.id == id)
                {
                    name = data.name;
                }
            }

            return name;
        }
    }
}
