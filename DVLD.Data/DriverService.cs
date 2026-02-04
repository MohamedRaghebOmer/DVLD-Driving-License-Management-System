using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using DVLD.Core.DTOs.Entities;

namespace DVLD.Data
{
    public static class DriverService
    {
        // -----------------------Create-------------------------
        public static bool AddNewDriver(Driver driver)
        {
            string query = @"INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
                    VALUES (@PersonID, @CreatedByUserID, @CreatedDate);
                    SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    DateTime dt = DateTime.Now;
                    if (dt.Second >= 30) dt = dt.AddMinutes(1);
                    DateTime smallDateTimeValue = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);

                    command.Parameters.AddWithValue("@PersonID", driver.PersonID);
                    command.Parameters.AddWithValue("@CreatedByUserID", driver.CreatedByUserID);
                    command.Parameters.AddWithValue("@CreatedDate", smallDateTimeValue);

                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        driver.DriverID = insertedID;
                        driver.CreatedDate = smallDateTimeValue;
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into drivers.", ex);
                throw;
            }
        }


        // ----------------------Read----------------------------
        public static Driver GetDriverByID(int driverID)
        {
            string query = @"SELECT * FROM Drivers WHERE DriverID = @driverID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@driverID", driverID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Driver
                            (
                                Convert.ToInt32(reader["DriverID"]),
                                Convert.ToInt32(reader["PersonID"]),
                                Convert.ToInt32(reader["CreatedByUserID"]),
                                Convert.ToDateTime(reader["CreatedDate"]),
                                Convert.ToDateTime(reader["DeletedDate"])
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

        public static Driver GetDriverByPersonID(int personID)
        {
            string query = "SELECT * FROM Drivers WHERE PerosnID = @personID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@personID", personID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Driver
                            (
                                Convert.ToInt32(reader["DriverID"]),
                                Convert.ToInt32(reader["PersonID"]),
                                Convert.ToInt32(reader["CreatedByUserID"]),
                                Convert.ToDateTime(reader["CreatedDate"]),
                                Convert.ToDateTime(reader["DeletedDate"])
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

        public static bool IsPersonUsed(int personID)
        {
            string query = "SELECT 1 FROM Drivers WHERE PerosnID = @personID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@personID", personID);
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

        public static bool IsPersonUsed(int personID, int excludedDriverID)
        {
            string query = "SELECT 1 FROM Drivers WHERE PerosnID = @personID AND DriverID != excludedDriverID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@excludedDriverID", excludedDriverID);
                    command.Parameters.AddWithValue("@personID", personID);
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

        public static DataTable GetAllDrivers()
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
                            SET PersonID = @personID
                            WHERE DriverID = @driverID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@personID", driver.PersonID);
                    command.Parameters.AddWithValue("@driverID", driver.DriverID);

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
        public static bool DeleteByDriverID(int driverID)
        {
            string query = @"UPDATE Drivers SET DeletedDate = @deletedDate WHERE DriverID = @dirverID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    DateTime deletedDate = DateTime.Now;
                    command.Parameters.AddWithValue("@deletedDate", deletedDate);
                    command.Parameters.AddWithValue("@driverID", driverID);
                    
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while changing DeletedDate field in Drivers to DateTime.Now", ex);
                throw;
            }
        }

        public static bool DeleteByPersonID(int personID)
        {
            string query = @"UPDATE Drivers SET DeletedDate = @deletedDate WHERE PersonID = @personID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    DateTime deletedDate = DateTime.Now;
                    command.Parameters.AddWithValue("@deletedDate", deletedDate);
                    command.Parameters.AddWithValue("@personID", personID);
                    connection.Open();
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while deleting from Drivers.", ex);
                throw;
            }
        }

    }
}
