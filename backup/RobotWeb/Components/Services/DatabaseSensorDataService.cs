using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace RobotWeb.Components.Services
{
    public class DatabaseSensorDataService
    {
        private readonly string connectionString = "Server=aei-sql2.avans.nl,1443;Database=DB2228229;uid=ITI2228229;password=H8yiE2L4;TrustServerCertificate=True";

        public async Task<List<SensorData>> GetSensorDataAsync(int numberOfDataMoments)
        {
            var sensorDataList = new List<SensorData>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = $"SELECT TOP {numberOfDataMoments} Id, Temperature, Humidity, Battery, Lux, Timestamp FROM SensorData ORDER BY Timestamp DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var data = new SensorData
                            {
                                Id = reader.GetInt32(0),
                                Temperature = reader.GetDouble(1),
                                Humidity = reader.GetDouble(2),
                                Battery = reader.GetInt32(3),
                                Lux = reader.GetInt32(4),
                                Timestamp = reader.GetDateTime(5).ToString("yyyy-MM-dd HH:mm:ss")
                            };
                            sensorDataList.Add(data);
                        }
                    }
                }
            }

            return sensorDataList;
        }
    }
}
