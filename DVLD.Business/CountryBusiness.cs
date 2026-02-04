using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.Logging;
using DVLD.Core.DTOs.Entities;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public static class CountryBusiness
    {        
        public static Country Find(int id)
        {
            if (id <= 0)
                return null;
             
            try
            {
                return CountryData.GetCountry(id);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading country info with ID = {id}");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return CountryData.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading all countries.", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Country Save(Country country)
        {
            // Add new country
            if (country.CountryID == -1)
            {
                CountryValidator.AddNewValidator(country);

                try
                {
                    int insertedID = CountryData.AddNew(country);

                    if (insertedID != -1)
                        return CountryData.GetCountry(insertedID);
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"BLL: Error adding new country {country.CountryName}", ex);
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
            else // Update existing country
            {
                CountryValidator.UpdateValidator(country);

                try
                {
                    if (CountryData.Update(country))
                        return CountryData.GetCountry(country.CountryID);
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"BLL: Error updating country {country.CountryName}", ex);
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }

            }
        }
        
        public static bool Delete(int countryID)
        {
            if (countryID < 1 || !CountryData.IsExists(countryID))
                return false;

            try
            {
                return CountryData.Delete(countryID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting country with ID = {countryID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
