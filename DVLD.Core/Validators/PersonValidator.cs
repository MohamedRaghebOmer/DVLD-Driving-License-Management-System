using System;
using DVLD.Core.DTOs.Entities;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;

namespace DVLD.Core.Validators
{
    public static class PersonValidator
    {
        public static void Validate(Person person)
        {
            if (person == null)
                throw new ValidationException("Person information cannot be empty.");

            if (string.IsNullOrWhiteSpace(person.FirstName))
                throw new ValidationException("First name is required.");

            if (string.IsNullOrWhiteSpace(person.SecondName))
                throw new ValidationException("Second name is required.");

            if (string.IsNullOrWhiteSpace(person.ThirdName))
                person.ThirdName = string.Empty;

            if (string.IsNullOrWhiteSpace(person.LastName))
                throw new ValidationException("Last name is required.");

            if (person.DateOfBirth == null || person.DateOfBirth > DateTime.Now.AddYears(-18))
                throw new ValidationException("Date of birth is required and must be at least 18 years old.");

            if (person.Gender == Gender.Unknown)
                throw new ValidationException("Gender is required.");

            if (string.IsNullOrWhiteSpace(person.Address))
                throw new ValidationException("Address is required.");

            if (string.IsNullOrWhiteSpace(person.Phone) || person.Phone.Length < 7 || person.Phone.Length > 20)
                throw new ValidationException("Invalid phone number.");

            if (string.IsNullOrWhiteSpace(person.Email))
                person.Email = string.Empty;
            else if (!person.Email.Contains("@") || !person.Email.Contains("."))
                throw new ValidationException("Invalid email address.");

            if (person.NationalityCountryID <= 0)
                throw new ValidationException("Nationality is required.");

            if (person.ImagePath == null)
                person.ImagePath = string.Empty;
        }
    }
}
