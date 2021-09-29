USE GuildCars

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleTypeSelectAll')
      DROP PROCEDURE VehicleTypeSelectAll
GO

CREATE PROCEDURE VehicleTypeSelectAll AS
BEGIN
	SELECT TypeId, TypeName
	FROM VehicleType
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'BodyStyleSelectAll')
      DROP PROCEDURE BodyStyleSelectAll
GO

CREATE PROCEDURE BodyStyleSelectAll AS
BEGIN
	SELECT BodyStyleId, BodyStyleName
	FROM BodyStyles
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'TransmissionSelectAll')
      DROP PROCEDURE TransmissionSelectAll
GO

CREATE PROCEDURE TransmissionSelectAll AS
BEGIN
	SELECT TransmissionId, TransmissionTypeName
	FROM Transmission
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ExteriorColorSelectAll')
      DROP PROCEDURE ExteriorColorSelectAll
GO

CREATE PROCEDURE ExteriorColorSelectAll AS
BEGIN
	SELECT ExteriorColorId, ExteriorColorName
	FROM ExteriorColor
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'InteriorColorSelectAll')
      DROP PROCEDURE InteriorColorSelectAll
GO

CREATE PROCEDURE InteriorColorSelectAll AS
BEGIN
	SELECT InteriorColorId, InteriorColorName
	FROM InteriorColor
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'StatesSelectAll')
      DROP PROCEDURE StatesSelectAll
GO

CREATE PROCEDURE StatesSelectAll AS
BEGIN
	SELECT StateId, StateName
	FROM States
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'PurchaseTypesSelectAll')
      DROP PROCEDURE PurchaseTypesSelectAll
GO

CREATE PROCEDURE PurchaseTypesSelectAll AS
BEGIN
	SELECT PurchaseTypeId, PurchaseTypeName
	FROM PurchaseTypes
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialsSelectAll')
      DROP PROCEDURE SpecialsSelectAll
GO

CREATE PROCEDURE SpecialsSelectAll AS
BEGIN
	SELECT SpecialId, Title, SpecialDescription 
	FROM Specials
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialsInsert')
      DROP PROCEDURE SpecialsInsert
GO

CREATE PROCEDURE SpecialsInsert (
	@SpecialId int output,
	@Title nvarchar(128),
	@SpecialDescription varchar(500)
) AS
BEGIN
	INSERT INTO Specials(Title, SpecialDescription)
	VALUES (@Title, @SpecialDescription);

	SET @SpecialId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialsDelete')
      DROP PROCEDURE SpecialsDelete
GO

CREATE PROCEDURE SpecialsDelete (
	@SpecialId int
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Specials WHERE SpecialId = @SpecialId;

	COMMIT TRANSACTION

END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactUsInsert')
      DROP PROCEDURE ContactUsInsert
GO

CREATE PROCEDURE ContactUsInsert (
	@ContactUsId int output,
	@ContactName nvarchar(256),
	@Email nvarchar(256),
	@PhoneNumber nvarchar(max),
	@ContactUsMessage varchar(500)

) AS
BEGIN
	INSERT INTO ContactUs(ContactName,Email, PhoneNumber, ContactUsMessage)
	VALUES (@ContactName, @Email, @PhoneNumber, @ContactUsMessage);

	SET @ContactUsId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'UsersSelectAll')
      DROP PROCEDURE UsersSelectAll
GO

CREATE PROCEDURE UsersSelectAll AS
BEGIN
	SELECT Id, LastName, FirstName, Email, uRole.RoleId, RoleName
	FROM AspNetUsers uRole
		INNER JOIN Roles r ON uRole.RoleId = r.RoleId
	ORDER By uRole.LastName ASC
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakesSelectAll')
      DROP PROCEDURE MakesSelectAll
GO

CREATE PROCEDURE MakesSelectAll AS
BEGIN
	SELECT MakeId, MakeName, CreatedDate, UserEmail
	FROM VehicleMakes
	ORDER By MakeName ASC
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakesInsert')
      DROP PROCEDURE MakesInsert
