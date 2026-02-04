using System;
using System.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Business.EntityValidators;
using DVLD.Data;
using DVLD.Core.Logging;

namespace DVLD.Business
{
    public static class DriverManager
    {
        public static bool Save(Driver driver)
        {
            // Add new dirver
            if (driver.DriverID == -1)
            {
                DriverValidator.AddNewValidator(driver);
                
                try
                {
                    return DriverService.AddNewDriver(driver);
                }
                catch(Exception ex)
                {
                    AppLogger.LogError("BLL: Error while adding a new driver.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
            else // Update existing driver
            {
                DriverValidator.UpdateValidator(driver);

                try
                {
                    return DriverService.Update(driver);
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("BLL: Error while updating an existing driver.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
        }

        public static Driver GetDriverByID(int driverID)
        {
            if (driverID < 1)
                return null;

            try
            {
                return DriverService.GetDriverByID(driverID);
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get driver by id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Driver GetDriverByPersonID(int personID)
        {
            if (personID < 1)
                return null;

            try
            {
                return DriverService.GetDriverByPersonID(personID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get driver by person id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAllDrivers()
        {
            try
            {
                return DriverService.GetAllDrivers();
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get all drivers.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByDriverID(int driverID)
        {
            if (driverID < 1)
                return false;

            try
            {
                return DriverService.DeleteByDriverID(driverID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Errror while deleting dirver with id = {driverID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByPersonID(int personID)
        {
            if (personID < 1)
                return false;

            try
            {
                return DriverService.DeleteByPersonID(personID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Errror while deleting dirver with person id = {personID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }
    }
}
