using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public class UserManager
    {
        public static bool Save(User user)
        {
            // Add new 
            if (user.UserID == -1)
            {
                UserValidator.AddNewValidator(user);

                try
                {
                    return UserService.AddNew(user);
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("BLL: Error while adding a new user.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
            else // Update
            {
                UserValidator.UpdateValidator(user);

                try
                {
                    return UserService.Update(user);
                }
                catch(Exception ex)
                {
                    AppLogger.LogError($"BLL: Error while updating user with ID: {user.UserID}.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
        }

        public static User GetUserByID(int userID)
        {
            if (userID < 1)
                return null;

            try
            {
                return UserService.GetUserByID(userID);
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading user by id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static User GetUserByPersonID(int personID)
        {
            if (personID < 1)
                return null;

            try
            {
                return UserService.GetUserByPersonID(personID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading user by person id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static User GetUserByName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            try
            {
                return UserService.GetUserByName(username);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading user by name.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static DataTable GetAllUsers()
        {
            try
            {
                return UserService.GetAll();
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading all users.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByUserID(int userID)
        {
            if (userID < 1 || !UserService.IsUserExists(userID))
                return false;

            try
            {
                return UserService.DeleteByUserID(userID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with ID = {userID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByPersonID(int personID)
        {
            if (personID < 1 || !UserService.IsPersonUsed(personID))
                return false;

            try
            {
                return UserService.DeleteByPersonID(personID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with person ID = {personID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static bool DeleteByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || !UserService.IsUsernameExists(username))
                return false;

            try
            {
                return UserService.DeleteByUsername(username);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with username = {username}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }
    }
}
