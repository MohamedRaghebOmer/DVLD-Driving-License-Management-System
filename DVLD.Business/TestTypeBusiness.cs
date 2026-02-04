using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public static class TestTypeBusiness
    {
        public static TestType Save(TestType testType)
        {
            // Add new test type
            if (testType.TestTypeID == -1)
            {
                TestTypeValidator.AddNewValidator(testType);

                try
                {
                    int newTestTypeID = TestTypeData.AddNew(testType);

                    if (newTestTypeID != -1)
                        return TestTypeData.Get(newTestTypeID);
                    
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
                        return TestTypeData.Get(testType.TestTypeID);
                    return null;
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
                return TestTypeData.Get(testTypeID);
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
                return TestTypeData.Get(testTypeTitle);
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
                return TestTypeData.GetAll();
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
                return TestTypeData.Delete(testTypeID);
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
