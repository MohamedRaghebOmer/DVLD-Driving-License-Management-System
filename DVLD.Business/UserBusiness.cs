using System;
using System.Data;
using DVLD.Data;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.Logging;
using DVLD.Business.EntityValidators;
using DVLD.Core.Exceptions;

namespace DVLD.Business
{
    public class UserBusiness
    {
        public static User Save(User user)
        {
            // Add new 
            if (user.UserId == -1)
            {
                UserValidator.AddNewValidator(user);

                try
                {
                    int newUserId = UserData.Add(user);
                    if (newUserId != -1)
                    {
                        return UserData.GetByUsersId(newUserId);
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
                        return UserData.GetByUsersId(user.UserId);
                    
                    return null;
                }
                catch(Exception ex)
                {
                    AppLogger.LogError($"BLL: Error while updating user with Id: {user.UserId}.");
                    throw new Exception("We encountered a technical issue. Please try again later.", ex);
                }
            }
        }

        public static User FindByUserId(int userId)
        {
            if (userId < 1)
                return null;

            try
            {
                return UserData.GetByUsersId(userId);
            }
            catch(Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading user by id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static User FindByPersonId(int personId)
        {
            if (personId < 1)
                return null;

            try
            {
                return UserData.GetByPersonId(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while reading user by person id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static User FindByUserByName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            try
            {
                return UserData.GetByUsername(username);
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

        public static bool Exists(int userId)
        {
            if (userId < 1)
                return false;

            try
            {
                return UserData.Exists(userId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking user existence by Id.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static bool IsActive(int userId)
        {
            if (userId < 1)
                return false;
            try
            {
                return UserData.IsActive(userId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if user is active.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsUsernameUsed(string username, int excludedUserId)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;
            try
            {
                return UserData.IsUsernameUsed(username, excludedUserId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if username is used.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool IsPersonUsed(int personId, int excludedUserId)
        {
            if (personId < 1)
                return false;
            try
            {
                return UserData.IsPersonUsed(personId, excludedUserId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("BLL: Error while checking if person is used.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByUserId(int userId)
        {
            if (userId < 1 || !UserData.Exists(userId))
                throw new ValidationException("Invalid user Id.");

            try
            {
                return UserData.DeleteByUserId(userId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with Id = {userId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }
        }

        public static bool DeleteByPersonId(int personId)
        {
            if (personId < 1 || !UserData.Exists(personId))
                throw new ValidationException("Invalid person Id.");

            try
            {
                return UserData.DeleteByPersonId(personId);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with person Id = {personId}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }

        public static bool DeleteByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || !UserData.IsUsernameUsed(username, -1))
                throw new ValidationException("Invalid username.");

            try
            {
                return UserData.DeleteByUsername(username);
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"BLL: Error while deleting user with username = {username}.");
                throw new Exception("We encountered a technical issue. Please try again later.", ex);
            }

        }
    }
}