GO

CREATE PROCEDURE MakesInsert (
	@MakeId int output,
	@MakeName varchar(50),
	@UserEmail nvarchar(256),
	@CreatedDate datetime2 = null
) AS

IF @CreatedDate is null
SET @CreatedDate = getdate()

BEGIN
	
	INSERT INTO VehicleMakes(MakeName, UserEmail, CreatedDate)
	VALUES (@MakeName, @UserEmail, @CreatedDate);

	SET @MakeId = SCOPE_IDENTITY();
	
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelsSelectAll')
      DROP PROCEDURE ModelsSelectAll
GO

CREATE PROCEDURE ModelsSelectAll AS
BEGIN
	SELECT ModelId, vmodel.MakeId, ModelTypeName, vmodel.CreatedDate,vmodel.UserEmail, vmake.MakeName
	FROM VehicleModels vmodel
		INNER JOIN VehicleMakes vmake ON vmodel.MakeId = vmake.MakeId
	ORDER By vmake.MakeName ASC, ModelTypeName ASC;
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelsInsert')
      DROP PROCEDURE ModelsInsert
GO

CREATE PROCEDURE ModelsInsert (
	@ModelId int output,
	@ModelTypeName varchar(50),
	@MakeId int,
	@UserEmail nvarchar(256),
	@CreatedDate datetime2 = null
) AS

IF @CreatedDate is null
SET @CreatedDate = getdate()

BEGIN
	INSERT INTO VehicleModels(ModelTypeName, MakeId, UserEmail, CreatedDate)
	VALUES (@ModelTypeName, @MakeId, @UserEmail, @CreatedDate);

	SET @ModelId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SalesReport')
      DROP PROCEDURE SalesReport
GO

CREATE PROCEDURE SalesReport AS
BEGIN
	SELECT UserId, U.FirstName, U.LastName, PV.PurchasePrice, PV.PurchaseDate
	FROM PurchaseVehicle PV
		INNER JOIN AspNetUsers U ON PV.UserId = U.Id
		
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'NewInventorySelect20')
      DROP PROCEDURE NewInventorySelect20
GO

CREATE PROCEDURE NewInventorySelect20 AS
BEGIN
	SELECT TOP 20 *
	FROM Vehicle V 
		INNER JOIN VehicleMakes vmake ON V.MakeId = vmake.MakeId
		INNER JOIN VehicleModels vmodel ON V.ModelId = vmodel.ModelId
		INNER JOIN BodyStyles vbodystyles ON V.BodyStyleId = vbodystyles.BodyStyleId
		INNER JOIN Transmission vtransmission ON V.TransmissionId = vtransmission.TransmissionId
		INNER JOIN ExteriorColor vExterColor ON V.ExteriorColorId = vExterColor.ExteriorColorId	
		INNER JOIN InteriorColor vInterColor ON V.InteriorColorId = vInterColor.InteriorColorId
		--INNER JOIN VehicleType vType ON V.TypeId = vType.TypeId

	WHERE TypeId = 1
	ORDER by MSRP DESC	
	
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'UsedInventorySelect20')
      DROP PROCEDURE UsedInventorySelect20
GO

CREATE PROCEDURE UsedInventorySelect20 AS
BEGIN
	SELECT TOP 20 *
	FROM Vehicle V 
		INNER JOIN VehicleMakes vmake ON V.MakeId = vmake.MakeId
		LEFT JOIN VehicleModels vmodel ON V.ModelId = vmodel.ModelId
		LEFT JOIN BodyStyles vbodystyles ON V.BodyStyleId = vbodystyles.BodyStyleId
		LEFT JOIN Transmission vtransmission ON V.TransmissionId = vtransmission.TransmissionId
		LEFT JOIN ExteriorColor vExterColor ON V.ExteriorColorId = vExterColor.ExteriorColorId	
		LEFT JOIN InteriorColor vInterColor ON V.InteriorColorId = vInterColor.InteriorColorId

	WHERE TypeId = 2
	ORDER by MSRP DESC	
	
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleInventorySelect20')
      DROP PROCEDURE VehicleInventorySelect20
