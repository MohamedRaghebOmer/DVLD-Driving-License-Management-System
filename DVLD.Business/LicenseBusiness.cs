using System;
using System.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Core.Logging;
using DVLD.Data;

namespace DVLD.Business
{
    public static class LicenseBusiness
    {
        public static License Save(License license)
        {
            // Add new license
            if (license.LicenseId == -1)
            {
                EntityValidators.LicenseValidator.AddNewValidator(license);
                license.IssueDate = DateTime.Now;
                license.IsActive = true;
                license.ExpirationDate = license.IssueDate.AddYears(LicenseClassData.GetDefaultValidityLength(license.LicenseClass));

                try
                {
                    int newLicenseId = LicenseData.Add(license);
                    return LicenseData.GetLicenseById(newLicenseId);
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("BLL: Error adding new license.");
                    throw new Exception("An error occurred while adding the license. Please try again later.", ex);
                }
            }
            else // Update existing license
            {
                EntityValidators.LicenseValidator.UpdateValidator(license);

                try
                {
                    bool isUpdated = LicenseData.UpdateLicense(license);
                    return isUpdated ? LicenseData.GetLicenseById(license.LicenseId) : null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("BLL: Error updating license.");
                    throw new Exception("An error occurred while updating the license. Please try again later.", ex);
                }
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return LicenseData.GetAll();

            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while getting all licenses.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static License GetById(int licenseId)
        {
            try
            {
                return LicenseData.GetLicenseById(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting license with ID {licenseId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static DataTable GetDriverLicenses(int driverId)
        {
            if (driverId <= 0)
                throw new ValidationException("Invalid Driver ID. It must be a positive integer.");

            try
            {
                return LicenseData.GetDriverLicenses(driverId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while getting licenses for DriverID {driverId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool DeactivateLicense(int licenseId)
        {
            if (licenseId <= 0)
                throw new ValidationException("Invalid License ID. It must be a positive integer.");
            try
            {
                return LicenseData.DeactivateLicense(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deactivating license with ID {licenseId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool ReactivateLicense(int licenseId)
        {
            if (licenseId <= 0)
                throw new ValidationException("Invalid License ID. It must be a positive integer.");
            try
            {
                return LicenseData.ReactivateLicense(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while reactivating license with ID {licenseId}.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }

        public static bool IsActive(int licenseId)
        {
            if (licenseId <= 0)
                throw new ValidationException("Invalid license id, it must be a positive integer.");

            try
            {
                return LicenseData.IsActive(licenseId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while checking if license with ID {licenseId} is active.", ex);
                throw new Exception("We encountered a technical issue, please try again later.");
            }
        }
    }
}
