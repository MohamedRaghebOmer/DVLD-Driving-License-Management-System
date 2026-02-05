using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal static class ApplicationTypeValidator
    {
        public static void AddNewValidator(ApplicationType applicationType)
        {
            Core.Validators.ApplicationTypeValidator.Validate(applicationType);

            if (ApplicationTypeData.IsTitleUsed(applicationType.ApplicationTypeTitle, -1))
                throw new BusinessException("Application Type Title is already used.");
        }

        public static void UpdateValidator(ApplicationType applicationType)
        {
            Core.Validators.ApplicationTypeValidator.Validate(applicationType);

            if (ApplicationTypeData.IsTitleUsed(applicationType.ApplicationTypeTitle, applicationType.ApplicationTypeId))
                throw new BusinessException("Application Type Title is already used.");
        }
    }
}
