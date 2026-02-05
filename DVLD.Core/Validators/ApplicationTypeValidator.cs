using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;

namespace DVLD.Core.Validators
{
    public static class ApplicationTypeValidator
    {
        public static void Validate(ApplicationType applicationType)
        {
            if (applicationType == null)
                throw new ValidationException("Application type can't be empty.");

            if (applicationType.ApplicationTypeId <= 0)
                throw new ValidationException("Application Type Id must be greater than zero.");

            if (string.IsNullOrWhiteSpace(applicationType.ApplicationTypeTitle))
                throw new ValidationException("Application Type Title cannot be empty.");

            if (applicationType.ApplicationFees < 0)
                throw new ValidationException("Application Fees cannot be negative.");
        }
    }
}
