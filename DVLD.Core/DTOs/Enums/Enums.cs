namespace DVLD.Core.DTOs.Enums
{
    public enum Gender { Male = 0, Female = 1, Unknown = 2}

    public enum LicenseClass 
    { 
        Class1_SmallMotorcycle = 1,
        Class2_HeavyMotorcycle = 2,
        Class3_OrdinaryDrivingLicense = 3,
        Class4_Commercial = 4,
        Class5_Agricultural = 5,
        Class6_SmallAndMediumBus = 6,
        Class7_TruckAndHeavyVehicle = 7
    }

    public enum IssueReason
    {
        FirstTime = 1,
        Renew = 2,
        ReplacementLost = 3,
        ReplacementDamaged = 4
    }

    public enum ApplicationType
    {
       NewLocalDrivingLicenseService = 1,
       RenewDrivingLicenseService = 2,
       ReplacementForLostDrivingLicense = 3,
       ReplacementForDamagedDrivingLicense = 4,
       ReleaseDetainedDrivingLicense = 5,
       NewInternationalLicense = 6
    }
}
