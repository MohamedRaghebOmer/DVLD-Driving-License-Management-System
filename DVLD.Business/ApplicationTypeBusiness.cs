using System;
using DVLD.Data;
using DVLD.Core.Logging;
using DVLD.Core.Exceptions;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Business
{
    public static class ApplicationTypeBusiness
    {
        public static decimal GetFees(ApplicationType applicationType)
        {
            try
            {
                return ApplicationTypeData.GetFees(applicationType);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting fees for application type {applicationType}.", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static string[] GetAllApplicationTypeTitles()
        {
            try
            {
                var titles = ApplicationTypeData.GetAllApplicationTypeTitles();
                return titles.ToArray();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while fetching all application type titles.", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool UpdateFees(ApplicationType applicationType, decimal newFees)
        {
            if (newFees < 0)
                throw new ValidationException("Fees cannot be negative.");

            try
            {
                return ApplicationTypeData.UpdateFees(applicationType, newFees);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while updating fees for application type {applicationType}.", ex);
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }
    }
}
