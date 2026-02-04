using System;
using DVLD.Data.Settings;
using System.Data.SqlClient;
using DVLD.Core.Logging;
using DVLD.Core.DTOs.Entities;
using System.Data;

namespace DVLD.Data
{
    public static class CountryData
    {
        // -------------------------Create----------------------
        public static int AddNew(Country country)
        {
            string query = @"INSERT INTO Countries(CountryName)
                            VALUES(@countryName)
                            SELECT SCOPE_IDENTITY();";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryName", country.CountryName);
                    
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        return insertedID;

                    return -1;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while inserting into Countries.", ex);
                throw;
            }
        }


        // -------------------------Read------------------------
        public static Country GetCountry(int countryID)
        {
            string query = @"SELECT CountryName
                            FROM Countries
                            WHERE CountryID = @countryID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryID", countryID);
                
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return new Country(countryID, reader.GetString(reader.GetOrdinal("CountryName")));
                        else
                            return null;
                    }
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from Countries table.", ex);
                throw;
            }
        }

        public static bool IsExists(int countryID)
        {
            string query = @"SELECT 1
                            FROM Countries
                            WHERE CountryID = @countryID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryID", countryID);

                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from Countries.", ex);
                throw;
            }
        }

        public static bool IsExists(string countryName)
        {
            string query = @"SELECT 1
                            FROM Countries
                            WHERE LOWER(CountryName) = LOWER(@countryName);";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryName", countryName);

                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from Countries.", ex);
                throw;
            }
        }

        public static bool IsExists(string countryName, int excludedID)
        {
            string query = @"SELECT 1 
                            FROM Countries 
                            WHERE LOEWR(CountryName) = LOWER(@countryName) AND CountryID != @excludedId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@excludedId", excludedID);
                    command.Parameters.AddWithValue("@countryName", countryName);

                    connection.Open();

                    return command.ExecuteScalar() != null;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from Countries.", ex);
                throw;
            }

        }

        public static DataTable GetAll()
        {
            string query = @"SELECT CountryID, CountryName
                            FROM Countries
                            ORDER BY CountryName;";
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable countriesTable = new DataTable();
                        countriesTable.Load(reader);

                        return countriesTable;
                    }
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting all from Countries.", ex);
                throw;
            }
        }


        // ------------------------Update-----------------------
        public static bool Update(Country country)
        {
            string query = @"UPDATE Countries
                            SET CountryName = @newCountryName
                            WHERE CountryID = @countryID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newCountryName", country.CountryName);
                    command.Parameters.AddWithValue("@countryID", country.CountryID);

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while update from Countries.", ex);
                throw;
            }
        }


        // ------------------------Delete-----------------------
        public static bool Delete(int countryID)
        {
            string query = @"DELETE FROM Countries
                            WHERE CountryID = @countryID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryID", countryID);

                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError("DAL: Error while deleting from Countries.", ex);
                throw;
            }
        }
    }
}
