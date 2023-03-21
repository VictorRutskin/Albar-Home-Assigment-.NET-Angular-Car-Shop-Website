CREATE DATABASE CarWebsite;
GO


USE CarWebsite;

CREATE TABLE Cars (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(MAX),
    Category NVARCHAR(MAX),
    Price FLOAT,
    UnitsInStock INT,
    ModelYear INT,
    ImageSrc NVARCHAR(MAX)
)

INSERT INTO Cars (Name, Category, Price, UnitsInStock, ModelYear, ImageSrc)
VALUES ('Toyota Camry', 'Family', 25000.00, 10, 2022,'Car-1.jpg');

INSERT INTO Cars (Name, Category, Price, UnitsInStock, ModelYear, ImageSrc)
VALUES ('Honda Civic', 'Mini', 22000.00, 5, 2022, 'Car-2.jpg');

INSERT INTO Cars (Name, Category, Price, UnitsInStock, ModelYear, ImageSrc)
VALUES ('Ford F-150', 'Truck', 35000.00, 15, 2022, 'Car-3.jpg');

INSERT INTO Cars (Name, Category, Price, UnitsInStock, ModelYear, ImageSrc)
VALUES ('Tesla Model S', 'Luxury', 80000.00, 2, 2022 ,'Car-4.jpg');

INSERT INTO Cars (Name, Category, Price, UnitsInStock, ModelYear, ImageSrc)
VALUES ('Chevrolet Corvette', 'Sports', 65000.00, 3, 2022 ,'Car-5.jpg');

INSERT INTO Cars (Name, Category, Price, UnitsInStock, ModelYear, ImageSrc)
VALUES ('Jeep Wrangler', 'SUV', 30000.00, 8, 2022 ,'Car-6.jpg');

INSERT INTO Cars (Name, Category, Price, UnitsInStock, ModelYear, ImageSrc)
VALUES ('Audi A4', 'Luxury', 45000.00, 4, 2022 ,'Car-7.jpg');

INSERT INTO Cars (Name, Category, Price, UnitsInStock, ModelYear, ImageSrc)
VALUES ('BMW X5', 'SUV', 55000.00, 6, 2022 ,'Car-8.jpg');

INSERT INTO Cars (Name, Category, Price, UnitsInStock, ModelYear, ImageSrc)
VALUES ('Mercedes-Benz S-Class', 'Luxury', 90000.00, 1, 2022 ,'Car-9.jpg');

INSERT INTO Cars (Name, Category, Price, UnitsInStock, ModelYear, ImageSrc)
VALUES ('Lamborghini Huracan', 'Sports', 300000.00, 1, 2022 ,'Car-10.jpg');
GO

CREATE TABLE Users (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(MAX),
    Password NVARCHAR(MAX),
    LastLogin DATETIME
);

INSERT INTO Users (Name, Password, LastLogin)
VALUES ('Shlomi Mandel', 'HappyDuck300', '2022-01-01 12:00:00');

INSERT INTO Users (Name, Password, LastLogin)
VALUES ('Victor Rutskin', 'BigBruhMomentPerformer', '2022-01-03 18:30:00');
GO
