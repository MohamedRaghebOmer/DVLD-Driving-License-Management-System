using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class ApplicationType
    {
        private int _applicationTypeId;
        private string _applicationTypeTitle;
        private decimal _applicationFees;

        public int ApplicationTypeId
        {
            get => _applicationTypeId;

            private set
            {
                if (value > 0)
                    _applicationTypeId = value;
                else
                    throw new ValidationException("ApplicationTypeId must be greater than zero.");
            }
        }

        public string ApplicationTypeTitle
        {
            get => _applicationTypeTitle;
            
            private set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _applicationTypeTitle = value;
                else
                    throw new ValidationException("ApplicationTypeTitle cannot be empty.");
            }
        }

        public decimal ApplicationFees
        {
            get => _applicationFees;
            
            private set
            {
                if (value >= 0)
                    _applicationFees = value;
                else
                    throw new ValidationException("ApplicationFees cannot be negative.");
            }
        }


        public ApplicationType()
        {
            _applicationTypeId = -1;
            _applicationTypeTitle = string.Empty;
            _applicationFees = 0;
        }

        public ApplicationType(string applicationTypeTitle, decimal applicationFees) : this()
        {
            ApplicationTypeTitle = applicationTypeTitle;
            ApplicationFees = applicationFees;
        }

        internal ApplicationType(int applicationTypeId, string applicationTypeTitle, decimal applicationFees) : this()
        {
            ApplicationTypeId = applicationTypeId;
            ApplicationTypeTitle = applicationTypeTitle;
            ApplicationFees = applicationFees;
        }
    }
}
