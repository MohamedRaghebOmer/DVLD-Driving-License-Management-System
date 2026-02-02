using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal static class PersonValidator
    {
        public static void AddNewValidator(Person person)
        {
            Core.Validators.PersonValidator.Validate(person);

            if (PersonService.IsExists(person.NationalNumber))
                throw new BusinessException("National number is already exists, please enter a valid one.");

            if (!CountryService.IsExists(person.NationalityCountryID))
                throw new BusinessException("Country ID is not exists, please enter a valid one.");
        }

        public static void UpdateValidator(Person person)
        {
            Core.Validators.PersonValidator.Validate(person);

            if (PersonService.IsExists(person.NationalNumber, person.PersonID))
                throw new BusinessException("National number is already exists, please enter a valid one.");

            if (!CountryService.IsExists(person.NationalityCountryID))
                throw new BusinessException("Country ID is not exists, please enter a valid one.");
        }
    }
}
