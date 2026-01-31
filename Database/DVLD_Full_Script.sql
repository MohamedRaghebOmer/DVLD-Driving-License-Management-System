/* Check if database exists, if not create it.
   This allows the script to run on any machine immediately.
*/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DVLD')
BEGIN
    CREATE DATABASE [DVLD]
END
GO

USE [DVLD]
GO
/****** Object:  Table [dbo].[People]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[NationalNo] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[SecondName] [nvarchar](20) NOT NULL,
	[ThirdName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Gender] [tinyint] NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[NationalityCountryID] [int] NOT NULL,
	[ImagePath] [nvarchar](250) NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestAppointments]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestAppointments](
	[TestAppointmentID] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeID] [int] NOT NULL,
	[LocalDrivingLicenseApplicationID] [int] NOT NULL,
	[AppointmentDate] [smalldatetime] NOT NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[IsLocked] [bit] NOT NULL,
 CONSTRAINT [PK_TestAppointments] PRIMARY KEY CLUSTERED 
(
	[TestAppointmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tests]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[TestID] [int] IDENTITY(1,1) NOT NULL,
	[TestAppointmentID] [int] NOT NULL,
	[TestResult] [bit] NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[TestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[ApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicantPersonID] [int] NOT NULL,
	[ApplicationDate] [datetime] NOT NULL,
	[ApplicationTypeID] [int] NOT NULL,
	[ApplicationStatus] [tinyint] NOT NULL,
	[LastStatusDate] [datetime] NOT NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicenseClasses]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenseClasses](
	[LicenseClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NOT NULL,
	[ClassDescription] [nvarchar](500) NOT NULL,
	[MinimumAllowedAge] [tinyint] NOT NULL,
	[DefaultValidityLength] [tinyint] NOT NULL,
	[ClassFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_LicenseClasses] PRIMARY KEY CLUSTERED 
(
	[LicenseClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocalDrivingLicenseApplications]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalDrivingLicenseApplications](
	[LocalDrivingLicenseApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[LicenseClassID] [int] NOT NULL,
 CONSTRAINT [PK_DrivingLicsenseApplications] PRIMARY KEY CLUSTERED 
(
	[LocalDrivingLicenseApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[LocalDrivingLicenseApplications_View]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[LocalDrivingLicenseApplications_View]
AS
SELECT        dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, dbo.LicenseClasses.ClassName, dbo.People.NationalNo, dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + ISNULL(dbo.People.ThirdName, '') 
                         + ' ' + dbo.People.LastName AS FullName, dbo.Applications.ApplicationDate,
                             (SELECT        COUNT(dbo.TestAppointments.TestTypeID) AS PassedTestCount
                               FROM            dbo.Tests INNER JOIN
                                                         dbo.TestAppointments ON dbo.Tests.TestAppointmentID = dbo.TestAppointments.TestAppointmentID
                               WHERE        (dbo.TestAppointments.LocalDrivingLicenseApplicationID = dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID) AND (dbo.Tests.TestResult = 1)) AS PassedTestCount, 
                         CASE WHEN Applications.ApplicationStatus = 1 THEN 'New' WHEN Applications.ApplicationStatus = 2 THEN 'Cancelled' WHEN Applications.ApplicationStatus = 3 THEN 'Completed' END AS Status
FROM            dbo.LocalDrivingLicenseApplications INNER JOIN
                         dbo.Applications ON dbo.LocalDrivingLicenseApplications.ApplicationID = dbo.Applications.ApplicationID INNER JOIN
                         dbo.LicenseClasses ON dbo.LocalDrivingLicenseApplications.LicenseClassID = dbo.LicenseClasses.LicenseClassID INNER JOIN
                         dbo.People ON dbo.Applications.ApplicantPersonID = dbo.People.PersonID
GO
/****** Object:  Table [dbo].[TestTypes]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestTypes](
	[TestTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeTitle] [nvarchar](100) NOT NULL,
	[TestTypeDescription] [nvarchar](500) NOT NULL,
	[TestTypeFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_TestTypes] PRIMARY KEY CLUSTERED 
(
	[TestTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TestAppointments_View]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TestAppointments_View]
AS
SELECT        dbo.TestAppointments.TestAppointmentID, dbo.TestAppointments.LocalDrivingLicenseApplicationID, dbo.TestTypes.TestTypeTitle, dbo.LicenseClasses.ClassName, dbo.TestAppointments.AppointmentDate, 
                         dbo.TestAppointments.PaidFees, dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + ISNULL(dbo.People.ThirdName, '') + ' ' + dbo.People.LastName AS FullName, dbo.TestAppointments.IsLocked
FROM            dbo.TestAppointments INNER JOIN
                         dbo.TestTypes ON dbo.TestAppointments.TestTypeID = dbo.TestTypes.TestTypeID INNER JOIN
                         dbo.LocalDrivingLicenseApplications ON dbo.TestAppointments.LocalDrivingLicenseApplicationID = dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID INNER JOIN
                         dbo.Applications ON dbo.LocalDrivingLicenseApplications.ApplicationID = dbo.Applications.ApplicationID INNER JOIN
                         dbo.People ON dbo.Applications.ApplicantPersonID = dbo.People.PersonID INNER JOIN
                         dbo.LicenseClasses ON dbo.LocalDrivingLicenseApplications.LicenseClassID = dbo.LicenseClasses.LicenseClassID
GO
/****** Object:  Table [dbo].[Licenses]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Licenses](
	[LicenseID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
	[LicenseClass] [int] NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IssueReason] [tinyint] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Licenses] PRIMARY KEY CLUSTERED 
(
	[LicenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[DriverID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[CreatedDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Drivers_1] PRIMARY KEY CLUSTERED 
(
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Drivers_View]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Drivers_View]
AS
SELECT        dbo.Drivers.DriverID, dbo.Drivers.PersonID, dbo.People.NationalNo, dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + ISNULL(dbo.People.ThirdName, '') + ' ' + dbo.People.LastName AS FullName, 
                         dbo.Drivers.CreatedDate,
                             (SELECT        COUNT(LicenseID) AS NumberOfActiveLicenses
                               FROM            dbo.Licenses
                               WHERE        (IsActive = 1) AND (DriverID = dbo.Drivers.DriverID)) AS NumberOfActiveLicenses
FROM            dbo.Drivers INNER JOIN
                         dbo.People ON dbo.Drivers.PersonID = dbo.People.PersonID
GO
/****** Object:  Table [dbo].[ApplicationTypes]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationTypes](
	[ApplicationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationTypeTitle] [nvarchar](150) NOT NULL,
	[ApplicationFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_ApplicationTypes] PRIMARY KEY CLUSTERED 
(
	[ApplicationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Countrie__10D160BFDBD6933F] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetainedLicenses]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetainedLicenses](
	[DetainID] [int] IDENTITY(1,1) NOT NULL,
	[LicenseID] [int] NOT NULL,
	[DetainDate] [smalldatetime] NOT NULL,
	[FineFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[IsReleased] [bit] NOT NULL,
	[ReleaseDate] [smalldatetime] NULL,
	[ReleasedByUserID] [int] NULL,
	[ReleaseApplicationID] [int] NULL,
 CONSTRAINT [PK_DetainedLicenses] PRIMARY KEY CLUSTERED 
(
	[DetainID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InternationalLicenses]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternationalLicenses](
	[InternationalLicenseID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
	[IssuedUsingLocalLicenseID] [int] NOT NULL,
	[IssueDate] [smalldatetime] NOT NULL,
	[ExpirationDate] [smalldatetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_InternationalLicenses] PRIMARY KEY CLUSTERED 
(
	[InternationalLicenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 26/1/2026 10:15:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Applications] ON 
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (43, 1, CAST(N'2023-09-23T20:17:32.483' AS DateTime), 1, 2, CAST(N'2023-09-23T20:17:59.753' AS DateTime), 20.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (45, 1, CAST(N'2023-09-23T20:32:17.010' AS DateTime), 1, 3, CAST(N'2023-09-23T20:44:05.453' AS DateTime), 20.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (46, 1, CAST(N'2023-09-23T20:39:42.570' AS DateTime), 1, 1, CAST(N'2023-09-23T20:39:42.570' AS DateTime), 50.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (47, 1, CAST(N'2023-09-23T20:45:14.393' AS DateTime), 6, 3, CAST(N'2023-09-23T20:45:14.393' AS DateTime), 50.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (48, 1024, CAST(N'2023-09-23T21:06:37.867' AS DateTime), 1, 1, CAST(N'2023-09-23T21:06:37.867' AS DateTime), 15.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (49, 1, CAST(N'2023-09-23T21:09:00.460' AS DateTime), 3, 3, CAST(N'2023-09-23T21:09:00.460' AS DateTime), 10.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (50, 1, CAST(N'2023-09-23T21:15:50.123' AS DateTime), 3, 3, CAST(N'2023-09-23T21:15:50.123' AS DateTime), 10.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (51, 1, CAST(N'2023-09-23T21:16:23.963' AS DateTime), 4, 3, CAST(N'2023-09-23T21:16:23.963' AS DateTime), 5.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (52, 1, CAST(N'2023-09-24T03:24:20.553' AS DateTime), 1, 3, CAST(N'2023-09-24T11:08:27.533' AS DateTime), 15.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (61, 1, CAST(N'2023-09-24T11:10:04.557' AS DateTime), 2, 3, CAST(N'2023-09-24T11:10:04.557' AS DateTime), 5.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (62, 1, CAST(N'2023-09-24T11:10:46.160' AS DateTime), 4, 3, CAST(N'2023-09-24T11:10:46.160' AS DateTime), 5.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (63, 1, CAST(N'2023-09-24T11:11:00.693' AS DateTime), 4, 3, CAST(N'2023-09-24T11:11:00.693' AS DateTime), 5.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (64, 1, CAST(N'2023-09-24T11:24:21.113' AS DateTime), 3, 3, CAST(N'2023-09-24T11:24:21.113' AS DateTime), 10.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (65, 1025, CAST(N'2023-09-24T13:44:30.513' AS DateTime), 1, 3, CAST(N'2023-09-24T13:52:51.067' AS DateTime), 15.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (66, 1025, CAST(N'2023-09-24T13:56:34.560' AS DateTime), 3, 3, CAST(N'2023-09-24T13:56:34.560' AS DateTime), 10.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (67, 1025, CAST(N'2023-09-24T13:58:43.560' AS DateTime), 4, 3, CAST(N'2023-09-24T13:58:43.560' AS DateTime), 5.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (68, 1025, CAST(N'2023-09-24T14:00:07.650' AS DateTime), 1, 1, CAST(N'2023-09-24T14:00:07.650' AS DateTime), 15.0000, 1)
GO
INSERT [dbo].[Applications] ([ApplicationID], [ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (69, 1025, CAST(N'2023-09-24T14:02:25.850' AS DateTime), 6, 3, CAST(N'2023-09-24T14:02:25.850' AS DateTime), 50.0000, 1)
GO
SET IDENTITY_INSERT [dbo].[Applications] OFF
GO
SET IDENTITY_INSERT [dbo].[ApplicationTypes] ON 
GO
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (1, N'New Local Driving License Service', 15.0000)
GO
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (2, N'Renew Driving License Service', 5.0000)
GO
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (3, N'Replacement for a Lost Driving License', 10.0000)
GO
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (4, N'Replacement for a Damaged Driving License', 5.0000)
GO
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (5, N'Release Detained Driving Licsense', 15.0000)
GO
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (6, N'New International License', 50.0000)
GO
SET IDENTITY_INSERT [dbo].[ApplicationTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (1, N'Afghanistan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (2, N'Albania')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (3, N'Algeria')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (4, N'Andorra')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (5, N'Angola')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (6, N'Antigua and Barbuda')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (7, N'Argentina')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (8, N'Armenia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (9, N'Austria')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (10, N'Azerbaijan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (11, N'Bahrain')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (12, N'Bangladesh')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (13, N'Barbados')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (14, N'Belarus')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (15, N'Belgium')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (16, N'Belize')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (17, N'Benin')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (18, N'Bhutan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (19, N'Bolivia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (20, N'Bosnia and Herzegovina')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (21, N'Botswana')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (22, N'Brazil')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (23, N'Brunei')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (24, N'Bulgaria')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (25, N'Burkina Faso')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (26, N'Burundi')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (27, N'Cabo Verde')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (28, N'Cambodia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (29, N'Cameroon')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (30, N'Canada')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (31, N'Central African Republic')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (32, N'Chad')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (33, N'Channel Islands')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (34, N'Chile')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (35, N'China')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (36, N'Colombia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (37, N'Comoros')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (38, N'Congo')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (39, N'Costa Rica')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (40, N'Côte d''Ivoire')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (41, N'Croatia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (42, N'Cuba')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (43, N'Cyprus')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (44, N'Czech Republic')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (45, N'Denmark')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (46, N'Djibouti')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (47, N'Dominica')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (48, N'Dominican Republic')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (49, N'DR Congo')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (50, N'Ecuador')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (51, N'Egypt')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (52, N'El Salvador')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (53, N'Equatorial Guinea')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (54, N'Eritrea')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (55, N'Estonia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (56, N'Eswatini')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (57, N'Ethiopia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (58, N'Faeroe Islands')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (59, N'Finland')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (60, N'France')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (61, N'French Guiana')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (62, N'Gabon')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (63, N'Gambia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (64, N'Georgia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (65, N'Germany')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (66, N'Ghana')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (67, N'Gibraltar')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (68, N'Greece')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (69, N'Grenada')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (70, N'Guatemala')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (71, N'Guinea')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (72, N'Guinea-Bissau')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (73, N'Guyana')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (74, N'Haiti')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (75, N'Holy See')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (76, N'Honduras')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (77, N'Hong Kong')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (78, N'Hungary')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (79, N'Iceland')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (80, N'India')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (81, N'Indonesia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (82, N'Iran')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (83, N'Iraq')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (84, N'Ireland')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (85, N'Isle of Man')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (86, N'Israel')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (87, N'Italy')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (88, N'Jamaica')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (89, N'Japan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (90, N'Jordan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (91, N'Kazakhstan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (92, N'Kenya')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (93, N'Kuwait')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (94, N'Kyrgyzstan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (95, N'Laos')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (96, N'Latvia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (97, N'Lebanon')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (98, N'Lesotho')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (99, N'Liberia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (100, N'Libya')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (101, N'Liechtenstein')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (102, N'Lithuania')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (103, N'Luxembourg')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (104, N'Macao')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (105, N'Madagascar')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (106, N'Malawi')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (107, N'Malaysia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (108, N'Maldives')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (109, N'Mali')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (110, N'Malta')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (111, N'Mauritania')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (112, N'Mauritius')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (113, N'Mayotte')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (114, N'Mexico')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (115, N'Moldova')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (116, N'Monaco')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (117, N'Mongolia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (118, N'Montenegro')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (119, N'Morocco')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (120, N'Mozambique')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (121, N'Myanmar')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (122, N'Namibia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (123, N'Nepal')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (124, N'Netherlands')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (125, N'Nicaragua')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (126, N'Niger')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (127, N'Nigeria')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (128, N'North Korea')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (129, N'North Macedonia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (130, N'Norway')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (131, N'Oman')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (132, N'Pakistan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (133, N'Panama')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (134, N'Paraguay')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (135, N'Peru')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (136, N'Philippines')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (137, N'Poland')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (138, N'Portugal')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (139, N'Qatar')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (140, N'Réunion')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (141, N'Romania')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (142, N'Russia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (143, N'Rwanda')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (144, N'Saint Helena')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (145, N'Saint Kitts and Nevis')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (146, N'Saint Lucia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (147, N'Saint Vincent and the Grenadines')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (148, N'San Marino')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (149, N'Sao Tome & Principe')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (150, N'Saudi Arabia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (151, N'Senegal')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (152, N'Serbia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (153, N'Seychelles')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (154, N'Sierra Leone')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (155, N'Singapore')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (156, N'Slovakia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (157, N'Slovenia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (158, N'Somalia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (159, N'South Africa')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (160, N'South Korea')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (161, N'South Sudan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (162, N'Spain')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (163, N'Sri Lanka')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (164, N'State of Palestine')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (165, N'Sudan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (166, N'Suriname')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (167, N'Sweden')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (168, N'Switzerland')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (169, N'Syria')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (170, N'Taiwan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (171, N'Tajikistan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (172, N'Tanzania')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (173, N'Thailand')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (174, N'The Bahamas')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (175, N'Timor-Leste')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (176, N'Togo')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (177, N'Trinidad and Tobago')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (178, N'Tunisia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (179, N'Turkey')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (180, N'Turkmenistan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (181, N'Uganda')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (182, N'Ukraine')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (183, N'United Arab Emirates')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (184, N'United Kingdom')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (185, N'United States')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (186, N'Uruguay')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (187, N'Uzbekistan')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (188, N'Venezuela')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (189, N'Vietnam')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (190, N'Western Sahara')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (191, N'Yemen')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (192, N'Zambia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (193, N'Zimbabwe')
GO
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[DetainedLicenses] ON 
GO
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (5, 14, CAST(N'2023-09-25T08:53:00' AS SmallDateTime), 56.0000, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (6, 15, CAST(N'2023-09-25T08:54:00' AS SmallDateTime), 60.0000, 1, 0, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[DetainedLicenses] OFF
GO
SET IDENTITY_INSERT [dbo].[Drivers] ON 
GO
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (8, 1, 1, CAST(N'2023-09-24T03:26:00' AS SmallDateTime))
GO
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (9, 1025, 1, CAST(N'2023-09-24T13:53:00' AS SmallDateTime))
GO
SET IDENTITY_INSERT [dbo].[Drivers] OFF
GO
SET IDENTITY_INSERT [dbo].[InternationalLicenses] ON 
GO
INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (12, 69, 9, 17, CAST(N'2023-09-24T14:02:00' AS SmallDateTime), CAST(N'2024-09-24T14:02:00' AS SmallDateTime), 1, 1)
GO
SET IDENTITY_INSERT [dbo].[InternationalLicenses] OFF
GO
SET IDENTITY_INSERT [dbo].[LicenseClasses] ON 
GO
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (1, N'Class 1 - Small Motorcycle', N'It allows the driver to drive small motorcycles, It is suitable for motorcycles with small capacity and limited power.', 18, 5, 15.0000)
GO
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (2, N'Class 2 - Heavy Motorcycle License', N'Heavy Motorcycle License (Large Motorcycle License)', 21, 5, 30.0000)
GO
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (3, N'Class 3 - Ordinary driving license', N'Ordinary driving license (car licence)', 18, 10, 20.0000)
GO
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (4, N'Class 4 - Commercial', N'Commercial driving license (taxi/limousine)', 21, 10, 200.0000)
GO
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (5, N'Class 5 - Agricultural', N'Agricultural and work vehicles used in farming or construction, (tractors / tillage machinery)', 21, 10, 50.0000)
GO
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (6, N'Class 6 - Small and medium bus', N'Small and medium bus license', 21, 10, 250.0000)
GO
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (7, N'Class 7 - Truck and heavy vehicle', N'Truck and heavy vehicle license', 21, 10, 300.0000)
GO
SET IDENTITY_INSERT [dbo].[LicenseClasses] OFF
GO
SET IDENTITY_INSERT [dbo].[Licenses] ON 
GO
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (10, 52, 8, 3, CAST(N'2020-09-24T11:08:27.533' AS DateTime), CAST(N'2021-09-24T11:08:27.533' AS DateTime), NULL, 20.0000, 0, 1, 1)
GO
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (11, 61, 8, 3, CAST(N'2023-09-24T11:10:04.567' AS DateTime), CAST(N'2033-09-24T11:10:04.567' AS DateTime), NULL, 20.0000, 0, 2, 1)
GO
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (12, 62, 8, 3, CAST(N'2023-09-24T11:10:46.170' AS DateTime), CAST(N'2033-09-24T11:10:04.567' AS DateTime), NULL, 0.0000, 0, 3, 1)
GO
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (13, 63, 8, 3, CAST(N'2023-09-24T11:11:00.703' AS DateTime), CAST(N'2033-09-24T11:10:04.567' AS DateTime), NULL, 0.0000, 0, 3, 1)
GO
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (14, 64, 8, 3, CAST(N'2023-09-24T11:24:21.117' AS DateTime), CAST(N'2033-09-24T11:10:04.567' AS DateTime), NULL, 0.0000, 1, 4, 1)
GO
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (15, 65, 9, 3, CAST(N'2023-09-24T13:52:51.063' AS DateTime), CAST(N'2033-09-24T13:52:51.063' AS DateTime), NULL, 20.0000, 0, 1, 1)
GO
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (16, 66, 9, 3, CAST(N'2023-09-24T13:56:34.573' AS DateTime), CAST(N'2033-09-24T13:52:51.063' AS DateTime), NULL, 0.0000, 0, 4, 1)
GO
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (17, 67, 9, 3, CAST(N'2023-09-24T13:58:43.570' AS DateTime), CAST(N'2033-09-24T13:52:51.063' AS DateTime), NULL, 0.0000, 1, 3, 1)
GO
SET IDENTITY_INSERT [dbo].[Licenses] OFF
GO
SET IDENTITY_INSERT [dbo].[LocalDrivingLicenseApplications] ON 
GO
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (30, 52, 3)
GO
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (31, 65, 3)
GO
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (32, 68, 7)
GO
SET IDENTITY_INSERT [dbo].[LocalDrivingLicenseApplications] OFF
GO
SET IDENTITY_INSERT [dbo].[People] ON 
GO
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gender], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1, N'N1', N'Mohammed', N'Saqer', N'Mussa', N'Abu-Hadhoud', CAST(N'1977-11-06T00:00:00.000' AS DateTime), 0, N'Amman Jubaiha1', N'999876', N'Msaqer@gmail.com', 90, N'C:\DVLD-People-Images\93776d4e-f437-4b5c-a9eb-a831b42af6eb.png')
GO
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gender], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1023, N'N2', N'Omar', N'Mohammed', N'Saqer', N'Abu-Hadhoud', CAST(N'2005-06-01T20:13:44.000' AS DateTime), 0, N'Amman 20091-Street', N'07992992', N'Omar@g.com', 90, NULL)
GO
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gender], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1024, N'N3', N'Hamzeh', N'Mohammed', N'Saqer', N'Abu-Hadhoud', CAST(N'2005-09-23T21:05:06.873' AS DateTime), 0, N'Amman', N'234566', N'H@H.com', 90, N'C:\DVLD-People-Images\c69c1ea5-2738-4f0b-baa5-b76810c5c9ee.png')
GO
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gender], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (1025, N'n4', N'Khalid', N'ALi', N'Maher', N'hamed', CAST(N'2005-09-24T13:32:14.183' AS DateTime), 0, N'Amman - Uni street 8938', N'566543', N'Kh@k.com', 90, N'C:\DVLD-People-Images\eefc59c8-9471-43a5-b786-f476fe7843af.jpg')
GO
SET IDENTITY_INSERT [dbo].[People] OFF
GO
SET IDENTITY_INSERT [dbo].[TestAppointments] ON 
GO
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (65, 1, 30, CAST(N'2023-09-24T03:25:00' AS SmallDateTime), 10.0000, 1, 1)
GO
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (66, 2, 30, CAST(N'2023-09-24T03:25:00' AS SmallDateTime), 20.0000, 1, 1)
GO
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (67, 3, 30, CAST(N'2023-09-24T03:25:00' AS SmallDateTime), 30.0000, 1, 1)
GO
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (68, 1, 31, CAST(N'2023-09-24T13:49:00' AS SmallDateTime), 10.0000, 1, 1)
GO
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (69, 2, 31, CAST(N'2023-09-24T13:50:00' AS SmallDateTime), 20.0000, 1, 1)
GO
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (70, 2, 31, CAST(N'2023-09-25T13:51:00' AS SmallDateTime), 20.0000, 1, 1)
GO
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked]) VALUES (71, 3, 31, CAST(N'2023-09-28T13:52:00' AS SmallDateTime), 30.0000, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[TestAppointments] OFF
GO
SET IDENTITY_INSERT [dbo].[Tests] ON 
GO
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (29, 65, 1, NULL, 1)
GO
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (30, 66, 1, NULL, 1)
GO
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (31, 67, 1, NULL, 1)
GO
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (32, 68, 1, N'with Glasses', 1)
GO
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (33, 69, 0, NULL, 1)
GO
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (34, 70, 1, NULL, 1)
GO
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (35, 71, 1, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Tests] OFF
GO
SET IDENTITY_INSERT [dbo].[TestTypes] ON 
GO
INSERT [dbo].[TestTypes] ([TestTypeID], [TestTypeTitle], [TestTypeDescription], [TestTypeFees]) VALUES (1, N'Vision Test', N'This assesses the applicant''s visual acuity to ensure they have sufficient vision to drive safely.', 10.0000)
GO
INSERT [dbo].[TestTypes] ([TestTypeID], [TestTypeTitle], [TestTypeDescription], [TestTypeFees]) VALUES (2, N'Written (Theory) Test', N'This test assesses the applicant''s knowledge of traffic rules, road signs, and driving regulations. It typically consists of multiple-choice questions, and the applicant must select the correct answer(s). The written test aims to ensure that the applicant understands the rules of the road and can apply them in various driving scenarios.', 20.0000)
GO
INSERT [dbo].[TestTypes] ([TestTypeID], [TestTypeTitle], [TestTypeDescription], [TestTypeFees]) VALUES (3, N'Practical (Street) Test', N'This test evaluates the applicant''s driving skills and ability to operate a motor vehicle safely on public roads. A licensed examiner accompanies the applicant in the vehicle and observes their driving performance.', 30.0000)
GO
SET IDENTITY_INSERT [dbo].[TestTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (1, 1, N'Msaqer77', N'1234', 1)
GO
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (15, 1025, N'user4', N'1234', 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_ApplicationStatus]  DEFAULT ((1)) FOR [ApplicationStatus]
GO
ALTER TABLE [dbo].[ApplicationTypes] ADD  CONSTRAINT [DF_ApplicationTypes_Fees]  DEFAULT ((0)) FOR [ApplicationFees]
GO
ALTER TABLE [dbo].[DetainedLicenses] ADD  CONSTRAINT [DF_DetainedLicenses_IsReleased]  DEFAULT ((0)) FOR [IsReleased]
GO
ALTER TABLE [dbo].[LicenseClasses] ADD  CONSTRAINT [DF_LicenseClasses_Age]  DEFAULT ((18)) FOR [MinimumAllowedAge]
GO
ALTER TABLE [dbo].[LicenseClasses] ADD  CONSTRAINT [DF_LicenseClasses_DefaultPeriodLength]  DEFAULT ((1)) FOR [DefaultValidityLength]
GO
ALTER TABLE [dbo].[LicenseClasses] ADD  CONSTRAINT [DF_LicenseClasses_ClassFees]  DEFAULT ((0)) FOR [ClassFees]
GO
ALTER TABLE [dbo].[Licenses] ADD  CONSTRAINT [DF_Licenses_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Licenses] ADD  CONSTRAINT [DF_Licenses_IssueReason]  DEFAULT ((1)) FOR [IssueReason]
GO
ALTER TABLE [dbo].[People] ADD  CONSTRAINT [DF_People_Gender]  DEFAULT ((0)) FOR [Gender]
GO
ALTER TABLE [dbo].[TestAppointments] ADD  CONSTRAINT [DF_TestAppointments_AppointmentLocked]  DEFAULT ((0)) FOR [IsLocked]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_ApplicationTypes] FOREIGN KEY([ApplicationTypeID])
REFERENCES [dbo].[ApplicationTypes] ([ApplicationTypeID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_ApplicationTypes]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_People] FOREIGN KEY([ApplicantPersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_People]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_Users]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Applications] FOREIGN KEY([ReleaseApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Applications]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Licenses] FOREIGN KEY([LicenseID])
REFERENCES [dbo].[Licenses] ([LicenseID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Licenses]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Users]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Users1] FOREIGN KEY([ReleasedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Users1]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK_Drivers_People] FOREIGN KEY([PersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK_Drivers_People]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK_Drivers_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK_Drivers_Users]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Applications]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Drivers] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Drivers]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Licenses] FOREIGN KEY([IssuedUsingLocalLicenseID])
REFERENCES [dbo].[Licenses] ([LicenseID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Licenses]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Users]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Applications]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Drivers] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Drivers]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_LicenseClasses] FOREIGN KEY([LicenseClass])
REFERENCES [dbo].[LicenseClasses] ([LicenseClassID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_LicenseClasses]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Users]
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications]  WITH CHECK ADD  CONSTRAINT [FK_DrivingLicsenseApplications_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications] CHECK CONSTRAINT [FK_DrivingLicsenseApplications_Applications]
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications]  WITH CHECK ADD  CONSTRAINT [FK_DrivingLicsenseApplications_LicenseClasses] FOREIGN KEY([LicenseClassID])
REFERENCES [dbo].[LicenseClasses] ([LicenseClassID])
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications] CHECK CONSTRAINT [FK_DrivingLicsenseApplications_LicenseClasses]
GO
ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Countries1] FOREIGN KEY([NationalityCountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Countries1]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_LocalDrivingLicenseApplications] FOREIGN KEY([LocalDrivingLicenseApplicationID])
REFERENCES [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_LocalDrivingLicenseApplications]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_TestTypes] FOREIGN KEY([TestTypeID])
REFERENCES [dbo].[TestTypes] ([TestTypeID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_TestTypes]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_Users]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_TestAppointments] FOREIGN KEY([TestAppointmentID])
REFERENCES [dbo].[TestAppointments] ([TestAppointmentID])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_TestAppointments]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_People] FOREIGN KEY([PersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_People]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-New 2-Cancelled 3-Completed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications', @level2type=N'COLUMN',@level2name=N'ApplicationStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Minmum age allowed to apply for this license' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LicenseClasses', @level2type=N'COLUMN',@level2name=N'MinimumAllowedAge'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'How many years the licesnse will be valid.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LicenseClasses', @level2type=N'COLUMN',@level2name=N'DefaultValidityLength'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Licenses', @level2type=N'COLUMN',@level2name=N'IssueReason'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Male , 1 Femail' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'People', @level2type=N'COLUMN',@level2name=N'Gender'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 - Fail 1-Pass' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tests', @level2type=N'COLUMN',@level2name=N'TestResult'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Drivers"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 215
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "People"
            Begin Extent = 
               Top = 6
               Left = 253
               Bottom = 136
               Right = 454
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Drivers_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Drivers_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "LocalDrivingLicenseApplications"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 309
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Applications"
            Begin Extent = 
               Top = 6
               Left = 347
               Bottom = 136
               Right = 534
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LicenseClasses"
            Begin Extent = 
               Top = 196
               Left = 343
               Bottom = 326
               Right = 549
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "People"
            Begin Extent = 
               Top = 6
               Left = 816
               Bottom = 136
               Right = 1017
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LocalDrivingLicenseApplications_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LocalDrivingLicenseApplications_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TestAppointments"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 304
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "TestTypes"
            Begin Extent = 
               Top = 6
               Left = 342
               Bottom = 136
               Right = 537
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LocalDrivingLicenseApplications"
            Begin Extent = 
               Top = 6
               Left = 575
               Bottom = 119
               Right = 841
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Applications"
            Begin Extent = 
               Top = 6
               Left = 879
               Bottom = 136
               Right = 1066
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "People"
            Begin Extent = 
               Top = 6
               Left = 1104
               Bottom = 136
               Right = 1305
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LicenseClasses"
            Begin Extent = 
               Top = 6
               Left = 1343
               Bottom = 136
               Right = 1549
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TestAppointments_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'= 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TestAppointments_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TestAppointments_View'
GO
