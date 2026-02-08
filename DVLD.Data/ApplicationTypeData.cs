using System;
using System.Data.SqlClient;
using DVLD.Data.Settings;
using DVLD.Core.Logging;
using System.Collections.Generic;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Data
{
    public static class ApplicationTypeData
    {
        public static decimal GetFees(ApplicationType applicationType)
        {
            string query = "SELECT Fees FROM ApplicationTypes WHERE ApplicationTypeId = @ApplicationTypeId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationTypeId", (int)applicationType);
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && decimal.TryParse(result.ToString(), out decimal fees))
                        return fees;
                }
                return -1; // Return -1 if the application type is not found or if the value is invalid
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching fees for application type {applicationType}.", ex);
                throw;
            }
        }

        public static List<string> GetAllApplicationTypeTitles()
        {
            string query = "SELECT ApplicationTypeTitle FROM ApplicationTypes;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> titles = new List<string>();
                        while (reader.Read())
                        {
                            titles.Add(reader["ApplicationTypeTitle"].ToString());
                        }
                        return titles;
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving from ApplicationTypes.", ex);
                throw;
            }
        }

        public static bool UpdateFees(ApplicationType applicationType, decimal newFees)
        {
            string query = "UPDATE ApplicationTypes SET Fees = @Fees WHERE ApplicationTypeId = @ApplicationTypeId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Fees", newFees);
                    command.Parameters.AddWithValue("@ApplicationTypeId", (int)applicationType);
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if the update was successful
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while updating fees for application type {applicationType}.", ex);
                throw;
            }
        }
    }
}