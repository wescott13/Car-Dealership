USE [GuildCars]
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DbReset')
      DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN

	DELETE FROM Specials;
	DELETE FROM PurchaseVehicle;
	--DELETE FROM VehicleInventory;
	DELETE FROM Vehicle;
	DELETE FROM VehicleModels;
	DELETE FROM VehicleMakes;
	DELETE FROM BodyStyles;
	DELETE FROM VehicleType;
	DELETE FROM Transmission;
	DELETE FROM ExteriorColor;
	DELETE FROM InteriorColor;
	DELETE FROM States;
	DELETE FROM Roles;
	DELETE FROM PurchaseTypes;
	DELETE FROM ContactUs;
	DELETE FROM AspNetUsers;
	DELETE FROM AspNetRoles;
	DELETE FROM AspNetUserRoles

	DELETE FROM AspNetUsers WHERE id IN ('03f2f18c-e5c5-4d17-a8f4-9c22060dce88', 'e33cead2-2d9a-4944-bec9-85f36c2ce364');

	DBCC CHECKIDENT ('Vehicle', RESEED, 1)
	DBCC CHECKIDENT ('Specials', RESEED, 1)
	DBCC CHECKIDENT ('BodyStyles', RESEED, 1)
	DBCC CHECKIDENT ('VehicleMakes', RESEED, 1)
	DBCC CHECKIDENT ('VehicleModels', RESEED, 1)
	DBCC CHECKIDENT ('PurchaseVehicle', RESEED, 1)
	DBCC CHECKIDENT ('ContactUs', RESEED, 1)

	SET IDENTITY_INSERT ContactUs ON;

	INSERT INTO ContactUs (ContactUsId, ContactName, Email, PhoneNumber,ContactUsMessage)
	VALUES (1, 'test','testContactUs1@test.com', '111-111-1111','test contact us message 1')

	SET IDENTITY_INSERT ContactUs OFF;

	SET IDENTITY_INSERT Specials ON;

	INSERT INTO Specials (SpecialId, Title, SpecialDescription)
	VALUES (1, 'SpecialTest_1', 'The Special Test 1.'),
	(2, 'SpecialTest_2', 'The Special Test 2.'),
	(3, 'SpecialTest_3', 'The Special Test 3.'),
	(4, 'SpecialTest_4', 'The Special Test 4.'),
	(5, 'SpecialTest_5', 'The Special Test 5.')

	SET IDENTITY_INSERT Specials OFF;

	SET IDENTITY_INSERT BodyStyles ON;

	INSERT INTO BodyStyles (BodyStyleId, BodyStyleName)
	VALUES (1, 'Car'),
	(2, 'SUV'),
	(3, 'Truck'),
	(4, 'Van')

	SET IDENTITY_INSERT BodyStyles OFF;

	SET IDENTITY_INSERT VehicleMakes ON;

	INSERT INTO VehicleMakes (MakeId, MakeName, UserEmail)
	VALUES (1, 'Audi', 'test@test.com'),
	(2, 'BMW', 'test@test.com'),
	(3, 'Ford', 'test@test.com'),
	(4, 'Mazda', 'test@test.com'),
	(5, 'RAM', 'test@test.com'),
	(6, 'Subaru', 'test@test.com'),
	(7, 'Toyota', 'test@test.com'),
	(8, 'Tesla', 'test@test.com'),
	(9, 'Honda', 'test@test.com')

	SET IDENTITY_INSERT VehicleMakes OFF;

	SET IDENTITY_INSERT VehicleModels ON;

	INSERT INTO VehicleModels (ModelId, MakeId, ModelTypeName, UserEmail)
	VALUES (1, 1, 'A1','test@test.com'),
	(2, 1, 'A4','test@test.com'),
	(3, 2, 'X3 Sports Activity Coupe','test@test.com'),
	(4, 2, '7 Series Sedan','test@test.com'),
	(5, 3, 'DB test model','test@test.com'),
	(6, 4, 'DB test model','test@test.com'),
	(7, 5, 'DB test model','test@test.com'),
	(8, 6, 'DB test model','test@test.com'),
	(9, 7, 'DB test model','test@test.com'),
	(10, 8, 'DB test model','test@test.com'),
	(11, 9, 'DB test model','test@test.com'),
	(12, 2, '4 Series Coupe','test@test.com')

	SET IDENTITY_INSERT VehicleModels OFF;

	SET IDENTITY_INSERT Transmission ON;

	INSERT INTO Transmission (TransmissionId, TransmissionTypeName)
	VALUES (1, 'Automatic'),
	(2, 'Manual')
	
	SET IDENTITY_INSERT Transmission OFF;

	SET IDENTITY_INSERT ExteriorColor ON;

	INSERT INTO ExteriorColor (ExteriorColorId, ExteriorColorName)
	VALUES (1, 'Yellow'),
	(2, 'Yellow-Green'),
	(3, 'Green'),
	(4, 'Blue-Green'),
	(5, 'Blue'),
	(6, 'Blue-Violet'),
	(7, 'Violet'),
	(8, 'Red-Violet'),
	(9, 'Red'),
	(10, 'Red-Orange'),
	(11, 'Orange'),
	(12, 'Yellow-Orange'),
	(13, 'Black'),
	(14, 'White'),
	(15, 'Tan')

	SET IDENTITY_INSERT ExteriorColor OFF;

	SET IDENTITY_INSERT InteriorColor ON;

	INSERT INTO InteriorColor (InteriorColorId, InteriorColorName)
	VALUES (1, 'Black'),
	(2, 'Gray'),
	(3, 'Tan'),
	(4, 'Blue'),
	(5, 'White')

	SET IDENTITY_INSERT InteriorColor OFF;

	SET IDENTITY_INSERT VehicleType ON;

	INSERT INTO VehicleType (TypeId, TypeName)
	VALUES (1, 'New'),
	(2, 'Used')

	SET IDENTITY_INSERT VehicleType OFF;

	SET IDENTITY_INSERT Vehicle ON;

	INSERT INTO Vehicle(VehicleId, MakeId, ModelId, TypeId, BodyStyleId, TransmissionId, ExteriorColorId, InteriorColorId, Mileage, VIN,
		MSRP, SalePrice, VehicleYear, VehicleDescription, HasFeatured, ImageFileName)
	VALUES 
	(1, 1, 1, 1, 1, 1, 1, 1, 0, 'VinTest1', 5000, 1000, 1970, 'Test Vehicle 1 description.', 0, 'placeholder1.png'),
	(3, 1, 2, 1, 1, 1, 1, 1, 0, 'VinTest3', 10000, 5000, 1980, 'Test Vehicle 3 description.', 0, 'placeholder2.png'),
	(4, 1, 1, 1, 1, 1, 1, 1, 0, 'VinTest4', 12000, 10000, 1990, 'Test Vehicle 4 description.', 0, 'placeholder3.png'),
	(5, 1, 2, 1, 1, 1, 1, 1, 0, 'VinTest3', 18000, 15000, 2000, 'Test Vehicle 5 description.', 0, 'placeholder4.png'),
	(8, 1, 2, 1, 1, 1, 1, 1, 0, 'VinTest5', 19000, 15000, 2000, 'Test Vehicle 8 description.', 0, 'test.png'),
	 (2, 2, 1, 2, null, null, null, null, 1000, 'VinTest2', 25000, 20000, 2005, 'Test Vehicle 2 description.', 1, 'placeholder5.png'),
	 (6, 2, 3, 2, null, null, null, null, 1000, 'VinTest6', 30000, 25000, 2020, 'Test Vehicle 6 description.', 1, 'placeholder6.png'),
	 (7, 3, 5, 2, null, null, null, null, 1000, 'VinTest7', 60000, 60000, 2022, 'Test Vehicle 6 description.', 1, 'placeholder7.png')

	SET IDENTITY_INSERT Vehicle OFF;

	INSERT INTO States (StateId, StateName)
	VALUES ('AK', 'Alaska'),
