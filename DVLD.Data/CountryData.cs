using System;
using System.Data;
using System.Data.SqlClient;
using DVLD.Data.Settings;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;

namespace DVLD.Data
{
    public static class CountryData
    {
        // -------------------------Create----------------------
        public static int Add(Country country)
        {
            string query = @"INSERT INTO Countries(CountryName)
                            VALUES(@countryName)
                            SELECT SCOPE_IdENTITY();";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryName", country.CountryName);
                    
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedId))
                        return insertedId;

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
        public static Country Get(int countryId)
        {
            string query = @"SELECT CountryName
                            FROM Countries
                            WHERE CountryId = @countryId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryId", countryId);
                
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return new Country(countryId, reader.GetString(reader.GetOrdinal("CountryName")));
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

        public static Country Get(string countryName)
        {
            string query = @"SELECT CountryId, CountryName
                            FROM Countries
                            WHERE LOWER(CountryName) = LOWER(@countryName);";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryName", countryName);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return new Country(reader.GetInt32(reader.GetOrdinal("CountryId")), reader.GetString(reader.GetOrdinal("CountryName")));
                        else
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while selecting from Countries table.", ex);
                throw;
            }
        }

        public static bool Exists(int countryId)
        {
            string query = @"SELECT 1
                            FROM Countries
                            WHERE CountryId = @countryId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryId", countryId);

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

        public static bool IsCountryNameUsed(string countryName, int excludedId)
        {
            string query = @"SELECT 1 
                            FROM Countries 
                            WHERE LOEWR(CountryName) = LOWER(@countryName) AND CountryId != @excludedId;";

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

        public static DataTable GetAll()
        {
            string query = @"SELECT CountryId, CountryName
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
                            WHERE CountryId = @countryId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newCountryName", country.CountryName);
                    command.Parameters.AddWithValue("@countryId", country.CountryId);

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
        public static bool Delete(int countryId)
        {
            string query = @"DELETE FROM Countries
                            WHERE CountryId = @countryId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryId", countryId);

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

        public static bool Delete(string countryName)
        {
            string query = @"DELETE FROM Countries
                            WHERE LOWER(CountryName) = LOWER(@countryName);";

            try
            {
                using (SqlConnection connection = new SqlConnection(DataSettings.connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@countryName", countryName);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("DAL: Error while deleting from Countries.", ex);
                throw;
            }
        }

    }
}
