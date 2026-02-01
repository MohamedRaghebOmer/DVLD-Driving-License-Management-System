using System;
using DVLD.Data;
using DVLD.Core.Logging;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;

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
                return CountryService.GetCountryInfoByID(id);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading country info with ID = {id}");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Save(Country country)
        {
            if (country.CountryName.Length < 3)
                throw new BusinessException("Country name must be at least 3 characters long.");

            if (CountryService.IsCountryExist(country.CountryName, country.CountryID))
            {
                throw new BusinessException($"The country name '{country.CountryName}' is already taken by another record.");
            }

            try
            {
                if (country.CountryID == -1)
                    return CountryService.AddNewCountry(country);
                else
                    return CountryService.UpdateCountry(country);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error saving country {country.CountryName}", ex);
                throw new Exception("A technical error occurred while saving. Please try again later.", ex);
            }
        }
        
        public static bool Delete(int countryID)
        {
            if (countryID <= 0)
                return false;

            if (!CountryService.IsCountryExist(countryID))
                return false;

            try
            {
                return CountryService.DeleteCountry(countryID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting country with ID = {countryID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
