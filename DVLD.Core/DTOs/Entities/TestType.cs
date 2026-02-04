using DVLD.Core.Exceptions;

namespace DVLD.Core.DTOs.Entities
{
    public class TestType
    {
        private int _testTypeID;
        private string _testTypeTitle;
        private string _testTypeDescription;
        private  decimal _testTypeFees;

        public int TestTypeID
        {
            get => _testTypeID;

            private set
            {
                if (value < 1)
                    throw new ValidationException("Test type id can't be negative.");

                _testTypeID = value;
            }
        }

        public string TestTypeTitle
        {
            get => _testTypeTitle;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("TestType title can't be empty.");
                else if (value.Length > 100)
                    throw new ValidationException("Test type title can't exceeds 100 character.");

                _testTypeTitle = value;
            }
        }

        public string TestTypeDescription
        {
            get => _testTypeDescription;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Test type description can't be empty");
            }
        }

        public decimal TestTypeFees
        {
            get => _testTypeFees;

            set
            {
                if (value < 0)
                    throw new ValidationException("Test type fees can't be negative.");
            }
        }


        public TestType()
        {
            this._testTypeID = -1;
            this._testTypeTitle = string.Empty;
            this._testTypeDescription = string.Empty;
            this._testTypeFees = 0;
        }

        public TestType(string testTypeTitle, string testTypeDescription, decimal testTypeFees) : this()
        {
            this.TestTypeTitle = testTypeTitle;
            this.TestTypeDescription = testTypeDescription;
            this.TestTypeFees = testTypeFees;
        }

        internal TestType(int testTypeID, string testTypeTitle, string testTypeDescription, decimal testTypeFees) : this()
        {
            this.TestTypeID = testTypeID;
            this.TestTypeTitle = testTypeTitle;
            this.TestTypeDescription = testTypeDescription;
            this.TestTypeFees = testTypeFees;
        }
    }
}
