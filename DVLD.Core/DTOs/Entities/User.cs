using DVLD.Core.Exceptions;
using System;
using System.Runtime.InteropServices;

namespace DVLD.Core.DTOs.Entities
{
    public class User
    {
        private int _userId;
        private int _personId;
        private string _username;
        private string _password;
        private bool _isActive;

        public int UserId
        {
            get => _userId;

            private set
            {
                if (value <= 0)
                    throw new ValidationException("UserId can't be negative.");

                _userId = value;
            }
        }

        public int PersonId
        {
            get => _personId;
            set
            {
                if (value <= 0)
                    throw new ValidationException("PersonId must be a positive integer.");
                
                _personId = value;
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Username cannot be empty.");
                
                _username = value;
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                    throw new ValidationException("Password must be at least 4 characters long.");
                _password = value;
            }
        }

        public bool IsActive
        { 
            get => _isActive;
            set => _isActive = value;
        }        
      

        public User()
        {
            this._userId = -1;
            this._personId = -1;
            this._username = string.Empty;
            this._password = string.Empty;
            this._isActive = false;
        }

        public User(int personId, string username, string password, bool isActive) : this()
        {
            // this._userId = -1;
            this.PersonId = personId;
            this.Username = username;
            this.Password = password;
            this.IsActive = isActive;
        }

        internal User(int userId, int personId, string username, string password, bool isActive) : this()
        {
            this.UserId = userId;
            this.PersonId = personId;
            this.Username = username;
            this.Password = password;
            this.IsActive = isActive;
        }
    }
}
