using System;
using System.Runtime.CompilerServices;
using DVLD.Core.DTOs.Enums;

[assembly: InternalsVisibleTo("DVLD.Data")]

namespace DVLD.Core.DTOs.Entities
{
    public class Person
    {
        private int _personID;
        private string _nationalNo;
        private string _firstName;
        private string _secondName;
        private string _thirdName;
        private string _lastName;
        private DateTime _dateOfBirth;
        private Gender _gender;
        private string _address;
        private string _phone;
        private string _email;
        private int _countryID;
        private string _imagePath;

        public int PersonID
        {
            get => _personID; 

            internal set
            {
                if (value > 0 || value == -1)
                    _personID = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(PersonID), "Invalid PersonID");
            }
        }

        public string NationalNumber
        {
            get => _nationalNo;
            
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(NationalNumber), "National number cannot be null.");

                if (value.Length > 5 && value.Length < 20)
                    _nationalNo = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(NationalNumber), "Invalid national number length.");
            }
        }

        public string FirstName
        {
            get => _firstName;

            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(FirstName), "First name cannot be null.");

                // Not null and less than 15 characters
                if (value.Length > 1 && value.Length < 15)
                    _firstName = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(FirstName), "First name length must be less than 15 characters.");
            }
        }

        public string SecondName
        {
            get => _secondName;

            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(SecondName), "Second name cannot be null.");

                // Not null and less than 15 characters
                if (value.Length > 1 && value.Length < 15)
                    _secondName = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(SecondName), "Second name lenght must be less than 15 characters.");
            }
        }

        public string ThirdName
        {
            get => _thirdName;

            set
            {
                // Nullable field, but less than 15 characters
                string val = value ?? string.Empty;
                if (val.Length < 15)
                    _thirdName = val;
                else
                    throw new ArgumentOutOfRangeException(nameof(ThirdName), "Third name lenght must be less than 15 characters.");

            }
        }

        public string LastName
        {
            get => _lastName;

            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(LastName), "Last name cannot be null.");

                // Not null and less than 15 characters
                if (value.Length > 1 && value.Length < 15)
                    _lastName = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(LastName), "Last name lenght must be less than 15 characters.");

            }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;

            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(DateOfBirth), "Date of birth cannot be null.");

                DateTime minDate = DateTime.Now.AddYears(-100);
                DateTime maxDate = DateTime.Now.AddYears(-18);

                // Must be older than 18 years old and younger than 100 years old
                if ((value >= minDate && value <= maxDate) || value == DateTime.MinValue)
                    _dateOfBirth = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(DateOfBirth), "Person must be between 18 and 100 years old.");
            }
        }
        
        public Gender Gender
        {
            get => _gender;
            set => _gender = value;
        }

        public string Address
        {
            get => _address;

            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Address), "Address cannot be null.");

                if (value.Length > 5)
                    _address = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(Address), "Address must be more than 5 characters.");
            }
        }

        public string Phone
        {
            get => _phone;

            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Phone), "Phone cannot be null.");

                if (value.Length >= 7 && value.Length <= 20)
                    _phone = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(Phone), "Invalid phone length.");
            }
        }

        public string Email
        {
            get => _email;

            set
            {
                // Nullable field, but less than 50 characters
                if (value == null)
                    value = string.Empty;
                else if (!value.Contains("@") || !value.Contains("."))
                    throw new ArgumentException("Invalid email format.", nameof(Email));
                
                if (value.Length < 50)
                    _email = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(Email), "Invalid email length.");
            }
        }

        public int NationalityCountryID
        {
            get => _countryID;
            
            set
            {
                if (value > 0 || value == -1)
                    _countryID = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(NationalityCountryID), "Invalid nationality country ID");
            }

        }

        public string ImagePath
        {
            get => _imagePath;

            set
            {
                // Nullable field, but less than 100 characters
                string val = value ?? string.Empty;
                if (val.Length < 100)
                    _imagePath = val;
                else
                    throw new ArgumentOutOfRangeException(nameof(ImagePath), "Image path is too long.");
            } 
        }


        public Person()
        {
            this._personID = -1;
            this._nationalNo = string.Empty;
            this._firstName = string.Empty;
            this._secondName = string.Empty;
            this._thirdName = string.Empty;
            this._lastName = string.Empty;
            this._dateOfBirth = DateTime.MinValue;
            this._gender = Gender.Unknown;
            this._address = string.Empty;
            this._phone = string.Empty;
            this._email = string.Empty;
            this._countryID = -1;
            this._imagePath = string.Empty;
        }

        public Person(string nationalNo, string firstName, string secondName, string thirdName,
                      string lastName, DateTime dateOfBirth, Gender gender, string address, string phone,
                      string email, int nationalityCountryID, string imagePath) : this()
        {
            // this._personID = -1;
            this.NationalNumber = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityCountryID;
            this.ImagePath = imagePath;
        }
    
        internal Person(int personID, string nationalNo,  string firstName, string secondName, string thirdName,
                      string lastName, DateTime dateOfBirth, Gender gender, string address, string phone,
                      string email, int nationalityCountryID, string imagePath) : this()
        {
            this.PersonID = personID;
            this.NationalNumber = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityCountryID;
            this.ImagePath = imagePath;
        }
    }
}
