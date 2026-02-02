using System;
using DVLD.Core.DTOs.Enums;
using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class Person
    {
        private const int MaxNameLength = 15;

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
                if (value > 0)
                    _personID = value;
                else
                    throw new ValidationException("Invalid PersonID");
            }
        }

        public string NationalNumber
        {
            get => _nationalNo;
            
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("National number cannot be empty.");

                if (value.Length > 5 && value.Length < 20)
                    _nationalNo = value;
                else
                    throw new ValidationException("Invalid national number length.");
            }
        }

        public string FirstName
        {
            get => _firstName;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("First name cannot be empty.");

                // Not null and less than 15 characters
                if (value.Length > 1 && value.Length <= MaxNameLength)
                    _firstName = value;
                else
                    throw new ValidationException($"First name must be between 2 and {MaxNameLength} characters.");
            }
        }

        public string SecondName
        {
            get => _secondName;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Second name cannot be empty.");

                // Not null and less than 15 characters
                if (value.Length > 1 && value.Length <= MaxNameLength)
                    _secondName = value;
                else
                    throw new ValidationException($"Second name must be between 2 and {MaxNameLength} characters");
            }
        }

        public string ThirdName
        {
            get => _thirdName;

            set
            {
                // Nullable field, but less than 15 characters
                if (string.IsNullOrWhiteSpace(value))
                    value = string.Empty;

                if (value.Length <= MaxNameLength)
                    _thirdName = value;
                else
                    throw new ValidationException($"Third name length cannot exceed {MaxNameLength} characters.");
            }
        }

        public string LastName
        {
            get => _lastName;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Last name cannot be empty.");

                // Not null and less than 15 characters
                if (value.Length > 1 && value.Length <= MaxNameLength)
                    _lastName = value;
                else
                    throw new ValidationException($"Last name length must be between 2 and {MaxNameLength} characters.");

            }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;

            set
            {
                if (value == null)
                    throw new ValidationException("Date of birth cannot be empty.");

                DateTime minDate = DateTime.Now.AddYears(-100);
                DateTime maxDate = DateTime.Now.AddYears(-18);

                // Must be between 18 and 100 years old
                if ((value >= minDate && value <= maxDate))
                    _dateOfBirth = value;
                else
                    throw new ValidationException($"Person must be between 18 and 100 years old.");

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
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Address cannot be empty.");

                if (value.Length > 5)
                    _address = value;
                else
                    throw new ValidationException("Address must be more than 5 characters.");
            }
        }

        public string Phone
        {
            get => _phone;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Phone cannot be empty.");

                if (value.Length >= 7 && value.Length <= 20)
                    _phone = value;
                else
                    throw new ValidationException("Invalid phone length.");
            }
        }

        public string Email
        {
            get => _email;

            set
            {
                // Nullable field, but less than 50 characters
                if (string.IsNullOrWhiteSpace(value))
                    value = string.Empty;
                else if (!value.Contains("@") || !value.Contains("."))
                    throw new ValidationException("Invalid email format.");
                
                if (value.Length < 50)
                    _email = value;
                else
                    throw new ValidationException("Invalid email length.");
            }
        }

        public int NationalityCountryID
        {
            get => _countryID;
            
            set
            {
                if (value > 0)
                    _countryID = value;
                else
                    throw new ValidationException("Invalid country ID.");
            }

        }

        public string ImagePath
        {
            get => _imagePath;

            set
            {
                // Nullable field, but less than 100 characters
                string val = value ?? string.Empty;
                if (val.Length < 250)
                    _imagePath = val;
                else
                    throw new ValidationException("Image path is too long.");
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
            this._dateOfBirth = new DateTime(1, 1, 1);
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
