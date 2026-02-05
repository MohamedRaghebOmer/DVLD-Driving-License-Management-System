using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Data.Settings;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;

namespace DVLD.Data
{
    public static class ApplicationTypeData
    {
        // --------------------------Create--------------------------
        public static int Add(ApplicationType type)
        {
            string query = @"INSERT INTO ApplicationTypes(ApplicationTypeTitle, ApplicationTypeFees)
                            VALUES(@title, @fees); SELECT SCOPE_IdENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", type.ApplicationTypeTitle);
                    command.Parameters.AddWithValue("@fees", type.ApplicationFees);
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int newId))
                        return newId;

                    return -1;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into ApplicationTypes.", ex);
                throw;
            }
        }


        // --------------------------Read--------------------------
        public static ApplicationType Get(int applicationTypeId)
        {
            string query = @"SELECT *
                            FROM ApplicationTypes
                            WHERE ApplicationTypeId = @id";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", applicationTypeId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ApplicationType
                            (
                                applicationTypeId: Convert.ToInt32(reader["ApplicationTypeId"]),
                                applicationTypeTitle: reader["ApplicationTypeTitle"].ToString(),
                                applicationFees: Convert.ToDecimal(reader["ApplicationTypeFees"])
                            );
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving from ApplicationTypes.", ex);
                throw;
            }
        }

        public static ApplicationType Get(string applicationTypeTitle)
        {
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeTitle = @title;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", applicationTypeTitle);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ApplicationType
                            (
                                applicationTypeId: Convert.ToInt32(reader["ApplicationTypeId"]),
                                applicationTypeTitle: reader["ApplicationTypeTitle"].ToString(),
                                applicationFees: Convert.ToDecimal(reader["ApplicationTypeFees"])
                            );
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving from ApplicationTypes.", ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            string query = "SELECT * FROM ApplicationTypes;";

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
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            return dt;
                        }
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving from ApplicationTypes.", ex);
                throw;
            }
        }

        public static bool Exists(int applicationTypeId)
        {
            string query = "SELECT 1 FROM ApplicationTypes WHERE ApplicationTypeId = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", applicationTypeId);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existance form ApplicationTypes.", ex);
                throw;
            }
        }

        public static bool IsTitleUsed(string applicationTypeTitle, int excludedApplicationTypeId)
        {
            string query = "SELECT 1 FROM ApplicationTypes WHERE LOWER(ApplicationTypeTitle) = LOWER(@title) AND ApplicationTypeId != @excludedId";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", applicationTypeTitle);
                    command.Parameters.AddWithValue("@excludedId", excludedApplicationTypeId);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existance form ApplicationTypes.", ex);
                throw;
            }


        }

            
        // -------------------------Update---------------------------
        public static bool Update(ApplicationType applicationType)
        {
            string query = @"UPDATE ApplicationTypes
                           SET ApplicationTypeTitle = @newTitle, ApplicationTypeFees = @newFees
                           WHERE ApplicationTypeId = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newTitle", applicationType.ApplicationTypeTitle);
                    command.Parameters.AddWithValue("@newFees", applicationType.ApplicationFees);
                    command.Parameters.AddWithValue("@id", applicationType.ApplicationTypeId);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while updating ApplicationTypes.", ex);
                throw;
            }


        }


        // ------------------------Delete----------------------------
        public static bool Delete(int applicationTypeId)
        {
            string query = @"DELETE FROM ApplicationTypes WHERE ApplicationTypeId = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", applicationTypeId);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while deleteing form ApplicationTypes.", ex);
                throw;
            }


        }

        public static bool Delete(string applicationTypeTitle)
        {
            string query = "DELETE FROM ApplicationTypes WHERE ApplicationTypeTitle = @title;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", applicationTypeTitle);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while deleting form form ApplicationTypes.", ex);
                throw;
            }


        }
    }
}
