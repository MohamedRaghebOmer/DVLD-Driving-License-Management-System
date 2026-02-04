using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using DVLD.Core.DTOs.Entities;

namespace DVLD.Data
{
    public static class UserData
    {
        // ----------------------------Create----------------------------
        public static int AddNew(User user)
        {
            string query = @"INSERT INTO Users (PersonID, Username, Password, IsActive)
                            VALUES (@PersonID, @Username, @Password, @IsActive);
                            SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", user.PersonID);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@IsActive", user.IsActive);

                    connection.Open();

                    object result = command.ExecuteScalar();
                    
                    if (result != null && int.TryParse(result.ToString(), out int newUserID))
                         return newUserID;
                    
                    return -1;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into users.", ex);
                throw;
            }
        }


        // ----------------------------Read----------------------------
        public static User GetByID(int userID)
        {
            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            (
                                Convert.ToInt32(reader["UserID"]),
                                Convert.ToInt32(reader["PersonID"]),
                                reader["Username"].ToString(),
                                reader["Password"].ToString(),
                                Convert.ToBoolean(reader["IsActive"])
                            );
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from users.", ex);
                throw;
            }
        }

        public static User GetByPersonID(int personID)
        {
            string query = "SELECT * FROM Users WHERE PersonID = @PersonID";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            (
                                Convert.ToInt32(reader["UserID"]),
                                Convert.ToInt32(reader["PersonID"]),
                                reader["Username"].ToString(),
                                reader["Password"].ToString(),
                                Convert.ToBoolean(reader["IsActive"])
                            );
                        }   
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from users.", ex);
                throw;
            }
        }

        public static User GetByName(string username)
        {
            string query = "SELECT * FROM Users WHERE LOWER(Username) = LOWER(@Username);";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            (
                                Convert.ToInt32(reader["UserID"]),
                                Convert.ToInt32(reader["PersonID"]),
                                reader["Username"].ToString(),
                                reader["Password"].ToString(),
                                Convert.ToBoolean(reader["IsActive"])
                            );
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from users.", ex);
                throw;
            }
        }

        public static bool IsExists(int UserID)
        {
            string query = "SELECT 1 FROM Users WHERE UserID = @UserID";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existence in users by UserID.", ex);
                throw;
            }
        }

        public static bool IsExists(string Username)
        {
            string query = "SELECT 1 FROM Users WHERE LOWER(Username) = LOWER(@Username)";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existence in users by Username.", ex);
                throw;
            }
        }

        public static bool IsPersonUsed(int personID)
        {
            string query = "SELECT 1 FROM Users WHERE PersonID = @PersonID";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existence in users by PersonID.", ex);
                throw;
            }
        }

        public static bool IsPersonUsed(int userID, int personID)
        {
            string query = "SELECT 1 FROM Users WHERE PersonID = @PersonID AND UserID != @UserID";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);
                    command.Parameters.AddWithValue("@UserID", userID);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existence in users by PersonID for other UserID.", ex);
                throw;
            }
        }

        public static bool IsNameUsed(int userID, string username)
        {
            string query = "SELECT 1 FROM Users WHERE LOWER(Username) = LOWER(@Username) AND UserID != @UserID";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@UserID", userID);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existence in users by Username for other UserID.", ex);
                throw;
            }
        }

        public static bool IsActive(int userID)
        {
            string query = @"SELECT 1 FROM Users WHERE UserID = @userID AND IsActive = 1;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking user activation status.", ex);
                throw;
            }

        }
                
        public static DataTable GetAll()
        {
            string query = "SELECT * FROM Users";

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
                            DataTable usersTable = new DataTable();
                            usersTable.Load(reader);
                            return usersTable;
                        }

                        return null;
                    }
                }

            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting all from users.", ex);
                throw;
            }
        }


        // ----------------------------Update----------------------------
        public static bool Update(User user)
        {
            string query = @"UPDATE Users
                             SET PersonID = @PersonID,
                                 Username = @Username,
                                 Password = @Password,
                                 IsActive = @IsActive
                             WHERE UserID = @UserID";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", user.PersonID);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@IsActive", user.IsActive);
                    command.Parameters.AddWithValue("@UserID", user.UserID);

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while updating users.", ex);
                throw;
            }
        }


        // ----------------------------Delete----------------------------
        public static bool DeleteByUserID(int userID)
        {
            string query = @"DELETE FROM Users WHERE ID = @userID;";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while deleting from Users where UserID = {userID}", ex);
                throw;
            }
        }

        public static bool DeleteByPersonID(int personID)
        {
            string query = @"DELETE FROM Users WHERE PersonID = @personID;";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@personID", personID);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while deleting from Users where person id = {personID}", ex);
                throw;
            }
        }

        public static bool DeleteByUserName(string username)
        {
            string query = "DELETE FROM Users WHERE LOWER(UserName) = LOWER(@username);";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while deleting from Users where username = {username}", ex);
                throw;
            }
        }
    }
}