('AL', 'Alabama'),
('AZ', 'Arizona'),
('AR', 'Arkansas'),
('CA', 'California'),
('CO', 'Colorado'),
('CT', 'Connecticut'),
('DE', 'Delaware'),
('DC', 'District of Columbia'),
('FL', 'Florida'),
('GA', 'Georgia'),
('HI', 'Hawaii'),
('ID', 'Idaho'),
('IL', 'Illinois'),
('IN', 'Indiana'),
('IA', 'Iowa'),
('KS', 'Kansas'),
('KY', 'Kentucky'),
('LA', 'Louisiana'),
('ME', 'Maine'),
('MD', 'Maryland'),
('MA', 'Massachusetts'),
('MI', 'Michigan'),
('MN', 'Minnesota'),
('MS', 'Mississippi'),
('MO', 'Missouri'),
('MT', 'Montana'),
('NE', 'Nebraska'),
('NV', 'Nevada'),
('NH', 'New Hampshire'),
('NJ', 'New Jersey'),
('NM', 'New Mexico'),
('NY', 'New York'),
('NC', 'North Carolina'),
('ND', 'North Dakota'),
('OH', 'Ohio'),
('OK', 'Oklahoma'),
('OR', 'Oregon'),
('PA', 'Pennsylvania'),
('PR', 'Puerto Rico'),
('RI', 'Rhode Island'),
('SC', 'South Carolina'),
('SD', 'South Dakota'),
('TN', 'Tennessee'),
('TX', 'Texas'),
('UT', 'Utah'),
('VT', 'Vermont'),
('VA', 'Virginia'),
('WA', 'Washington'),
('WV', 'West Virginia'),
('WI', 'Wisconsin'),
('WY', 'Wyoming');

