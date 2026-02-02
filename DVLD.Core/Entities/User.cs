using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class User
    {
        private int _userID;
        private int _personID;
        private string _username;
        private string _password;
        private bool _isActive;

        public int UserID
        {
            get => _userID;

            internal set
            {
                if (value <= 0)
                    throw new ValidationException("UserID must be a positive integer.");
                
                _userID = value;
            }
        }

        public int PersonID
        {
            get => _personID;
            set
            {
                if (value <= 0)
                    throw new ValidationException("PersonID must be a positive integer.");
                
                _personID = value;
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
            this._userID = -1;
            this._personID = -1;
            this._username = string.Empty;
            this._password = string.Empty;
            this._isActive = false;
        }

        public User(int personID, string username, string password, bool isActive) : this()
        {
            // this._userID = -1;
            this.PersonID = personID;
            this.Username = username;
            this.Password = password;
            this.IsActive = isActive;
        }

        internal User(int userID, int personID, string username, string password, bool isActive)
            : this()
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.Username = username;
            this.Password = password;
            this.IsActive = isActive;
        }
    }
}
