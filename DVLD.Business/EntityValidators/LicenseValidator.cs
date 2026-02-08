using System;
using System.Diagnostics;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal class LicenseValidator
    {
        public static void AddNewValidator(License license)
        {
            Core.Validators.LicenseValidator.Validate(license);

            if (!ApplicationData.Exists(license.ApplicationId))
                throw new BusinessException("Application does not exist.");

            if (!ApplicationData.IsActive(license.ApplicationId))
                throw new BusinessException("Can't add license for inactive application.");

            if (ApplicationData.GetApplicationStatus() == ApplicationStatus.Canceled)
                throw new BusinessException("Can't add license for a canceled application.");

            if (ApplicationData.GetApplicationStatus() == ApplicationStatus.Completed)
                throw new BusinessException("Can't add license for a completed application.");

            if (ApplicationData.GetApplicationType() == ApplicationType.ReleaseDetainedDrivingLicense)
                throw new BusinessException("Invalid application type.");

            if (ApplicationData.GetApplicationType() == ApplicationType.NewInternationalLicense)
                throw new BusinessException("Can't add license for an international application.");

            if (!DriverData.Exists(license.DriverId))
                throw new BusinessException("Driver does not exist.");

            if (LicenseData.DoesDriverHasActiveLicense(license.DriverId, license.LicenseClass))
                throw new BusinessException("Driver already has an active license with the same type.");
        }

        public static void UpdateValidator(License license)
        {
            License storedInfo = LicenseData.GetLicenseById(license.LicenseId);

            Core.Validators.LicenseValidator.Validate(license);
            
            if (!LicenseData.Exists(license.LicenseId))
                throw new BusinessException("License does not exist.");

            if (storedInfo.ApplicationId != license.ApplicationId)
                throw new BusinessException("Can't change the application associated with the license.");

            if (storedInfo.DriverId != license.DriverId)
                throw new BusinessException("Can't change the driver associated with the license.");

            if (storedInfo.LicenseClass != license.LicenseClass)
                throw new BusinessException("Can't change the license class of an existing license.");

            if (storedInfo.IssueDate != license.IssueDate)
                throw new BusinessException("Can't change license issue date.");

            if (storedInfo.ExpirationDate != license.ExpirationDate)
                throw new BusinessException("Can't change license expiration date.");

            if (storedInfo.IssueReason != license.IssueReason)
                throw new BusinessException("Can't change license issue reason.");

            if (storedInfo.CreatedByUserId != license.CreatedByUserId)
                throw new BusinessException("Can't change user created the license.");
        }
    }
}
