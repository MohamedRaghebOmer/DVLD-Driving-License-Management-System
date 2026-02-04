using System;
using System.Data.SqlClient;
using DVLD.Data.Settings;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using System.Data;

namespace DVLD.Data
{
    public static class TestTypeService
    {
        // ---------------------------Create----------------------------
        public static bool AddNewTestType(TestType testType)
        {
            string qurey = @"INSERT INTO TestTypes(TestTypeTitle, TestTypeDescription, TestTypeFees)
                            VALUES (@title, @description, @fees); SELECT SCOPE_IDENTITY();";

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

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        testType.TestTypeID = insertedID;
                        return insertedID != -1;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into TestTypes.", ex);
                throw;
            }
        }


        // ---------------------------Read------------------------------
        public static TestType GetTestTypeByID(int testTypeID)
        {
            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", testTypeID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TestType testType = new TestType
                            {
                                TestTypeID = reader.GetInt32(0),
                                TestTypeTitle = reader.GetString(1),
                                TestTypeDescription = reader.GetString(2),
                                TestTypeFees = reader.GetDecimal(3)
                            };

                            return testType;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while retrieving TestType by ID.", ex);
                throw;
            }
        }

        public static TestType GetTestTypeByTitle(string testTypeTitle)
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
                            {
                                TestTypeID = Convert.ToInt32(reader["TestTypeID"]),
                                TestTypeTitle = reader["TestTypeTitle"].ToString(),
                                TestTypeDescription = reader["TestTypeDescription"].ToString(),
                                TestTypeFees = Convert.ToDecimal(reader["TestTypeFees"])
                            };
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

        public static DataTable GetAllTestTypes()
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

        public static bool IsTestTypeExists(int testTypeID)
        {
            string query = "SELECT 1 FROM TestTypes WHERE TestTypeID = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", testTypeID);
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

        public static bool IsTestTypeExists(string testTypeTitle)
        {
            string query = "SELECT 1 FROM TestTypes WHERE LOWER(TestTypeTitle) = LOWER(@title);";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", testTypeTitle);
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

        public static bool IsTestTypeTitleUsed(string testTypeTitle, int excludedTestTypeID)
        {
            string query = "SELECT 1 FROM TestTypes WHERE TestTypeTitle = @title AND TestTypeID != @id;";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", testTypeTitle);
                    command.Parameters.AddWithValue("@id", excludedTestTypeID);
                    
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
        public static bool UpdateTestType(TestType testType)
        {
            string query = @"UPDATE TestTypes SET TestTypeTitle = @title, TestTypeDescription = @descripition, 
                            TestTypeFees = @fees WHERE TestTypeID = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", testType.TestTypeTitle);
                    command.Parameters.AddWithValue("@descripition", testType.TestTypeDescription);
                    command.Parameters.AddWithValue("@fees", testType.TestTypeFees);
                    command.Parameters.AddWithValue("@id", testType.TestTypeID);

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DLL: Error while updating Drivers where driver id = {testType.TestTypeID}.", ex);
                throw;
            }

        }


        // --------------------------Delete----------------------------
        public static bool DeleteTestType(int testTypeID)
        {
            string query = "DELETE FROM TestTypes WHERE TestTypeID = @id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", testTypeID);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"DAL: Error while deleting from TestTypes where TestTypeID = {testTypeID}", ex);
                throw;
            }
        }

        public static bool DeleteTestType(string testTypeTitle)
        {
            string query = "DELETE FROM TestTypes WHERE TestTypeTitle = @testTypeTitle;";

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
