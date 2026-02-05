using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;
using DVLD.Core.Exceptions;

namespace DVLD.Business
{
    public static class PersonBusiness
    {
        public static Person Save(Person person)
        {
            // Add new person
            if (person.PersonId == -1)
            {
                PersonValidator.AddNewValidator(person);

                try
                {
                    int newPersonId = PersonData.Add(person);
                    
                    if (newPersonId != -1)
                        return PersonData.Get(newPersonId);
                    return null;
                }
                catch(Exception ex)
                {
                    AppLogger.LogError("BLL: Error while saving new person.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
            else // Update existing person
            {
                PersonValidator.UpdateValidator(person);
                
                try
                {
                    if (PersonData.Update(person))
                        return PersonData.Get(person.PersonId);
                    
                    return null;
                }
                catch(Exception ex)
                {
                    AppLogger.LogError("BLL: Error while saving existing person.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
        }

        public static Person Find(int personId)
        {
            if (personId < 1)
                return null;

            try
            {
                return PersonData.Get(personId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading person with Id = {personId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Person Find(string nationalNumber)
        {
            if (string.IsNullOrWhiteSpace(nationalNumber))
                return null;

            try
            {
                return PersonData.Get(nationalNumber);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading person with national number = {nationalNumber}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return PersonData.GetAll();
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading all people.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Exists(int personId)
        {
            if (personId < 1)
                return false;
            try
            {
                return PersonData.Exists(personId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking existence of person with Id = {personId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsNationalNumberUsed(string nationalNumber, int excludePersonId)
        {
            if (string.IsNullOrWhiteSpace(nationalNumber))
                return false;
            try
            {
                return PersonData.IsNationalNumberUsed(nationalNumber, excludePersonId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking if national number = {nationalNumber} is used.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool EmailUsedByOther(string email, int excludePersonId)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                return PersonData.IsEmailUsed(email, excludePersonId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking if email = {email} is used by other.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Delete(int personId)
        {
            if (personId < 1 || !PersonData.Exists(personId))
                throw new ValidationException("Person not found.");

            try
            {
                return PersonData.Delete(personId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting person with Id = {personId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);

            }
        }

        public static bool Delete(string nationalNumber)
        {
            if (string.IsNullOrWhiteSpace(nationalNumber) || !PersonData.IsNationalNumberUsed(nationalNumber, -1))
                throw new ValidationException("Person not found.");

            try
            {
                return PersonData.Delete(nationalNumber);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting person with national number = {nationalNumber}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
