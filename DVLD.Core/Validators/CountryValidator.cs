using DVLD.Core.DTOs.Entities;
using DVLD.Core.Exceptions;

namespace DVLD.Core.Validators
{
    public static class CountryValidator
    {
        public static void Validate(Country country)
        {
            if (country == null)
                throw new ValidationException("Country information cannot be null.");
           
            if (string.IsNullOrWhiteSpace(country.CountryName))
                throw new ValidationException("Country name is required.");
        }
    }
}
