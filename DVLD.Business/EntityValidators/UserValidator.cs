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

            if (UserData.IsPersonUsed(user.PersonId, -1))
                throw new BusinessException("The person is already associated with another user.");

            if (UserData.IsUsernameUsed(user.Username, -1))
                throw new BusinessException("The username is already taken.");
        }

        public static void UpdateValidator(User user)
        {
            Core.Validators.UserValidator.Validate(user);

            if (UserData.IsPersonUsed(user.PersonId, user.UserId)) 
                throw new BusinessException("The person is already associated with another user.");

            if (UserData.IsUsernameUsed(user.Username, user.UserId))
                throw new BusinessException("The username is already taken by another user.");
        }
    }
}
