using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Data
{
    public static class LicenseClassData
    {
        public static DataTable GetAll()
        {
            string query = "SELECT * FROM LicenseClasses;";

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
                }
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while fetching all license classes.", ex);
                throw;
            }
        }

        public static int GetMinimumAllowedAge(LicenseClass licenseClass)
        {
            string query = "SELECT MinimumAllowedAge FROM LicenseClasses WHERE LicenseClassId = @LicenseClassId;";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseClassId", (int)licenseClass);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    
                    if (result != null && int.TryParse(result.ToString(), out int minimumAge))
                        return minimumAge;
                }

                return -1; // Return -1 if the license class is not found or if the value is invalid
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching minimum allowed age for license class {licenseClass}.", ex);
                throw;
            }
        }

        public static int GetDefaultValidityLength(LicenseClass licenseClass)
        {
            string query = "SELECT DefaultValidityLength FROM LicenseClasses WHERE LicenseClassId = @LicenseClassId;";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseClassId", (int)licenseClass);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int validityLength))
                        return validityLength;
                }
                return -1; // Return -1 if the license class is not found or if the value is invalid
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching default validity length for license class {licenseClass}.", ex);
                throw;
            }
        }

        public static decimal GetClassFees(LicenseClass licenseClass)
        {
            string query = "SELECT ClassFees FROM LicenseClasses WHERE LicenseClassId = @LicenseClassId;";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseClassId", (int)licenseClass);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && decimal.TryParse(result.ToString(), out decimal classFees))
                        return classFees;
                }
                return -1; // Return -1 if the license class is not found or if the value is invalid
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching class fees for license class {licenseClass}.", ex);
                throw;
            }
        }

        public static bool UpdateMinimumAllowedAge(LicenseClass licenseClass, int newMinimumAge)
        {
            string query = "UPDATE LicenseClasses SET MinimumAllowedAge = @MinimumAllowedAge WHERE LicenseClassId = @LicenseClassId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MinimumAllowedAge", newMinimumAge);
                    command.Parameters.AddWithValue("@LicenseClassId", (int)licenseClass);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while updating minimum allowed age for license class {licenseClass}.", ex);
                throw;
            }
        }

        public static bool UpdateDafaultValidityLength(LicenseClass licenseClass, int newValidityLength)
        {
            string query = "UPDATE LicenseClasses SET DefaultValidityLength = @DefaultValidityLength WHERE LicenseClassId = @LicenseClassId;";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DefaultValidityLength", newValidityLength);
                    command.Parameters.AddWithValue("@LicenseClassId", (int)licenseClass);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while updating default validity length for license class {licenseClass}.", ex);
                throw;
            }
        }

        public static bool UpdateClassFees(LicenseClass licenseClass, decimal newClassFees)
        {
            string query = "UPDATE LicenseClasses SET ClassFees = @ClassFees WHERE LicenseClassId = @LicenseClassId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassFees", newClassFees);
                    command.Parameters.AddWithValue("@LicenseClassId", (int)licenseClass);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while updating class fees for license class {licenseClass}.", ex);
                throw;
            }
        }
    }
}