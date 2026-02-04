using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;

namespace DVLD.Business.EntityValidators
{
    internal class TestTypeValidator
    {
        public static void AddNewValidator(TestType testType)
        {
            Core.Validators.TestTypeValidator.Validate(testType);

            if (TestTypeService.IsTestTypeExists(testType.TestTypeTitle))
                throw new ValidationException("The title is already associated with another test type.");
        }

        public static void UpdateValidator(TestType testType)
        {
            Core.Validators.TestTypeValidator.Validate(testType);

            if (TestTypeService.IsTestTypeTitleUsed(testType.TestTypeTitle, testType.TestTypeID))
                throw new ValidationException("The title is already associated with another test type.");
        }
    }
}
