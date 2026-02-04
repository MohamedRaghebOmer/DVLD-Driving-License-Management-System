using System;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;

namespace DVLD.Core.Validators
{
    public static class DriverValidator
    {
        public static void Validate(Driver driver)
        {
            if (driver == null)
                throw new ValidationException("Driver can't be empty.");

            if (driver.DriverID < 1)
                throw new ValidationException("Driver ID can't be negative.");

            if (driver.PersonID < 1)
                throw new ValidationException("Person ID can't be negative.");

            if (driver.CreatedByUserID < 1)
                throw new ValidationException("User ID can't be negative.");

            if (driver.CreatedDate > DateTime.Now)
                throw new ValidationException("Creation date can't be in the future.");

            if (driver.DeletedDate != null && driver.DeletedDate > DateTime.Now)
                throw new ValidationException("Deletion date can't be in the future.");
        }
    }
}