SET IDENTITY_INSERT Roles ON;

	INSERT INTO Roles(RoleId, RoleName)
	VALUES 
	(3, 'Disabled'),
	(1, 'Admin'),
	(2, 'Sales')

SET IDENTITY_INSERT Roles OFF;

INSERT INTO AspNetRoles(Id, Name)
	VALUES ('3', 'Disabled');

INSERT INTO AspNetRoles(Id, Name)
	VALUES ('1', 'Admin');

INSERT INTO AspNetRoles(Id, Name)
	VALUES ('2', 'Sales');

INSERT INTO AspNetUsers(Id, Email, EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,UserName, RoleId, FirstName, LastName, PasswordHash, SecurityStamp)
	VALUES ('03f2f18c-e5c5-4d17-a8f4-9c22060dce88', 'sales@test.com', 0, 0, 0, 0, 0, 'sales@test.com', 2, 'FirstnameSales','LastnameSales','AGtpsUBJuwUvbWvFVd5a8zSP7/NZ3crgBh+9CjyBy3yUQY2/trhpFoe3R0C+CbedwQ==','285039ad-ae1b-458f-872e-2752ceec3f9c');

INSERT INTO AspNetUsers(Id, Email, EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,UserName, RoleId, FirstName, LastName, PasswordHash, SecurityStamp)
	VALUES ('e33cead2-2d9a-4944-bec9-85f36c2ce364', 'admin@test.com', 0, 0, 0, 0, 0, 'admin@test.com', 1, 'FirstnameAdmin','LastnameAdmin','AKUKsEoQfpsb34VmnAufO9b4guHWAOaJeYWVvw5+rYBuMLmxYCVrryV/0C8EGWn2bQ==','855e85fb-4094-411b-a127-afc4c556aeda');


Insert Into AspNetUserRoles (UserId,RoleId) Values('e33cead2-2d9a-4944-bec9-85f36c2ce364',1);

Insert Into AspNetUserRoles (UserId,RoleId) Values('03f2f18c-e5c5-4d17-a8f4-9c22060dce88',2);

SET IDENTITY_INSERT PurchaseTypes ON;

	INSERT INTO PurchaseTypes(PurchaseTypeId, PurchaseTypeName)
	VALUES (1, 'Bank Finance'),
	(2, 'Cash'),
	(3, 'Dealer Finance')

SET IDENTITY_INSERT PurchaseTypes OFF;

SET IDENTITY_INSERT PurchaseVehicle ON;

INSERT INTO PurchaseVehicle(PurchaseId, UserId, PurchaseName, PhoneNumber, Email, Street1, Street2, City, StateId, Zipcode, PurchasePrice,
		PurchaseTypeId, PurchaseDate)
	VALUES (1, 'e33cead2-2d9a-4944-bec9-85f36c2ce364', 'admin@test.com', '111-111-1111', 'testpurchase1@test.com', 'testpurchase1street1', 'testpurchases1street2', 'TestCity', 'OH', 
	'11111', 10000, 1, '01-01-01'),
	(2, '03f2f18c-e5c5-4d17-a8f4-9c22060dce88', 'sales@test.com', '222-222-2222', 'testpurchase2@test.com', 'testpurchase2street1', null, 'TestCity', 'MN', 
	'22222', 20000, 2, '02-02-02'),
	(3, '03f2f18c-e5c5-4d17-a8f4-9c22060dce88', 'sales@test.com', '333-333-3333', 'testpurchase3@test.com', 'testpurchase3street1', null, 'TestCity', 'AK', 
	'33333', 30000, 3, '03-03-03');

	--INSERT INTO AspNetUsers(Id, Email, EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,UserName, RoleId, FirstName, LastName, PasswordHash, SecurityStamp)
	--VALUES ('e33cead2-2d9a-4944-bec9-85f36c2ce364', 'admin@test.com', 0, 0, 0, 0, 0, 'admintest', 1, 'FirstnameAdmin','LastnameAdmin','AKUKsEoQfpsb34VmnAufO9b4guHWAOaJeYWVvw5+rYBuMLmxYCVrryV/0C8EGWn2bQ==','855e85fb-4094-411b-a127-afc4c556aeda');

	--INSERT INTO AspNetUsers(Id, Email, EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,UserName, RoleId, FirstName, LastName, PasswordHash, SecurityStamp)
	--VALUES ('03f2f18c-e5c5-4d17-a8f4-9c22060dce88', 'sales@test.com', 0, 0, 0, 0, 0, 'salestest', 2, 'FirstnameSales','LastnameSales','AGtpsUBJuwUvbWvFVd5a8zSP7/NZ3crgBh+9CjyBy3yUQY2/trhpFoe3R0C+CbedwQ==','285039ad-ae1b-458f-872e-2752ceec3f9c');


SET IDENTITY_INSERT PurchaseVehicle OFF;


END

