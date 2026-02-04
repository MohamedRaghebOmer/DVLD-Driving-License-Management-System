using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public static class TestTypeManager
    {
        public static bool Save(TestType testType)
        {
            // Add new test type
            if (testType.TestTypeID == -1)
            {
                TestTypeValidator.AddNewValidator(testType);

                try
                {
                    return TestTypeService.AddNewTestType(testType);
                }
                catch(Exception ex)
                {
                    AppLogger.LogError("BLL: Error while adding a new test type.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
            else // Update existing test type
            {
                TestTypeValidator.UpdateValidator(testType);

                try
                {
                    return TestTypeService.UpdateTestType(testType);
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("BLL: Error while updating a new test type.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }

            }
        }

        public static TestType GetTestType(int testTypeID)
        {
            if (testTypeID < 1)
                return null;

            try
            {
                return TestTypeService.GetTestTypeByID(testTypeID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get test type by id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static TestType GetTestType(string testTypeTitle)
        {
            if (string.IsNullOrWhiteSpace(testTypeTitle))
                return null;

            try
            {
                return TestTypeService.GetTestTypeByTitle(testTypeTitle);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get test type by title.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static DataTable GetAllTestTypes()
        {
            try
            {
                return TestTypeService.GetAllTestTypes();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get all test type.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static bool DeleteTestType(int testTypeID)
        {
            if (testTypeID < 1)
                return false;

            try
            {
                return TestTypeService.DeleteTestType(testTypeID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to delete test type with id = {testTypeID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static bool DeleteTestType(string testTypeTitle)
        {
            if (string.IsNullOrWhiteSpace(testTypeTitle))
                return false;

            try
            {
                return TestTypeService.DeleteTestType(testTypeTitle);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to delete test type with title = {testTypeTitle}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

    }
}
