using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Core.Logging;
using DVLD.Data.Settings;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Data
{
    public static class PersonService
    {
        // -----------------------Create------------------------
        public static bool AddNew(Person person)
        {
            string query = @"INSERT INTO People(NationalNo, FirstName, SecondName,
                            ThirdName, LastName, DateOfBirth, Gender, Address, 
                            Phone, Email, NationalityCountryID, ImagePath)
                            VALUES
                            (@NationalNo, @FirstName, @SecondName,
                            @ThirdName, @LastName, @DateOfBirth,
                            @Gender, @Address, @Phone, @Email,
                            @NationalityCountryID, @ImagePath)
                            SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", person.NationalNumber);
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@SecondName", person.SecondName);
                    if (!string.IsNullOrEmpty(person.ThirdName))
                        command.Parameters.AddWithValue("@ThirdName", person.ThirdName);
                    else
                        command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", person.Gender);
                    command.Parameters.AddWithValue("@Address", person.Address);
                    command.Parameters.AddWithValue("@Phone", person.Phone);
                    if (!string.IsNullOrEmpty(person.Email))
                        command.Parameters.AddWithValue("@Email", person.Email);
                    else
                        command.Parameters.AddWithValue("@Email", DBNull.Value);
                    command.Parameters.AddWithValue("@NationalityCountryID", person.NationalityCountryID);
                    if (!string.IsNullOrEmpty(person.ImagePath))
                        command.Parameters.AddWithValue("@ImagePath", person.ImagePath);
                    else
                        command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

                    connection.Open();
                    
                    object result = command.ExecuteScalar();

                    if (result != null)
                        person.PersonID = Convert.ToInt32(result);

                    return person.PersonID != -1;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into People." + ex);
                throw;
            }
        }


        // ----------------------Read---------------------------
        public static Person Get(int personID)
        {
            string query = @"SELECT * FROM People WHERE PersonID = @PersonID;";

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
                            return new Person(
                                (int)reader["PersonID"],
                                (string)reader["NationalNo"],
                                (string)reader["FirstName"],
                                (string)reader["SecondName"],
                                reader["ThirdName"] == DBNull.Value ? string.Empty : (string)reader["ThirdName"],
                                (string)reader["LastName"],
                                (DateTime)reader["DateOfBirth"],
                                (Gender)(byte)reader["Gendor"],
                                (string)reader["Address"],
                                (string)reader["Phone"],
                                reader["Email"] == DBNull.Value ? string.Empty : (string)reader["Email"],
                                (int)reader["NationalityCountryID"],
                                reader["ImagePath"] == DBNull.Value ? string.Empty : (string)reader["ImagePath"]
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while reading from People." + ex);
                throw;
            }
            return null;
        }

        public static Person Get(string nationalNumber)
        {
            string query = @"SELECT * FROM People WHERE NationalNo = @nationalNumber;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nationalNumber", nationalNumber);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Person(
                                (int)reader["PersonID"],
                                (string)reader["NationalNo"],
                                (string)reader["FirstName"],
                                (string)reader["SecondName"],
                                reader["ThirdName"] == DBNull.Value ? string.Empty : (string)reader["ThirdName"],
                                (string)reader["LastName"],
                                (DateTime)reader["DateOfBirth"],
                                (Gender)(byte)reader["Gendor"],
                                (string)reader["Address"],
                                (string)reader["Phone"],
                                reader["Email"] == DBNull.Value ? string.Empty : (string)reader["Email"],
                                (int)reader["NationalityCountryID"],
                                reader["ImagePath"] == DBNull.Value ? string.Empty : (string)reader["ImagePath"]
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while reading from People." + ex);
                throw;
            }
            return null;
        }

        public static bool IsExists(int personID)
        {
            string query = "SELECT 1 FROM People WHERE PersonID = @personID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query , connection))
                {
                    command.Parameters.AddWithValue("@personID", personID);
                    
                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while reading from People." + ex);
                throw;
            }
        }

        public static bool IsExists(string nationalNumber)
        {
            string query = "SELECT 1 FROM People WHERE NationalNo = @nationalNumber;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nationalNumber", nationalNumber);

                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while reading from People." + ex);
                throw;
            }
        }

        public static bool IsExists(string nationalNumber, int excludedID)
        {
            string query = "SELECT 1 FROM People WHERE NationalNo = @nationalNumber AND PersonID != @excludedID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nationalNumber", nationalNumber);
                    command.Parameters.AddWithValue("@excludedID", excludedID);

                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while reading from People." + ex);
                throw;
            }
        }

        public static bool IsEmailExists(string email)
        {
            string query = "SELECT 1 FROM People WHERE Email = @Email;";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while reading from People." + ex);
                throw;
            }
        }

        public static DataTable GetAll()
        {
            string query = "SELECT * FROM People;";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            DataTable allPeople = new DataTable();
                            allPeople.Load(reader);
                            
                            return allPeople;
                        }
                        else
                            return null;

                    }
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while reading from People." + ex);
                throw;
            }
        }

        // ----------------------Update-------------------------
        public static bool Update(Person person)
        {
            string query = @"UPDATE People 
                             SET NationalNo = @NationalNo, 
                                 FirstName = @FirstName, 
                                 SecondName = @SecondName,
                                 ThirdName = @ThirdName, 
                                 LastName = @LastName, 
                                 DateOfBirth = @DateOfBirth, 
                                 Gender = @Gender, 
                                 Address = @Address, 
                                 Phone = @Phone, 
                                 Email = @Email, 
                                 NationalityCountryID = @NationalityCountryID, 
                                 ImagePath = @ImagePath
                             WHERE PersonID = @PersonID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", person.NationalNumber);
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@SecondName", person.SecondName);
                    if (!string.IsNullOrEmpty(person.ThirdName))
                        command.Parameters.AddWithValue("@ThirdName", person.ThirdName);
                    else
                        command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", person.Gender);
                    command.Parameters.AddWithValue("@Address", person.Address);
                    command.Parameters.AddWithValue("@Phone", person.Phone);
                    if (!string.IsNullOrEmpty(person.Email))
                        command.Parameters.AddWithValue("@Email", person.Email);
                    else
                        command.Parameters.AddWithValue("@Email", DBNull.Value);
                    command.Parameters.AddWithValue("@NationalityCountryID", person.NationalityCountryID);
                    if (!string.IsNullOrEmpty(person.ImagePath))
                        command.Parameters.AddWithValue("@ImagePath", person.ImagePath);
                    else
                        command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
                    command.Parameters.AddWithValue("@PersonID", person.PersonID);

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while updating from People." + ex);
                throw;
            }
        }


        // ---------------------Delete--------------------------
        public static bool Delete(int personID)
        {
            string query = @"DELETE FROM People WHERE PersonID = @personID;";

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
                AppLogger.LogError("DAL: Error while deleting from People." + ex);
                throw;
            }
        }

        public static bool Delete(string nationalNumber)
        {
            string query = @"DELETE FROM People WHERE NationalNo = @nationalNumber;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nationalNumber", nationalNumber);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while deleting from People." + ex);
                throw;
            }
        }

    }
}
