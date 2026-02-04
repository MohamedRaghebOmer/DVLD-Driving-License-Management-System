using System;
using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class Driver
    {
        private int _driverID;
        private int _personID;
        private int _createdByUserID;
        private DateTime _createdDate;


        public int DriverID
        {
            get => _driverID;

            private set
            {
                if (value > 0)
                    _driverID = value;
                else
                    throw new ValidationException("Driver ID must be a positive integer.");
            }
        }

        public int PersonID
        {
            get => _personID;

            set
            {
                if (value > 0)
                    _personID = value;
                else
                    throw new ValidationException("Person ID must be a positive integer.");
            }
        }

        public int CreatedByUserID
        {
            get => _createdByUserID;

            set
            {
                if (value > 0)
                    _createdByUserID = value;
                else
                    throw new ValidationException("User ID must be a positive integer.");
            }
        }

        public DateTime CreatedDate
        {
            get => _createdDate;

            private set
            {
                if (value <= DateTime.Now)
                    _createdDate = value;
                else
                    throw new ValidationException("Creation date can't be in the future.");
            }
        }


        public Driver()
        {
            this._driverID = -1;
            this._personID = -1;
            this._createdByUserID = -1;
            this._createdDate = new DateTime(1900, 1, 1);
        }

        public Driver(int personID, int createdByUserID) : this()
        {
            // this._driverID = -1;
            this.PersonID = personID;
            this.CreatedByUserID = createdByUserID;
            // this._dateCreated = new DateTime(1, 1, 1);
        }

        internal Driver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            this.DriverID = driverID;
            this.PersonID = personID;
            this.CreatedByUserID = createdByUserID;
            this.CreatedDate = createdDate;
        }
    }
}
