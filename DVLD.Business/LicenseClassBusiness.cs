using System;
using DVLD.Data;
using DVLD.Core.Logging;
using DVLD.Core.Exceptions;
using DVLD.Core.DTOs.Enums;

namespace DVLD.Business
{
    public static class LicenseClassBusiness
    {
        public static int GetMinimumAllowedAge(LicenseClass licenseClass)
        {
            if (!Enum.IsDefined(typeof(LicenseClass), licenseClass))
                throw new ValidationException("Undfined license class.");

            try
            {
                return LicenseClassData.GetMinimumAllowedAge(licenseClass);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting minimum allowed age for license class.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static int GetDefaultValidityLength(LicenseClass licenseClass)
        {
            if (!Enum.IsDefined(typeof(LicenseClass), licenseClass))
                throw new ValidationException("Undfined license class.");

            try
            {
                return LicenseClassData.GetDefaultValidityLength(licenseClass);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting default validity length for license class.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static decimal GetClassFees(LicenseClass licenseClass)
        {
            if (!Enum.IsDefined(typeof(LicenseClass), licenseClass))
                throw new ValidationException("Undfined license class.");
            try
            {
                return LicenseClassData.GetClassFees(licenseClass);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting class fees for license class.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool UpdateMinimumAllowedAge(LicenseClass licenseClass, int newMinimumAge)
        {
            if (!Enum.IsDefined(typeof(LicenseClass), licenseClass))
                throw new ValidationException("Undfined license class.");
            if (newMinimumAge < 0)
                throw new ValidationException("Minimum age cannot be negative.");
            try
            {
                return LicenseClassData.UpdateMinimumAllowedAge(licenseClass, newMinimumAge);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while updating minimum allowed age for license class.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool UpdateDefaultValidityLength(LicenseClass licenseClass, int newValidityLength)
        {
            if (!Enum.IsDefined(typeof(LicenseClass), licenseClass))
                throw new ValidationException("Undfined license class.");
            if (newValidityLength < 0)
                throw new ValidationException("Validity length cannot be negative.");
            try
            {
                return LicenseClassData.UpdateDefaultValidityLength(licenseClass, newValidityLength);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while updating default validity length for license class.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool UpdateClassFees(LicenseClass licenseClass, decimal newFees)
        {
            if (!Enum.IsDefined(typeof(LicenseClass), licenseClass))
                throw new ValidationException("Undfined license class.");
            
            if (newFees < 0)
                throw new ValidationException("new Fees cannot be negative.");

            try
            {
                return LicenseClassData.UpdateClassFees(licenseClass, newFees);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while updating class fees for license class.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }
    }
}
