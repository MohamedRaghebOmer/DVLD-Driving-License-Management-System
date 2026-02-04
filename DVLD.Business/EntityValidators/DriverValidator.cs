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

            if (UserService.IsUserExists(driver.CreatedByUserID))
            {
                if (UserService.IsActiveUser(driver.CreatedByUserID))
                {
                    if (PersonService.IsExists(driver.PersonID))
                    {
                        if (DriverService.IsPersonUsed(driver.PersonID))
                            throw new ValidationException("The person is already associated with another driver.");
                    }
                    else
                        throw new ValidationException("Person doesn't exists.");
                }
                else
                    throw new ValidationException("Account inactive. Please contact your administrator to activate your account.");
            }
            else
                throw new ValidationException($"User with id = {driver.CreatedByUserID} doesn't exits.");
        }

        public static void UpdateValidator(Driver driver)
        {
            Core.Validators.DriverValidator.Validate(driver);
            Driver storedInfo = DriverService.GetDriverByID(driver.DriverID);

            if (storedInfo.CreatedByUserID == driver.CreatedByUserID)
            {
                if (UserService.IsActiveUser(driver.CreatedByUserID))
                {
                    if (storedInfo.CreatedDate == driver.CreatedDate)
                    {
                        if (PersonService.IsExists(driver.PersonID))
                        {
                            if (DriverService.IsPersonUsed(driver.PersonID, driver.DriverID))
                                throw new ValidationException("The person is already associated with another driver.");
                        }
                        else
                            throw new ValidationException("Person doesn't exists.");
                    }
                    else
                        throw new ValidationException("Creation date can't be changed.");
                }
                else
                    throw new ValidationException("Account inactive. Please contact your administrator to activate your account.");
            }
            else
                throw new ValidationException($"Can't change user who created the driver to another one.");
        }
    }
}   
