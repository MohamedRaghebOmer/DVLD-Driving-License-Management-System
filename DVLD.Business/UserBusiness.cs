using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;

namespace DVLD.Business
{
    public class UserBusiness
    {
        public static User Save(User user)
        {
            // Add new 
            if (user.UserID == -1)
            {
                UserValidator.AddNewValidator(user);

                try
                {
                    int newUserID = UserData.AddNew(user);
                    if (newUserID != -1)
                    {
                        return UserData.GetByID(newUserID);
                    }
                    return null;
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
                    if (UserData.Update(user))
                        return UserData.GetByID(user.UserID);
                    
                    return null;
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
                return UserData.GetByID(userID);
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
                return UserData.GetByPersonID(personID);
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
                return UserData.GetByName(username);
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
                return UserData.GetAll();
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading all users.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByUserID(int userID)
        {
            if (userID < 1 || !UserData.IsExists(userID))
                return false;

            try
            {
                return UserData.DeleteByUserID(userID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with ID = {userID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByPersonID(int personID)
        {
            if (personID < 1 || !UserData.IsPersonUsed(personID))
                return false;

            try
            {
                return UserData.DeleteByPersonID(personID);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with person ID = {personID}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static bool DeleteByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || !UserData.IsExists(username))
                return false;

            try
            {
                return UserData.DeleteByUserName(username);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with username = {username}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }
    }
}
