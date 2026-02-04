using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal static class UserValidator
    {
        public static void AddNewValidator(User user)
        {
            Core.Validators.UserValidator.Validate(user);

            if (UserData.IsPersonUsed(user.PersonID))
                throw new ValidationException("The person is already associated with another user.");

            if (UserData.IsExists(user.Username))
                throw new ValidationException("The username is already taken.");
        }

        public static void UpdateValidator(User user)
        {
            Core.Validators.UserValidator.Validate(user);

            if (UserData.IsPersonUsed(user.UserID, user.PersonID))
                throw new ValidationException("The person is already associated with another user.");

            if (UserData.IsNameUsed(user.UserID, user.Username))
                throw new ValidationException("The username is already taken by another user.");
        }
    }
}
