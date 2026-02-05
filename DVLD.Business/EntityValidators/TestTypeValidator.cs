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

            if (TestTypeData.IsTitleUsed(testType.TestTypeTitle, -1))
                throw new BusinessException("The title is already associated with another test type.");
        }

        public static void UpdateValidator(TestType testType)
        {
            Core.Validators.TestTypeValidator.Validate(testType);

            if (TestTypeData.IsTitleUsed(testType.TestTypeTitle, testType.TestTypeId))
                throw new BusinessException("The title is already associated with another test type.");
        }
    }
}
