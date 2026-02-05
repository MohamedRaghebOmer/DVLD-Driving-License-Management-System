using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;

namespace DVLD.Core.Validators
{
    public static class UserValidator
    {
        public static void Validate(User user)
        {
            if (user == null)
                throw new ValidationException("User cannot be null.");
           
            if (user.PersonId <= 0)
                throw new ValidationException("PersonId must be a positive integer.");
           
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ValidationException("Username cannot be empty.");
            
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 4)
                throw new ValidationException("Password must be at least 4 characters long.");
        }
    }
}
