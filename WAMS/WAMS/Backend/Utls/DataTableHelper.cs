using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Diagnostics;

namespace WAMS.Backend.Utls
{
    public class DataTableHelper
    {
        //private readonly IConfiguration Configuration;

        public class DataTableSQLFeedback
        {
            public List<JSONUser> JsonUsers { get; set; } = new List<JSONUser>();
            public int TotalCount = 0;
            public int SearchCount = 0;
        }

        public static DataTableSQLFeedback GetUsersPage(string Index = "0", string Rows = "10", string search = "", string sortingColumn = "", string sorting = "asc")
        {
            DataTableSQLFeedback result = new DataTableSQLFeedback();
            string? connectionString = "server=45.85.219.82;Port=3306;database=WAMS;User=berufsschule;Password=Vw]xG)3z3ZqmGPFC;";

            using (MySqlConnection connection = new MySqlConnection(connectionString)) {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                connection.Open();
                MySqlCommand command;

                // Überprüfen, ob Index und Rows gültige Ganzzahlen sind
                if (!int.TryParse(Index, out int offset)) {
                    offset = 0; // Standardwert, falls ungültig
                }

                if (!int.TryParse(Rows, out int limit)) {
                    limit = 10; // Standardwert, falls ungültig
                }

                if (String.IsNullOrEmpty(search)) {
                    command = new MySqlCommand(@$"
            SELECT 
                u.*, 
                COUNT(*) OVER () AS TotalCount
            FROM Users u
            LIMIT {limit} OFFSET {offset}", connection);
                } else {
                    command = new MySqlCommand(@$"
            WITH CurrentUsers AS (
                SELECT 
                    u.*, 
                    COUNT(*) OVER () AS TotalCount
                FROM Users u
                WHERE 
                    u.Username LIKE '%{search}%' 
                    OR u.FirstName LIKE '%{search}%' 
                    OR u.LastName LIKE '%{search}%' 
                    OR u.Role LIKE '%{search}%'
            )
            SELECT 
                TotalCount AS TotalCount,
                COUNT(*) AS SearchCount,
                CurrentUsers.*
            FROM 
                CurrentUsers
            LIMIT {limit} OFFSET {offset};", connection);
                }

                Debug.WriteLine("Query=" + command.CommandText);

                MySqlDataReader reader = command.ExecuteReader();

                stopwatch.Stop();
                Debug.WriteLine("---------------SQL Query-----------------");
                Debug.WriteLine("Query=" + command.CommandText);
                Debug.WriteLine($"Query execution time: {stopwatch.Elapsed.TotalMilliseconds} ms");
                Debug.WriteLine("--------------------------------------------");

                while (reader.Read()) {
                    if (!String.IsNullOrEmpty(reader["Id"].ToString())) {
                        JSONUser user = new JSONUser {
                            ID = reader["Id"].ToString(),
                            Avatar = "",
                            Username = reader["Username"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Role = reader["Role"].ToString(),
                            Status = reader["Status"].ToString(),
                            IsAdmin = reader["IsAdmin"].ToString() == "1",
                            LastLogin = reader["LastLogin"].ToString()
                        };

                        result.JsonUsers.Add(user);
                    }

                    result.TotalCount = Int32.Parse(reader["TotalCount"].ToString());

                    if (HasColumn(reader, "SearchCount")) {
                        result.SearchCount = Int32.Parse(reader["SearchCount"].ToString());
                    }
                }

                connection.Close();
            }
            return result;
        }



        public static bool HasColumn(MySqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++) {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase)) {
                    return true;
                }
            }
            return false;
        }

        private static string getSorting(string sortingColumn, string sorting)
        {
            string column = sortingColumn switch {
                "1" => "Username",
                "2" => "Role",
                "3" => "FirstName",
                "4" => "LastName",
                "5" => "LastLogin",
                "6" => "Status",
                _ => "LastLogin",
            };

            return $"ORDER BY {column} {sorting}";
        }
    }

    public class JSONUser
    {
        public string ID { get; set; }
		public string Avatar { get; set; }
		public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public bool IsAdmin { get; set; }
        public string LastLogin { get; set; }
    }
}
