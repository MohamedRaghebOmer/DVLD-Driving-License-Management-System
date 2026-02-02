using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.Logging;
using DVLD.Core.DTOs.Entities;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public static class CountryManager
    {        
        public static Country Find(int id)
        {
            if (id <= 0)
                return null;
             
            try
            {
                return CountryService.GetCountry(id);
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
                return CountryService.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading all countries.", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Save(Country country)
        {
            // Add new country
            if (country.CountryID == -1)
            {
                CountryValidator.AddNewValidator(country);

                try
                {
                    return CountryService.AddNew(country);
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
                    return CountryService.Update(country);

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
            if (countryID < 1 || !CountryService.IsExists(countryID))
                return false;

            try
            {
                return CountryService.Delete(countryID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting country with ID = {countryID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
