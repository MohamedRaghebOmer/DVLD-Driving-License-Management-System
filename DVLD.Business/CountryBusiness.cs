using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.Logging;
using DVLD.Core.DTOs.Entities;
using DVLD.Business.EntityValidators;
using DVLD.Core.Exceptions;

namespace DVLD.Business
{
    public static class CountryBusiness
    {        
        public static Country Save(Country country)
        {
            // Add new country
            if (country.CountryId == -1)
            {
                CountryValidator.AddNewValidator(country);

                try
                {
                    int insertedId = CountryData.Add(country);

                    if (insertedId != -1)
                        return CountryData.Get(insertedId);
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
                        return CountryData.Get(country.CountryId);
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

        public static Country Find(int id)
        {
            if (id <= 0)
                return null;
             
            try
            {
                return CountryData.Get(id);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading country info with Id = {id}");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Country Find(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return null;

            try
            {
                return CountryData.Get(countryName);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading country info with name = {countryName}");
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

        public static bool Exits(int countryId)
        {
            if (countryId < 1)
                return false;

            try
            {
                return CountryData.Exists(countryId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking country with id = {countryId} existance.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsCountryNameUsed(string countryName, int excludedId)
        {
            if (string.IsNullOrWhiteSpace(countryName) || excludedId < 1)
                return false;
            try
            {
                return CountryData.IsCountryNameUsed(countryName, excludedId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking country name = {countryName} existance for country with Id = {excludedId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
        
        public static bool Delete(int countryId)
        {
            if (countryId < 1 || !CountryData.Exists(countryId))
                throw new ValidationException("The specified country does not exist.");

            try
            {
                return CountryData.Delete(countryId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting country with Id = {countryId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Delete(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName) || !CountryData.IsCountryNameUsed(countryName, -1))
                throw new ValidationException(countryName + " country does not exist.");

            try
            {
                return CountryData.Delete(countryName);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting country with name = {countryName}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
