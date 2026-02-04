using System;
using System.CodeDom;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;


namespace DVLD.Core.Validators
{
    public static class TestTypeValidator
    {
        public static void Validate(TestType testType)
        {
            if (testType == null)
                throw new ValidationException("Test type can't be empty.");

            if (testType.TestTypeID < 1)
                throw new ValidationException("Test type id must be a positive integer.");

            if (string.IsNullOrWhiteSpace(testType.TestTypeTitle))
                throw new ValidationException("Test type title can't be empty.");

            if (string.IsNullOrWhiteSpace(testType.TestTypeDescription))
                throw new ValidationException("Test type description can't be empty.");

            if (testType.TestTypeFees < 0)
                throw new ValidationException("Test type fees must be a positive number.");
        }
    }
}
