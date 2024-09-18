using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace NyrisBilderSucheNew.Utls
{

    /// <summary>
    ///  Version 1.2 
    /// </summary>
    #region "ChangeLog"
    //Version 1.1
    // + SqlQueryResult<T> ExecuteQuery<T> wo eine OBJ angegeben werden kann,
    //   welches anhand der SQL_ColumnNameAttribute definiert wird.
    #endregion Region

    internal class SqlHelper
    {
        private SqlConnection Connection;
        //private readonly List<SqlQueryResult<object>> queryResults;


        public SqlHelper(string server, string database, string user, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = server,
                UserID = user,
                Password = password,
                InitialCatalog = database,
                TrustServerCertificate = true,
            };

            Connection = new SqlConnection(builder.ConnectionString);
        }

        public bool TestDatabaseConnection()
        {
            try
            {
                Connection.Open();
                Console.WriteLine("[SQLHelper] Verbindung erfolgreich geöffnet!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"[SQLHelper] Fehler beim Öffnen der Verbindung: {e.Message}");
                return false;
            }
            finally
            {
                Connection.Close();
            }
        }

        public SqlQueryResult ExecuteQuery(string sqlQuery)
        {
            SqlQueryResult result = new SqlQueryResult
            {
                Result = new List<Dictionary<string, object>>(),
                sqlQuery = sqlQuery,
                QueryExecutionTime = 0,
                CommandType = SqlCommandType.Select
            };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                Connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, Connection) { CommandTimeout = 5000 })
                {


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object value = reader[i];

                                data[columnName] = value;
                            }

                            result.Result.Add(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler bei der Ausführung der Abfrage: " + ex.Message);
                // Hier könntest du weitere Maßnahmen ergreifen, je nach Bedarf
            }
            finally
            {
                Connection.Close();
            }

            stopwatch.Stop();
            result.QueryExecutionTime = stopwatch.Elapsed.TotalMilliseconds;


            //queryResults.Add(result); // Füge das SqlQueryResult<T>-Objekt zur Liste hinzu

            return result;
        }


        public SqlQueryResult<T> ExecuteQuery<T>(string sqlQuery) where T : class, new()
        {
            List<T> returnList = new List<T>();

            SqlQueryResult<T> result = new SqlQueryResult<T>
            {
                Result = returnList,
                sqlQuery = sqlQuery,
                QueryExecutionTime = 0,
                CommandType = SqlCommandType.Select
            };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                Connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, Connection) { CommandTimeout = 5000 })
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T returnObj = new T();
                            foreach (var prop in typeof(T).GetProperties())
                            {
                                if (prop.CanWrite)
                                {
                                    // Standard-Spaltennamen verwenden, falls kein Attribut vorhanden ist
                                    var columnName = prop.Name;

                                    // Überprüfen, ob ein ColumnName-Attribut vorhanden ist
                                    var columnAttr = prop.GetCustomAttributes(typeof(SQL_ColumnNameAttribute), false)
                                                       .FirstOrDefault() as SQL_ColumnNameAttribute;
                                    if (columnAttr != null)
                                    {
                                        columnName = columnAttr.Name;
                                    }

                                    if (reader.HasColumn(columnName))
                                    {
                                        object value = reader[columnName];
                                        if (value != DBNull.Value)
                                        {
                                            prop.SetValue(returnObj, value);
                                        }
                                    }
                                }
                            }
                            returnList.Add(returnObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler bei der Ausführung der Abfrage: " + ex.Message);
                // Hier könnten weitere Maßnahmen ergriffen werden, je nach Bedarf
            }
            finally
            {
                Connection.Close();
            }

            stopwatch.Stop();
            result.QueryExecutionTime = stopwatch.Elapsed.TotalMilliseconds;

            return result;
        }

        public SqlUpdateResult UpdateQuery(string sqlQuery)
        {
            SqlUpdateResult result = new SqlUpdateResult
            {
                Result = -1,
                sqlQuery = sqlQuery,
                QueryExecutionTime = 0,
                CommandType = SqlCommandType.Update
            };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                Connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, Connection))
                {
                    result.Result = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler bei der Ausführung der Update-Abfrage: " + ex.Message);
                // Hier könntest du weitere Maßnahmen ergreifen, je nach Bedarf
            }
            finally
            {
                Connection.Close();
            }

            stopwatch.Stop();
            result.QueryExecutionTime = stopwatch.Elapsed.TotalMilliseconds;
            return result;
        }

        //public void ClearResults()
        //{
        //    queryResults.Clear();
        //}

        //public List<SqlQueryResult<object>> GetResults()
        //{
        //    return queryResults;
        //}

        public enum SqlCommandType
        {
            Select,
            Insert,
            Update,
            Delete
        }


        public class SqlQueryResult<T>
        {
            public List<T> Result { get; set; }
            public string sqlQuery { get; set; }
            public double QueryExecutionTime { get; set; }
            public SqlCommandType CommandType { get; set; }



            public void DebugString()
            {
                Debug.WriteLine("---------------SQL Abfrage-----------------");
                Debug.WriteLine($"Query: {sqlQuery}");
                Debug.WriteLine($"Command Type: {CommandType}");
                Debug.WriteLine($"Rows: {Result.Count.ToString("N0")}");
                Debug.WriteLine($"Abfragezeit: {QueryExecutionTime} ms");
                Debug.WriteLine("--------------------------------------------");
            }

            public void ConsoleString()
            {
                Console.WriteLine("---------------SQL Abfrage-----------------");
                Console.WriteLine($"Query: {sqlQuery}");
                Console.WriteLine($"Command Type: {CommandType}");
                Console.WriteLine($"Rows: {Result.Count.ToString("N0")}");
                Console.WriteLine($"Abfragezeit: {QueryExecutionTime} ms");
                Console.WriteLine("--------------------------------------------");
            }
        }

        public class SqlQueryResult
        {
            //public List<Dictionary<string, object>> Result { get; set; }
            public List<Dictionary<string, object>> Result { get; set; }
            public string sqlQuery { get; set; }
            public double QueryExecutionTime { get; set; }
            public SqlCommandType CommandType { get; set; }



            public void DebugString()
            {
                Debug.WriteLine("---------------SQL Abfrage-----------------");
                Debug.WriteLine($"Query: {sqlQuery}");
                Debug.WriteLine($"Command Type: {CommandType}");
                Debug.WriteLine($"Rows: {Result.Count.ToString("N0")}");
                Debug.WriteLine($"Abfragezeit: {QueryExecutionTime} ms");
                Debug.WriteLine("--------------------------------------------");
            }

            public void ConsoleString()
            {
                Console.WriteLine("---------------SQL Abfrage-----------------");
                Console.WriteLine($"Query: {sqlQuery}");
                Console.WriteLine($"Command Type: {CommandType}");
                Console.WriteLine($"Rows: {Result.Count.ToString("N0")}");
                Console.WriteLine($"Abfragezeit: {QueryExecutionTime} ms");
                Console.WriteLine("--------------------------------------------");
            }
        }

        public class SqlUpdateResult
        {
            public int Result { get; set; } //anzahl der Updates
            public string sqlQuery { get; set; }
            public double QueryExecutionTime { get; set; }
            public SqlCommandType CommandType { get; set; }

            public void DebugString()
            {
                Debug.WriteLine("---------------SQL Abfrage-----------------");
                Debug.WriteLine($"Query: {sqlQuery}");
                Debug.WriteLine($"Command Type: {CommandType}");
                Debug.WriteLine($"Abfragezeit: {QueryExecutionTime} ms");
                Debug.WriteLine("--------------------------------------------");
            }

            public void ConsoleString()
            {
                Console.WriteLine("---------------SQL Abfrage-----------------");
                Console.WriteLine($"Query: {sqlQuery}");
                Console.WriteLine($"Command Type: {CommandType}");
                Console.WriteLine($"Abfragezeit: {QueryExecutionTime} ms");
                Console.WriteLine("--------------------------------------------");
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SQL_ColumnNameAttribute : Attribute
    {
        public string Name { get; }

        public SQL_ColumnNameAttribute(string name)
        {
            Name = name;
        }
    }

    public static class Extensions
    {
        public static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
