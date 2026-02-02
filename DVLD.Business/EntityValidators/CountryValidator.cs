using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;
using DVLD.Data;

namespace DVLD.Business.EntityValidators
{
    internal static class CountryValidator
    {
        public static void AddNewValidator(Country country)
        {
            Core.Validators.CountryValidator.Validate(country);

            if (country.CountryName.Length < 3)
                throw new BusinessException("Country name is too short, please try another one.");

            if (CountryService.IsExists(country.CountryName))
                throw new BusinessException($"Country with name {country.CountryName} is already exists, please try another one.");
        }

        public static void UpdateValidator(Country country)
        {
            Core.Validators.CountryValidator.Validate(country);

            if (country.CountryName.Length < 3)
                throw new BusinessException("Country name is too short, please try another one.");

            if (CountryService.IsExists(country.CountryName, country.CountryID))
                throw new BusinessException($"The country name '{country.CountryName}'  is already exists, please try another one.");
        }
    }
}
