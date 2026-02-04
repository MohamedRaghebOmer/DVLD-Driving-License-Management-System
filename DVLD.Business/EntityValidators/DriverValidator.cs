using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;

namespace DVLD.Business.EntityValidators
{
    internal static class DriverValidator
    {
        public static void AddNewValidator(Driver driver)
        {
            Core.Validators.DriverValidator.Validate(driver);

            if (UserData.IsExists(driver.CreatedByUserID))
            {
                if (UserData.IsActive(driver.CreatedByUserID))
                {
                    if (PersonData.IsExists(driver.PersonID))
                    {
                        if (DriverData.IsPersonUsed(driver.PersonID))
                            throw new BusinessException("The person is already associated with another driver.");
                    }
                    else
                        throw new BusinessException("Person doesn't exists.");
                }
                else
                    throw new BusinessException("Account inactive. Please contact your administrator to activate your account.");
            }
            else
                throw new BusinessException($"User with id = {driver.CreatedByUserID} doesn't exits.");
        }

        public static void UpdateValidator(Driver driver)
        {
            Core.Validators.DriverValidator.Validate(driver);
            Driver storedInfo = DriverData.GetByID(driver.DriverID);

            if (storedInfo.CreatedByUserID == driver.CreatedByUserID)
            {
                if (UserData.IsActive(driver.CreatedByUserID))
                {
                    if (storedInfo.CreatedDate == driver.CreatedDate)
                    {
                        if (PersonData.IsExists(driver.PersonID))
                        {
                            if (DriverData.IsPersonUsed(driver.PersonID, driver.DriverID))
                                throw new BusinessException("The person is already associated with another driver.");
                        }
                        else
                            throw new BusinessException("Person doesn't exists.");
                    }
                    else
                        throw new BusinessException("Creation date can't be changed.");
                }
                else
                    throw new BusinessException("Account inactive. Please contact your administrator to activate your account.");
            }
            else
                throw new BusinessException($"Can't change user who created the driver to another one.");
        }
    }
}   
