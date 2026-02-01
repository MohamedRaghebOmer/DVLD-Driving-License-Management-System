using System;
using System.Data.SqlClient;
using DVLD.Core.Logging;
using DVLD.Core.DTOs.Entities;

namespace DVLD.Data
{
    public static class CountryService
    {
        // -------------------------Create----------------------
        public static bool AddNewCountry(Country country)
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
                    {
                        country.CountryID = insertedID;
                        return true;
                    }

                    return false;
                }
            }
            catch(Exception ex)
            {
                Core.Logging.AppLogger.LogError("DAL: Error while inserting into Countries table.", ex);
                throw;
            }
        }


        // -------------------------Read------------------------
        public static Country GetCountryInfoByID(int countryID)
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

        public static bool IsCountryExist(int countryID)
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

        public static bool IsCountryExist(string countryName, int excludedId)
        {
            string query = @"SELECT 1 
                            FROM Countries 
                            WHERE CountryName = @countryName AND CountryID != @excludedId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@excludedId", excludedId);
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
        
        // ------------------------Update-----------------------
        public static bool UpdateCountry(Country country)
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
        public static bool DeleteCountry(int countryID)
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
