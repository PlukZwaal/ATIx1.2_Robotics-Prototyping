using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

public static class DatabaseConnection
{
    //Dit is mijn connectionstring. Erg slordig om het hier hard coded neer te zetten, maar dat is nu eenmaal nu.
    private static readonly string ConnectionString = "Server=aei-sql2.avans.nl,1443;Database=DB2228229;uid=ITI2228229;password=H8yiE2L4;TrustServerCertificate=True";

    //Deze methode logt de data in de database.
    public static void LogData(float temperature, float humidity, int battery, int lux)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO SensorData (Temperature, Humidity, Battery, Lux) VALUES (@Temperature, @Humidity, @Battery, @Lux)";
                command.Parameters.AddWithValue("@Temperature", temperature);
                command.Parameters.AddWithValue("@Humidity", humidity);
                command.Parameters.AddWithValue("@Battery", battery);
                command.Parameters.AddWithValue("@Lux", lux);
                command.ExecuteNonQuery();
            }
        }
    }
}