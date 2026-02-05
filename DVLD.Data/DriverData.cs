using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using DVLD.Core.DTOs.Entities;

namespace DVLD.Data
{
    public static class DriverData
    {
        // -----------------------Create-------------------------
        public static int Add(Driver driver)
        {
            string query = @"INSERT INTO Drivers (PersonId, CreatedByUserId, CreatedDate)
                    VALUES (@PersonId, @CreatedByUserId, @CreatedDate);
                    SELECT SCOPE_IdENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    DateTime dt = DateTime.Now;
                    if (dt.Second >= 30) dt = dt.AddMinutes(1);
                    DateTime smallDateTimeValue = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);

                    command.Parameters.AddWithValue("@PersonId", driver.PersonId);
                    command.Parameters.AddWithValue("@CreatedByUserId", driver.CreatedByUserId);
                    command.Parameters.AddWithValue("@CreatedDate", smallDateTimeValue);

                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedId))
                        return insertedId;
                    else
                        return -1;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into drivers.", ex);
                throw;
            }
        }


        // ----------------------Read----------------------------
        public static Driver GetByDriverId(int driverId)
        {
            string query = @"SELECT * FROM Drivers WHERE DriverId = @driverId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@driverId", driverId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Driver
                            (
                                Convert.ToInt32(reader["DriverId"]),
                                Convert.ToInt32(reader["PersonId"]),
                                Convert.ToInt32(reader["CreatedByUserId"]),
                                Convert.ToDateTime(reader["CreatedDate"])
                            );
                        }
                        
                        return null;
                    }
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from Drivers.", ex);
                throw;
            }
        }

        public static Driver GetByPersonId(int personId)
        {
            string query = "SELECT * FROM Drivers WHERE PerosnId = @personId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@personId", personId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Driver
                            (
                                Convert.ToInt32(reader["DriverId"]),
                                Convert.ToInt32(reader["PersonId"]),
                                Convert.ToInt32(reader["CreatedByUserId"]),
                                Convert.ToDateTime(reader["CreatedDate"])
                            );
                        }
                        return null;
                    }
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Erorr while selecting form Drivers.", ex);
                throw;
            }
        }

        public static bool IsPersonUsed(int personId, int excludedDriverId)
        {
            string query = "SELECT 1 FROM Drivers WHERE PerosnId = @personId AND DriverId != excludedDriverId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@excludedDriverId", excludedDriverId);
                    command.Parameters.AddWithValue("@personId", personId);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Erorr while selecting form Drivers.", ex);
                throw;
            }
        }

        public static bool Exists(int driverId)
        {
            string query = "SELECT 1 FROM Drivers WHERE DriverId = @driverId;";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@driverId", driverId);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Erorr while selecting form Drivers.", ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            string query = "SELECT * FROM Drivers;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            return dataTable;
                        }
                    }

                    return null;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting all from Drivers.", ex);
                throw;
            }
        }


        // --------------------Update-----------------------------
        public static bool Update(Driver driver)
        {
            string query = @"UPDATE Drivers
                            SET PersonId = @personId
                            WHERE DriverId = @driverId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@personId", driver.PersonId);
                    command.Parameters.AddWithValue("@driverId", driver.DriverId);

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while updating Drivers.", ex);
                throw;
            }
        }


        // --------------------Delete-----------------------------
        public static bool DeleteByDriverId(int driverId)
        {
            string query = @"DELETE FROM Drivers WHERE DriverId = @driverId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@driverId", driverId);
                    
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while deleting form Drivers where driver id = {driverId}", ex);
                throw;
            }
        }

        public static bool DeleteByPersonId(int personId)
        {
            string query = @"DELETE FROM Drivers WHERE PersonId = @personId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@personId", personId);
                    connection.Open();
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while deleting from Drivers where person id = {personId}.", ex);
                throw;
            }
        }

    }
}
