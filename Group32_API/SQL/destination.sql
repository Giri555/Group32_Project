
-- delete the database if it exists
IF EXISTS(SELECT * from sys.databases WHERE name='DestinationInfoDB') 
BEGIN 
    DROP DATABASE DestinationInfoDB; 
END 

CREATE DATABASE DestinationInfoDB;
GO

USE DestinationInfoDB
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.DestinationInfo (
    DestinationId      INT  IDENTITY (1, 1) PRIMARY KEY,
    Name               NVARCHAR (50)  NOT NULL,
    Address            NVARCHAR(50) NOT NULL,
    PhoneNumber        NVARCHAR(50) NOT NULL,
    Type               NVARCHAR (500) NULL,
    Description        NVARCHAR (500) NULL
);
GO

insert into DestinationInfo(Name,Address,Description, Type, PhoneNumber) values('Wing Spot','34 East Str, Scarborough, M7G 8U9', 'Chicken wings restaurant', 'Restaurant', '4186905543');
insert into DestinationInfo(Name,Address,Description, Type, PhoneNumber) values('La Scogliera Italian Cuisine','2277 Kingston Rd, Scarborough, ON M1N 1T8', 'Welcome to La Scogliera, we love Italian food - cooking it and serving it to our guests.', 'Restaurant', '5696905543');
insert into DestinationInfo(Name,Address,Description, Type, PhoneNumber) values('Kim Kim Restaurant', '1188 Kennedy Rd, Scarborough, ON M1P 2L1', 'Low-key eatery set in a strip mall serving Indian-influenced Chinese fare such as masala fried rice.', 'Restaurant', '2348764325');
insert into DestinationInfo(Name,Address,Description, Type, PhoneNumber) values('Pho Metro', '2057 Lawrence Ave E, Scarborough, ON M1R 2Z4', 'Pho Metro opened in Scarborough in the lovely neighbourhood between Lawrence Ave E. and Warden Ave in 2010.' , 'Restaurant', '8906905890');
insert into DestinationInfo(Name,Address,Description, Type, PhoneNumber) values('Slice of Fire Pizza', '60 Main St N, Markham, ON L3P 1X5', 'Daily dough. Hand-cut veggies. High quality meat. Only real %100 dairy cheeses. Hence superb pizzas', 'Restaurant', '7686905543');

CREATE TABLE dbo.Review (
    ReviewId      INT      IDENTITY (1, 1) PRIMARY KEY,
    Comment       NVARCHAR (1000) NULL,
    Rating        INT    NOT NULL,
    Email         NVARCHAR(50) NOT NULL,
    DateTime      NVARCHAR(50) NOT NULL,
    DestinationId INT NOT NULL,
    CONSTRAINT FK_Review_Destination_DestinationId FOREIGN KEY ([DestinationId]) REFERENCES DestinationInfo (DestinationId) ON DELETE CASCADE   
    );
    
 Go
 


