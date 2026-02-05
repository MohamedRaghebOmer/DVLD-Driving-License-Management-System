using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Business.EntityValidators;
using DVLD.Core.Logging;
using DVLD.Core.Exceptions;

namespace DVLD.Business
{
    public static class ApplicationTypeBusiness
    {
        public static ApplicationType Save(ApplicationType applicationType)
        {
            // Add new application type
            if (applicationType.ApplicationTypeId == -1)
            {
                ApplicationTypeValidator.AddNewValidator(applicationType);

                try
                {
                    int newId = ApplicationTypeData.Add(applicationType);

                    if (newId != -1)
                        return ApplicationTypeData.Get(newId);
                    return null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"BLL: Error whele adding new application type with title = {applicationType.ApplicationTypeTitle}.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
            else // Update existing application type
            {
                try
                {
                    ApplicationTypeValidator.UpdateValidator(applicationType);

                    if (ApplicationTypeData.Update(applicationType))
                        return ApplicationTypeData.Get(applicationType.ApplicationTypeId);

                    return null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"BLL: Error whele updating application type with Id = {applicationType.ApplicationTypeId}.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
        }

        public static ApplicationType Find(int applicationTypeId)
        {
            if (applicationTypeId < 1)
                return null;

            try
            {
                return ApplicationTypeData.Get(applicationTypeId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while retrieving application type with Id = {applicationTypeId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static ApplicationType Find(string applicationTypeTitle)
        {
            if (string.IsNullOrWhiteSpace(applicationTypeTitle))
                return null;

            try
            {
                return ApplicationTypeData.Get(applicationTypeTitle);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while retrieving application type with title = {applicationTypeTitle}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return ApplicationTypeData.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while retrieving all application types.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Exists(int applicationTypeId)
        {
            if (applicationTypeId < 1)
                return false;

            try
            {
                return ApplicationTypeData.Exists(applicationTypeId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking existence of application type with Id = {applicationTypeId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsTitleUsed(string applicationTypeTitle, int excludeId = -1)
        {
            if (string.IsNullOrWhiteSpace(applicationTypeTitle))
                return false;
            try
            {
                return ApplicationTypeData.IsTitleUsed(applicationTypeTitle, excludeId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking if title is used for application type with title = {applicationTypeTitle}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Delete(int applicationTypeId)
        {
            if (applicationTypeId < 1 || !Exists(applicationTypeId))
                throw new ValidationException("Invalid application type Id.");

            try
            {
                return ApplicationTypeData.Delete(applicationTypeId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting application type with Id = {applicationTypeId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Delete(string applicationTypeTitle)
        {
            if (string.IsNullOrWhiteSpace(applicationTypeTitle) || !ApplicationTypeData.IsTitleUsed(applicationTypeTitle, -1))
                throw new ValidationException("Invalid application type title.");

            try
            {
                return ApplicationTypeData.Delete(applicationTypeTitle);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting application type with title = {applicationTypeTitle}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
