using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public static class PersonManager
    {
        public static bool Save(Person person)
        {
            // Add new person
            if (person.PersonID == -1)
            {
                PersonValidator.AddNewValidator(person);

                try
                {
                    return PersonService.AddNew(person);
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
                    return PersonService.Update(person);
                }
                catch(Exception ex)
                {
                    AppLogger.LogError("BLL: Error while saving existing person.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
        }

        public static Person GetPerson(int personID)
        {
            if (personID < 1)
                return null;

            try
            {
                return PersonService.Get(personID);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading person with ID = {personID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Person GetPerson(string nationalNumber)
        {
            if (string.IsNullOrWhiteSpace(nationalNumber))
                return null;

            try
            {
                return PersonService.Get(nationalNumber);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading person with national number = {nationalNumber}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAllPeople()
        {
            try
            {
                return PersonService.GetAll();
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading all people.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Delete(int personID)
        {
            if (personID < 1 || !PersonService.IsExists(personID))
                return false;

            try
            {
                return PersonService.Delete(personID);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting person with ID = {personID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);

            }
        }

        public static bool Delete(string nationalNumber)
        {
            if (string.IsNullOrWhiteSpace(nationalNumber) || !PersonService.IsExists(nationalNumber))
                return false;

            try
            {
                return PersonService.Delete(nationalNumber);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting person with national number = {nationalNumber}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
