using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using System;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Data
{
    public static class LicenseData
    {
        public static int Add(License license)
        {
            string query = @"INSERT INTO Licenses (ApplicationID, DriverID, LicenseClass, 
                            IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, 
                            CreatedByUserID) 
                            VALUES (@applicationID, @driverID, @licenseClass, @issueDate, 
                            @expirationDate, @notes, @paidFees, @isActive, @issueReason, 
                            @createdByUserID);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@applicationID", license.ApplicationId);
                    command.Parameters.AddWithValue("@driverID", license.DriverId);
                    command.Parameters.AddWithValue("@licenseClass", (int)license.LicenseClass);
                    command.Parameters.AddWithValue("@issueDate", license.IssueDate);
                    command.Parameters.AddWithValue("@expirationDate", license.ExpirationDate);
                    command.Parameters.AddWithValue("@notes", license.Notes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@paidFees", license.PaidFees);
                    command.Parameters.AddWithValue("@isActive", license.IsActive);
                    command.Parameters.AddWithValue("@issueReason", (int)license.IssueReason);
                    command.Parameters.AddWithValue("@createdByUserID", license.CreatedByUserId);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int newLicenseId))
                        return newLicenseId;
                }

                return -1; // Return -1 if the insert operation fails
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while adding a new license.", ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            string query = "SELECT * FROM Licenses;";

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
                AppLogger.LogError("DAL: Error while fetching all licenses.", ex);
                throw;
            }
        }

        public static DataTable GetByDriverId(int driverId)
        {
            string query = "SELECT * FROM Licenses WHERE DriverID = @DriverID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driverId);
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
                AppLogger.LogError($"DAL: Error while fetching licenses for DriverID {driverId}.", ex);
                throw;
            }
        }

        public static License GetLicenseById(int licenseId)
        {
            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", licenseId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new License
                            (
                                licenseId: Convert.ToInt32(reader["LicenseID"]),
                                applicationId: Convert.ToInt32(reader["ApplicationID"]),
                                driverId: Convert.ToInt32(reader["DriverID"]),
                                licenseClass: (LicenseClass)Convert.ToInt32(reader["LicenseClass"]),
                                issueDate: Convert.ToDateTime(reader["IssueDate"]),
                                expirationDate: Convert.ToDateTime(reader["ExpirationDate"]),
                                notes: reader["Notes"] != null ? reader["Notes"].ToString() : string.Empty,
                                paidFees: Convert.ToDecimal(reader["PaidFees"]),
                                isActive: Convert.ToBoolean(reader["IsActive"]),
                                issueReason: (IssueReason)Convert.ToInt32(reader["IssueReason"]),
                                createdByUserId: Convert.ToInt32(reader["CreatedByUserID"])
                            );
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching license with ID {licenseId}.", ex);
                throw;
            }
        }

        public static License GetByAppilcationId(int appilcationId)
        {
            string query = "SELECT * FROM Licenses WHERE ApplicationID = @ApplicationID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", appilcationId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new License
                            (
                                licenseId: Convert.ToInt32(reader["LicenseID"]),
                                applicationId: Convert.ToInt32(reader["ApplicationID"]),
                                driverId: Convert.ToInt32(reader["DriverID"]),
                                licenseClass: (LicenseClass)Convert.ToInt32(reader["LicenseClass"]),
                                issueDate: Convert.ToDateTime(reader["IssueDate"]),
                                expirationDate: Convert.ToDateTime(reader["ExpirationDate"]),
                                notes: reader["Notes"] != null ? reader["Notes"].ToString() : string.Empty,
                                paidFees: Convert.ToDecimal(reader["PaidFees"]),
                                isActive: Convert.ToBoolean(reader["IsActive"]),
                                issueReason: (IssueReason)Convert.ToInt32(reader["IssueReason"]),
                                createdByUserId: Convert.ToInt32(reader["CreatedByUserID"])
                            );
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while fetching license with ApplicationID {appilcationId}.", ex);
                throw;
            }
        }

        public static bool Exists(int licenseId)
        {
            string query = @"SELECT 1 FROM Licenses WHERE LicenseID = @licenseId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@licenseId", licenseId);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while checking existence of license with ID {licenseId}.", ex);
                throw;
            }
        }

        public static bool DoesApplicationIdExits(int applicationId)
        {
            string query = @"SELECT 1 FROM Licenses WHERE ApplicationID = @applicationId;";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@applicationId", applicationId);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while checking existence of license with ApplicationID {applicationId}.", ex);
                throw;
            }
        }

        public static bool DoesDriverHasActiveLicense(int driverId, LicenseClass licenseClass)
        {
            string query = @"SELECT 1 FROM Licenses WHERE DriverID = @Id AND LicenseClass = @class AND IsActive = 1 AND ExpirationDate > @todayDate;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", driverId);
                    command.Parameters.AddWithValue("@class", (int)licenseClass);
                    command.Parameters.AddWithValue("@todayDate", DateTime.Now);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while license with driver id = {driverId}.", ex);
                throw;
            }
        }

        public static bool IsActive(int licneseId)
        {
            string query = @"SELECT IsActive FROM Licenses WHERE LicenseID = @licenseId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@licenseId", licneseId);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && bool.TryParse(result.ToString(), out bool isActive))
                        return isActive;
                }
                return false; // Return false if the license is not found or if the value is invalid
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while checking if license with ID {licneseId} is active.", ex);
                throw;
            }
        }
    }
}
