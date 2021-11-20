-- ############### THIS IS A TEMPLATE ##############

-- delete the database if it exists
IF EXISTS(SELECT * from sys.databases WHERE name='CityInfoDB') 
BEGIN 
    DROP DATABASE CityInfoDB; 
END 

CREATE DATABASE CityInfoDB;
GO

USE CityInfoDB
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.CityInfo (
    CityId      INT            IDENTITY (1, 1) PRIMARY KEY,
    CityName    NVARCHAR (50)  NOT NULL,
    Description NVARCHAR (500) NULL
);
GO

insert into CityInfo(CityName,Description) values('New York City', 'the city with world finance center');
insert into CityInfo(CityName,Description) values('Paris', 'the captital city of France');
insert into CityInfo(CityName,Description) values('Sydney', 'A city is best known for its harbourfront Sydney Opera House');
insert into CityInfo(CityName,Description) values('Beijing', 'Capital city of China, it is the nation political, economic, and cultural center');
insert into CityInfo(CityName,Description) values('Toronto', 'The ciptial city of Ontario province');



CREATE TABLE dbo.PointsOfInterest (
    PointsOfInterestId          INT      IDENTITY (1, 1) PRIMARY KEY,
    NameofPOI        NVARCHAR (50)  NOT NULL,
    Description NVARCHAR (1000) NULL,
    CityId      INT            NOT NULL
    CONSTRAINT FK_PointsOfInterest_Cities_CityId FOREIGN KEY ([CityId]) REFERENCES CityInfo (CityId) ON DELETE CASCADE
    );
    
 Go
 






