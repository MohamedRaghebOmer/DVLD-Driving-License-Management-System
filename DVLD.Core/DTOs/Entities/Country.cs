using DVLD.Core.Exceptions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DVLD.Data")]

namespace DVLD.Core.DTOs.Entities
{
    public class Country
    {
        private int _countryId;
        private string _countryName;

        public int CountryId
        {
            get => _countryId;

            private set
            {
                if (value > 0)
                    _countryId = value;
                else
                    throw new ValidationException("Invalid CountryId.");
            }
        }

        public string CountryName 
        {
            get => _countryName;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Country name cannot be empty.");

                if (value.Length >= 3)
                    _countryName = value;
                else
                    throw new ValidationException("Invalid CountryName.");
            }
        }


        // Consturctores
        public Country()
        {
            this._countryId = -1;
            this._countryName = string.Empty;
        }

        public Country(string countryName)
        {
            this._countryId = -1;
            this._countryName = countryName;
        }

        // To use inside DVLD.Data.GetCountryInfoById only
        internal Country(int id, string countryName)
        {
            this._countryId = id;
            this._countryName = countryName;
        }
    }

}
