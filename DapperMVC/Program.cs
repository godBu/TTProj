using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DapperMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "localhost,1433";
                builder.UserID = "sa";
                builder.Password = "Password12345";
                builder.InitialCatalog = "Movies";

                //string SELECT = "SELECT * FROM MovieModel";
                //string INSERT = "INSERT INTO MovieModel (MovieID, Title, Genre, Rating, ReleaseDate, IMDbScore) VALUES (3, 'Bee Movie', 'Animated Comedy', 'U', '2007-12-14', 6.9)";
                string UPDATE = "UPDATE MovieModel SET Genre = 'Animation, Comedy' WHERE MovieID = 2";
                //string DELETE = "DELETE FROM MovieModel WHERE MovieID = 3";

                Console.WriteLine("Connecting to SQL server");
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                using (connection)
                {
                    //SqlCommand cmd = new SqlCommand(SELECT, connection);
                    //SqlCommand cmd = new SqlCommand(INSERT, connection);
                    SqlCommand cmd = new SqlCommand(UPDATE, connection);
                    //SqlCommand cmd = new SqlCommand(DELETE, connection);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.WriteLine(reader.GetValue(i));
                            }
                        }
                    }
                    Console.WriteLine("Done");
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.ToString());
            }
            Console.Write("All done");
            Console.Read();
        }
    }
}

/*
 * SQL to C# - it takes a few things to connect
 * 1. DataSource = localhost... if you're on Windows, it is simply localhost
 * If you're using docker, it's simply not localhost
 * 
 * 
 * 
 */