GO

CREATE PROCEDURE VehicleInventorySelect20 AS
BEGIN
	SELECT TOP 20 *
	FROM Vehicle V 
		INNER JOIN VehicleMakes vmake ON V.MakeId = vmake.MakeId
		LEFT JOIN VehicleModels vmodel ON V.ModelId = vmodel.ModelId
		LEFT JOIN BodyStyles vbodystyles ON V.BodyStyleId = vbodystyles.BodyStyleId
		LEFT JOIN Transmission vtransmission ON V.TransmissionId = vtransmission.TransmissionId
		LEFT JOIN ExteriorColor vExterColor ON V.ExteriorColorId = vExterColor.ExteriorColorId	
		LEFT JOIN InteriorColor vInterColor ON V.InteriorColorId = vInterColor.InteriorColorId

	ORDER by MSRP DESC	
	
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleInsert')
      DROP PROCEDURE VehicleInsert
GO

CREATE PROCEDURE VehicleInsert (
	@VehicleId int output,
	@MakeId int,
	@ModelId int,
	@TypeId int,
	@BodyStyleId int,
	@TransmissionId int,
	@ExteriorColorId int,
	@InteriorColorId int,
	@Mileage int,
	@VIN varchar(50),
	@MSRP decimal(8,0),
	@SalePrice decimal(8,0),
	@VehicleDescription varchar(500),
	@VehicleYear int,
	@HasFeatured bit,
	@ImageFileName varchar(50)
	
) AS
BEGIN
	INSERT INTO Vehicle(MakeId, ModelId, TypeId, BodyStyleId, TransmissionId, ExteriorColorId, InteriorColorId, Mileage, VIN,
	MSRP, SalePrice, VehicleDescription, VehicleYear, HasFeatured, ImageFileName)
	VALUES (@MakeId, @ModelId, @TypeId, @BodyStyleId, @TransmissionId, @ExteriorColorId, @InteriorColorId, @Mileage, 
	@VIN, @MSRP, @SalePrice, @VehicleDescription, @VehicleYear, @HasFeatured, @ImageFileName);

	SET @VehicleId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleSale')
      DROP PROCEDURE VehicleSale
GO

CREATE PROCEDURE VehicleSale (
	@PurchaseId int output,
	@UserId nvarchar(128),
	@PurchaseName nvarchar(128),
	@PhoneNumber nvarchar(max),
	@Email nvarchar(256),
	@Street1  varchar(128),
	@Street2  varchar(128),
	@City nvarchar(50),
	@StateId nvarchar(2),
	@Zipcode varchar(5),
	@PurchasePrice decimal(8,0),
	@PurchaseTypeId int,
	@PurchaseDate datetime2 = null
	
) AS

IF @PurchaseDate is null
SET @PurchaseDate = getdate()

BEGIN
	INSERT INTO PurchaseVehicle(UserId, PurchaseName, PhoneNumber, Email, Street1, Street2, City, StateId, Zipcode, PurchasePrice, PurchaseTypeId, PurchaseDate)
	VALUES (@UserId, @PurchaseName, @PhoneNumber, @Email, @Street1, @Street2, @City, @StateId, @Zipcode, 
	@PurchasePrice, @PurchaseTypeId, @PurchaseDate);

	SET @PurchaseId = SCOPE_IDENTITY()

END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleDelete')
      DROP PROCEDURE VehicleDelete
GO

CREATE PROCEDURE VehicleDelete (
	@VehicleId int
) AS
BEGIN
	BEGIN TRANSACTION
	DELETE FROM Vehicle WHERE VehicleId = @VehicleId;

	COMMIT TRANSACTION

END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleSelect')
      DROP PROCEDURE VehicleSelect
GO

