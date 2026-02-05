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
        public static int Add(User user)
        {
            string query = @"INSERT INTO Users (PersonId, Username, Password, IsActive)
                            VALUES (@PersonId, @Username, @Password, @IsActive);
                            SELECT SCOPE_IdENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonId", user.PersonId);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@IsActive", user.IsActive);

                    connection.Open();

                    object result = command.ExecuteScalar();
                    
                    if (result != null && int.TryParse(result.ToString(), out int newUserId))
                         return newUserId;
                    
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
        public static User GetByUsersId(int userId)
        {
            string query = "SELECT * FROM Users WHERE UserId = @UserId";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            (
                                Convert.ToInt32(reader["UserId"]),
                                Convert.ToInt32(reader["PersonId"]),
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

        public static User GetByPersonId(int personId)
        {
            string query = "SELECT * FROM Users WHERE PersonId = @PersonId";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonId", personId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            (
                                Convert.ToInt32(reader["UserId"]),
                                Convert.ToInt32(reader["PersonId"]),
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

        public static User GetByUsername(string username)
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
                                Convert.ToInt32(reader["UserId"]),
                                Convert.ToInt32(reader["PersonId"]),
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
        
        public static bool Exists(int userId)
        {
            string query = "SELECT 1 FROM Users WHERE UserId = @UserId";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existence in users by UserId.", ex);
                throw;
            }
        }

        public static bool IsPersonUsed(int personId, int excluedUserId)
        {
            string query = "SELECT 1 FROM Users WHERE PersonId = @PersonId AND UserId != @UserId";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonId", excluedUserId);
                    command.Parameters.AddWithValue("@UserId", personId);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existence in users by PersonId for other UserId.", ex);
                throw;
            }
        }

        public static bool IsUsernameUsed(string username, int excluedUserId)
        {
            string query = "SELECT 1 FROM Users WHERE LOWER(Username) = LOWER(@Username) AND UserId != @UserId";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", excluedUserId);
                    command.Parameters.AddWithValue("@UserId", username);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while checking existence in users by Username for other UserId.", ex);
                throw;
            }
        }

        public static bool IsActive(int userId)
        {
            string query = @"SELECT 1 FROM Users WHERE UserId = @userId AND IsActive = 1;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
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
                             SET PersonId = @PersonId,
                                 Username = @Username,
                                 Password = @Password,
                                 IsActive = @IsActive
                             WHERE UserId = @UserId";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonId", user.PersonId);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@IsActive", user.IsActive);
                    command.Parameters.AddWithValue("@UserId", user.UserId);

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
        public static bool DeleteByUserId(int userId)
        {
            string query = @"DELETE FROM Users WHERE Id = @userId;";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"DAL: Error while deleting from Users where UserId = {userId}", ex);
                throw;
            }
        }

        public static bool DeleteByPersonId(int personId)
        {
            string query = @"DELETE FROM Users WHERE PersonId = @personId;";
            
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
                AppLogger.LogError($"DAL: Error while deleting from Users where person id = {personId}", ex);
                throw;
            }
        }

        public static bool DeleteByUsername(string username)
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
