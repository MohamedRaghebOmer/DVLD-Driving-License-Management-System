using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;
using DVLD.Core.Exceptions;

namespace DVLD.Business
{
    public static class TestTypeBusiness
    {
        public static TestType Save(TestType testType)
        {
            // Add new test type
            if (testType.TestTypeId == -1)
            {
                TestTypeValidator.AddNewValidator(testType);

                try
                {
                    int newTestTypeId = TestTypeData.Add(testType);

                    if (newTestTypeId != -1)
                        return TestTypeData.Get(newTestTypeId);
                    
                    return null;
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
                    if (TestTypeData.Update(testType))
                        return TestTypeData.Get(testType.TestTypeId);
                    return null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("BLL: Error while updating a new test type.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }

            }
        }

        public static TestType Find(int testTypeId)
        {
            if (testTypeId < 1)
                return null;

            try
            {
                return TestTypeData.Get(testTypeId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get test type by id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static TestType Find(string testTypeTitle)
        {
            if (string.IsNullOrWhiteSpace(testTypeTitle))
                return null;

            try
            {
                return TestTypeData.Get(testTypeTitle);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get test type by title.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static DataTable GetAll()
        {
            try
            {
                return TestTypeData.GetAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get all test type.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static bool Exists(int testTypeId)
        {
            if (testTypeId < 1)
                return false;

            try
            {
                return TestTypeData.Exists(testTypeId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to check if test type with id = {testTypeId} exists.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsTitleUsed(string title, int excludedTestTypeId)
        {
            if (string.IsNullOrWhiteSpace(title))
                return false;

            try
            {
                return TestTypeData.IsTitleUsed(title, excludedTestTypeId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to check if test type title is used.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool Delete(int testTypeId)
        {
            if (testTypeId < 1 || !Exists(testTypeId))
                throw new ValidationException("The specified test type does not exist.");

            try
            {
                return TestTypeData.Delete(testTypeId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to delete test type with id = {testTypeId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static bool Delete(string testTypeTitle)
        {
            if (string.IsNullOrWhiteSpace(testTypeTitle) || !IsTitleUsed(testTypeTitle, -1))
                throw new ValidationException("The specified test type does not exist.");

            try
            {
                return TestTypeData.Delete(testTypeTitle);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while trying to delete test type with title = {testTypeTitle}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }
    }
}
