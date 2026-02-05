using System;
using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class Driver
    {
        private int _driverId;
        private int _personId;
        private int _createdByUserId;
        private DateTime _createdDate;


        public int DriverId
        {
            get => _driverId;

            private set
            {
                if (value > 0)
                    _driverId = value;
                else
                    throw new ValidationException("Driver Id must be a positive integer.");
            }
        }

        public int PersonId
        {
            get => _personId;

            set
            {
                if (value > 0)
                    _personId = value;
                else
                    throw new ValidationException("Person Id must be a positive integer.");
            }
        }

        public int CreatedByUserId
        {
            get => _createdByUserId;

            set
            {
                if (value > 0)
                    _createdByUserId = value;
                else
                    throw new ValidationException("User Id must be a positive integer.");
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
            this._driverId = -1;
            this._personId = -1;
            this._createdByUserId = -1;
            this._createdDate = new DateTime(1900, 1, 1);
        }

        public Driver(int personId, int createdByUserId) : this()
        {
            // this._driverId = -1;
            this.PersonId = personId;
            this.CreatedByUserId = createdByUserId;
            // this._dateCreated = new DateTime(1, 1, 1);
        }

        internal Driver(int driverId, int personId, int createdByUserId, DateTime createdDate)
        {
            this.DriverId = driverId;
            this.PersonId = personId;
            this.CreatedByUserId = createdByUserId;
            this.CreatedDate = createdDate;
        }
    }
}
