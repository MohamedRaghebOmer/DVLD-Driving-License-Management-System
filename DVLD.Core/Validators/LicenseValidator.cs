using System;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;

namespace DVLD.Core.Validators
{
    public static class LicenseValidator
    {
        public static void Validate(License license)
        {
            if (license == null)
                throw new ValidationException("License cannot be empty.");

            if (license.ApplicationId <= 0)
                throw new ValidationException("ApplicationId must be a positive integer.");

            if (license.DriverId <= 0)
                throw new ValidationException("DriverId must be a positive integer.");
            
            if (!Enum.IsDefined(typeof(LicenseClass), license.LicenseClass))
                throw new ValidationException("Invalid LicenseClass value.");
          
            if (license.IssueDate > DateTime.Now)
             
                throw new ValidationException("IssueDate cannot be in the future.");
      
            if (license.ExpirationDate <= license.IssueDate)
                throw new ValidationException("ExpirationDate must be after IssueDate.");
           
            if (license.PaidFees < 0)
                throw new ValidationException("PaidFees cannot be negative.");
        }
    }
}
