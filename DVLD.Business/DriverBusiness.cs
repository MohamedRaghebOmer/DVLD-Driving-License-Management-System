using System;
using System.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Business.EntityValidators;
using DVLD.Data;
using DVLD.Core.Logging;
using DVLD.Core.Exceptions;

namespace DVLD.Business
{
    public static class DriverBusiness
    {
        public static Driver Save(Driver driver)
        {
            // Add new driver
            if (driver.DriverId == -1)
            {
                DriverValidator.AddNewValidator(driver);
                
                try
                {
                    int newDriverId = DriverData.Add(driver);
                    
                    if (newDriverId != -1)
                        return DriverData.GetByDriverId(newDriverId);
                   
                    return null;
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
                    if (DriverData.Update(driver))
                        return DriverData.GetByDriverId(driver.DriverId);
                    
                    return null;
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("BLL: Error while updating an existing driver.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
        }

        public static Driver FindByDriverId(int driverId)
        {
            if (driverId < 1)
                return null;

            try
            {
                return DriverData.GetByDriverId(driverId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get driver by id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static Driver FindByPersonId(int personId)
        {
            if (personId < 1)
                return null;

            try
            {
                return DriverData.GetByPersonId(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get driver by person id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static DataTable GetAll()
        {
            try
            {
                return DriverData.GetAll();
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while trying to get all drivers.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsPersonUsed(int personId, int excludedDriverId)
        {
            if (personId < 1 || excludedDriverId < 1)
                return false;
            try
            {
                return DriverData.IsPersonUsed(personId, excludedDriverId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if person is used by any driver excluding a specific driver.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DoesDriverIdExist(int driverId)
        {
            if (driverId < 1)
                return false;
            try
            {
                return DriverData.Exists(driverId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking existence of driver by id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByDriverId(int driverId)
        {
            if (driverId < 1 || !DriverData.Exists(driverId))
                throw new ValidationException("The specified driver does not exist.");

            try
            {
                return DriverData.DeleteByDriverId(driverId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Errror while deleting dirver with id = {driverId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByPersonId(int personId)
        {
            if (personId < 1 || !DriverData.IsPersonUsed(personId, -1))
                throw new ValidationException("The specified person does not exist.");

            try
            {
                return DriverData.DeleteByPersonId(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Errror while deleting dirver with person id = {personId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

    }
}
