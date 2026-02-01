using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DVLD.Data")]

namespace DVLD.Core.DTOs.Entities
{
    public class Country
    {
        private int _countryID;
        private string _countryName;

        public int CountryID
        {
            get => _countryID;
            
            internal set
            {
                if (value > 0 || value == -1)
                    _countryID = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(_countryID), "Invalid CountryID.");
            }
        }

        public string CountryName 
        {
            get => _countryName;

            set
            {
                if (value.Length >= 3)
                    _countryName = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(_countryName), "Invalid CountryName.");
            }
        }


        // Consturctores
        public Country()
        {
            this._countryID = -1;
            this._countryName = string.Empty;
        }

        public Country(string countryName)
        {
            this._countryID = -1;
            this._countryName = countryName;
        }

        // To use inside DVLD.Data.GetCountryInfoByID only
        internal Country(int id, string countryName)
        {
            this._countryID = id;
            this._countryName = countryName;
        }
    }

}
