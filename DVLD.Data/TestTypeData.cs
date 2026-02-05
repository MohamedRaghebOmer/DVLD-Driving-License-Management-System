using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Data.Settings;
using DVLD.Core.Logging;
using DVLD.Core.DTOs.Entities;

namespace DVLD.Data
{
    public static class TestTypeData
    {
        // ---------------------------Create----------------------------
        public static int Add(TestType testType)
        {
            string qurey = @"INSERT INTO TestTypes(TestTypeTitle, TestTypeDescription, TestTypeFees)
                            VALUES (@title, @description, @fees); SELECT SCOPE_IdENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(qurey, connection))
                {
                    command.Parameters.AddWithValue("@title", testType.TestTypeTitle);
                    command.Parameters.AddWithValue("@description", testType.TestTypeDescription);
                    command.Parameters.AddWithValue("@fees", testType.TestTypeFees);
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedId))
                        return insertedId;

                    return -1;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into TestTypes.", ex);
                throw;
            }
        }


        // ---------------------------Read------------------------------
        public static TestType Get(int testTypeId)
        {
            string query = "SELECT * FROM TestTypes WHERE TestTypeId = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", testTypeId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TestType testType = new TestType
                            (
                                Convert.ToInt32(reader["TestTypeId"]),
                                reader["TestTypeTitle"].ToString(),
                                reader["TestTypeDescription"] == DBNull.Value ? string.Empty : reader["TestTypeDescription"].ToString(),
                                Convert.ToDecimal(reader["TestTypeFees"])
                            );

                            return testType;
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving TestType by Id.", ex);
                throw;
            }
        }

        public static TestType Get(string testTypeTitle)
        {
            string query = "SELECT * FORM TestTypes WHERE LOWER(TestTypeTitle) = LOWER(@title);";

            using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@title", testTypeTitle);
                connection.Open();

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TestType
                            (
                                Convert.ToInt32(reader["TestTypeId"]),
                                reader["TestTypeTitle"].ToString(),
                                reader["TestTypeDescription"] == DBNull.Value ? string.Empty : reader["TestTypeDescription"].ToString(),
                                Convert.ToDecimal(reader["TestTypeFees"])
                            );
                        }

                        return null;
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("DAL: Error while retrieving TestType by title.", ex);
                    throw;
                }
            }
        }

        public static DataTable GetAll()
        {
            string query = "SELECT * FROM TestTypes;";

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

                    return null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving all TestTypes.", ex);
                throw;
            }
        }

        public static bool Exists(int testTypeId)
        {
            string query = "SELECT 1 FROM TestTypes WHERE TestTypeId = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", testTypeId);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting form TestTypes.", ex);
                throw;
            }
        }

        public static bool IsTitleUsed(string testTypeTitle, int excludedTestTypeId)
        {
            string query = "SELECT 1 FROM TestTypes WHERE LOWER(TestTypeTitle) = LOWER(@title) AND TestTypeId != @id;";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", testTypeTitle);
                    command.Parameters.AddWithValue("@id", excludedTestTypeId);
                    
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting form TestTypes.", ex);
                throw;
            }
        }


        // --------------------------Update-----------------------------
        public static bool Update(TestType testType)
        {
            string query = @"UPDATE TestTypes SET TestTypeTitle = @title, TestTypeDescription = @descripition, 
                            TestTypeFees = @fees WHERE TestTypeId = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", testType.TestTypeTitle);
                    command.Parameters.AddWithValue("@descripition", testType.TestTypeDescription);
                    command.Parameters.AddWithValue("@fees", testType.TestTypeFees);
                    command.Parameters.AddWithValue("@id", testType.TestTypeId);

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DLL: Error while updating Drivers where driver id = {testType.TestTypeId}.", ex);
                throw;
            }

        }


        // --------------------------Delete----------------------------
        public static bool Delete(int testTypeId)
        {
            string query = "DELETE FROM TestTypes WHERE TestTypeId = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", testTypeId);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"DAL: Error while deleting from TestTypes where TestTypeId = {testTypeId}", ex);
                throw;
            }
        }

        public static bool Delete(string testTypeTitle)
        {
            string query = "DELETE FROM TestTypes WHERE LOWER(TestTypeTitle) = LOWER(@testTypeTitle);";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testTypeTitle", testTypeTitle);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while deleting from TestTypes where TestTypeTitle = {testTypeTitle}", ex);
                throw;
            }
        }

    }
}
