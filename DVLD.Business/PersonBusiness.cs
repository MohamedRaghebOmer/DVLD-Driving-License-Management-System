using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public static class PersonBusiness
    {
        public static Person Save(Person person)
        {
            // Add new person
            if (person.PersonID == -1)
            {
                PersonValidator.AddNewValidator(person);

                try
                {
                    int newPersonID = PersonData.AddNew(person);
                    
                    if (newPersonID != -1)
                        return PersonData.Get(newPersonID);
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
                        return PersonData.Get(person.PersonID);
                    
                    return null;
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
                return PersonData.Get(personID);
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
                return PersonData.Get(nationalNumber);
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
                return PersonData.GetAll();
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reading all people.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Delete(int personID)
        {
            if (personID < 1 || !PersonData.IsExists(personID))
                return false;

            try
            {
                return PersonData.Delete(personID);
            }
            catch(Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting person with ID = {personID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);

            }
        }

        public static bool Delete(string nationalNumber)
        {
            if (string.IsNullOrWhiteSpace(nationalNumber) || !PersonData.IsExists(nationalNumber))
                return false;

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
