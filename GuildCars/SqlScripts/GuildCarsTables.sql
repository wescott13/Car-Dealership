USE GuildCars
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ContactUs')
	DROP TABLE ContactUs
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Specials')
	DROP TABLE Specials
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseVehicle')
	DROP TABLE PurchaseVehicle
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Vehicle')
	DROP TABLE Vehicle
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='VehicleModels')
	DROP TABLE VehicleModels
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='VehicleMakes')
	DROP TABLE VehicleMakes
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BodyStyles')
	DROP TABLE BodyStyles
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='VehicleType')
	DROP TABLE VehicleType
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Transmission')
	DROP TABLE Transmission
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ExteriorColor')
	DROP TABLE ExteriorColor
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='InteriorColor')
	DROP TABLE InteriorColor
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='States')
	DROP TABLE States
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Roles')
	DROP TABLE Roles
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseTypes')
	DROP TABLE PurchaseTypes
GO

CREATE TABLE Specials (
	SpecialId int identity(1,1) not null primary key,
	Title nvarchar(128) not null,
	SpecialDescription varchar(500) null,
)

CREATE TABLE VehicleType (
	TypeId int identity(1,1) not null primary key,
	TypeName varchar(15) not null
)

CREATE TABLE BodyStyles (
	BodyStyleId int identity(1,1) not null primary key,
	BodyStyleName varchar(15) not null
)

CREATE TABLE VehicleMakes (
	MakeId int identity(1,1) not null primary key,
	MakeName varchar(50) not null,
	CreatedDate datetime2 not null default(getdate()),
	UserEmail nvarchar(256) not null
)

CREATE TABLE VehicleModels (
	ModelId int identity(1,1) not null primary key,
	MakeId int not null foreign key references VehicleMakes(MakeId),
	ModelTypeName varchar(50) not null,
	CreatedDate datetime2 not null default(getdate()),
	UserEmail nvarchar(256) not null
)

CREATE TABLE Transmission (
	TransmissionId int identity(1,1) not null primary key,
	TransmissionTypeName varchar(50) not null
)

CREATE TABLE ExteriorColor (
	ExteriorColorId int identity(1,1) not null primary key,
	ExteriorColorName varchar(50) not null
)
CREATE TABLE InteriorColor (
	InteriorColorId int identity(1,1) not null primary key,
	InteriorColorName varchar(50) not null
)

CREATE TABLE Vehicle (
	VehicleId int identity(1,1) not null primary key,
	MakeId int not null foreign key references VehicleMakes(MakeId),
	ModelId int not null foreign key references VehicleModels(ModelId),
	TypeId int null foreign key references VehicleType(TypeId),
	BodyStyleId int null foreign key references BodyStyles(BodyStyleId),
	TransmissionId int null foreign key references Transmission(TransmissionId),
	ExteriorColorId int null foreign key references ExteriorColor(ExteriorColorId),
	InteriorColorId int null foreign key references InteriorColor(InteriorColorId),
	Mileage int,
	VIN varchar(50) not null,
	MSRP decimal(8,0) not null,
	SalePrice decimal(8,0) not null,
	VehicleDescription varchar(500) null,
	VehicleYear int,
	HasFeatured bit not null,
	ImageFileName varchar(50)
)

CREATE TABLE PurchaseTypes (
	PurchaseTypeId int identity(1,1) not null primary key,
	PurchaseTypeName varchar(50) not null
)

CREATE TABLE ContactUs (
	ContactUsId int identity(1,1) not null primary key,
	ContactName nvarchar(256) null,
	Email nvarchar(256) null,
	PhoneNumber nvarchar(max) null,
	ContactUsMessage varchar(500) not null,
)

CREATE TABLE States (
	StateId nvarchar(2) not null primary key,
	StateName varchar(20) not null
)

CREATE TABLE Roles (
	RoleId int identity(1,1) not null primary key,
	RoleName varchar(15) not null
)

CREATE TABLE PurchaseVehicle(
	PurchaseId int identity(1,1) primary key,
	UserId nvarchar(128) not null,
	PurchaseName nvarchar(128) not null,
	PhoneNumber nvarchar(max) null,
	Email nvarchar(256) null,
	Street1  varchar(128) not null,
	Street2  varchar(128) null,
	City nvarchar(50) not null,
	StateId nvarchar(2) not null foreign key references States(StateId),
	Zipcode nvarchar (10) not null,
	PurchasePrice decimal(8,0) not null,
	PurchaseTypeId int null foreign key references PurchaseTypes(PurchaseTypeId),
	PurchaseDate datetime2 null default(getdate()),
	
)