CREATE PROCEDURE VehicleSelect (
	@VehicleId int
) AS
BEGIN
	SELECT *
	FROM Vehicle V 
		INNER JOIN VehicleMakes vmake ON V.MakeId = vmake.MakeId
		LEFT JOIN VehicleModels vmodel ON V.ModelId = vmodel.ModelId
		LEFT JOIN BodyStyles vbodystyles ON V.BodyStyleId = vbodystyles.BodyStyleId
		LEFT JOIN Transmission vtransmission ON V.TransmissionId = vtransmission.TransmissionId
		LEFT JOIN ExteriorColor vExterColor ON V.ExteriorColorId = vExterColor.ExteriorColorId	
		LEFT JOIN InteriorColor vInterColor ON V.InteriorColorId = vInterColor.InteriorColorId
	WHERE VehicleId = @VehicleId

END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'FeaturedVehicles')
      DROP PROCEDURE FeaturedVehicles
GO

CREATE PROCEDURE FeaturedVehicles AS
BEGIN
	SELECT VehicleId, ImageFileName, VehicleYear, vmake.MakeName, vmodel.ModelTypeName, SalePrice
	
	FROM Vehicle V
		INNER JOIN VehicleMakes vmake ON V.MakeId = vmake.MakeId
		INNER JOIN VehicleModels vmodel ON V.ModelId = vmodel.ModelId
	WHERE HasFeatured = 1
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleUpdate')
      DROP PROCEDURE VehicleUpdate
GO

CREATE PROCEDURE VehicleUpdate (
	@VehicleId int,
	@MakeId int,
	@ModelId int,
	@TypeId int,
	@BodyStyleId int,
	@TransmissionId int,
	@ExteriorColorId int,
	@InteriorColorId int,
	@Mileage int,
	@VIN varchar(50),
	@MSRP decimal(8,0),
	@SalePrice decimal(8,0),
	@VehicleDescription varchar(500),
	@VehicleYear int,
	@HasFeatured bit,
	@ImageFileName varchar(50)
) AS
BEGIN
	UPDATE Vehicle SET
		MakeId = @MakeId,
		ModelId = @ModelId, 
		TypeId = @TypeId, 
		BodyStyleId = @BodyStyleId,
		TransmissionId = @TransmissionId,
		ExteriorColorId = @ExteriorColorId,
		InteriorColorId = @InteriorColorId,
		Mileage = @Mileage,
		VIN = @VIN,
		MSRP = @MSRP,
		SalePrice = @SalePrice,
		VehicleDescription = @VehicleDescription,
		VehicleYear = @VehicleYear,
		HasFeatured = @HasFeatured,
		ImageFileName = @ImageFileName

	WHERE VehicleId = @VehicleId	
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'NewVehicleInventory')
		DROP PROCEDURE NewVehicleInventory
GO

CREATE PROCEDURE NewVehicleInventory AS 
BEGIN
SELECT VehicleYear, MakeName, ModelTypeName, COUNT(Vehicle.ModelId) As vehicleCount, SUM(MSRP) AS Stockvalue
	
	FROM Vehicle
		inner join VehicleMakes ON Vehicle.MakeId = VehicleMakes.MakeId 
	left join VehicleModels ON Vehicle.ModelId = VehicleModels.ModelId 
	WHERE TypeId = 1
	GROUP BY VehicleYear, MakeName, ModelTypeName
	order by VehicleYear desc
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UsedVehicleInventory')
		DROP PROCEDURE UsedVehicleInventory
GO

CREATE PROCEDURE UsedVehicleInventory AS 
BEGIN
SELECT VehicleYear, MakeName, ModelTypeName, COUNT(Vehicle.ModelId) As vehicleCount, SUM(MSRP) AS Stockvalue
	
	FROM Vehicle
		inner join VehicleMakes ON Vehicle.MakeId = VehicleMakes.MakeId 
	left join VehicleModels ON Vehicle.ModelId = VehicleModels.ModelId 
	WHERE TypeId = 2
	GROUP BY VehicleYear, MakeName, ModelTypeName
	order by VehicleYear desc
END


