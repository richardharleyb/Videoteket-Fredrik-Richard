DROP TABLE IF EXISTS RentalItem;
DROP TABLE IF EXISTS Rental;
DROP TABLE IF EXISTS PhysicalCopy;
DROP TABLE IF EXISTS Movie;
DROP TABLE IF EXISTS Customer;

CREATE TABLE Customer (
  Id          INT NOT NULL AUTO_INCREMENT,
  Name        VARCHAR(128) NOT NULL,
  PhoneNumber VARCHAR(128) NOT NULL,
  Email       VARCHAR(128) NULL,
  Address     VARCHAR(128) NOT NULL,
  PRIMARY KEY (Id)
);

CREATE TABLE Movie (
  Id          INT NOT NULL AUTO_INCREMENT,
  Title       VARCHAR(128) NOT NULL,
  ReleaseDate DATE NOT NULL,
  IsNew       BOOL NOT NULL,
  PRIMARY KEY (Id)
);

CREATE TABLE PhysicalCopy (
  Id       INT NOT NULL AUTO_INCREMENT,
  Barcode  VARCHAR(128) NOT NULL UNIQUE,
  IsActive BOOL NOT NULL,
  MovieId  INT NOT NULL,
  PRIMARY KEY (Id),
  FOREIGN KEY (MovieId) REFERENCES Movie(Id)
);

CREATE TABLE Rental (
  Id         INT NOT NULL AUTO_INCREMENT,
  CustomerId INT NOT NULL,
  RentedAt   DATETIME NOT NULL,
  PRIMARY KEY (Id),
  FOREIGN KEY (CustomerId) REFERENCES Customer(Id)
);

CREATE TABLE RentalItem (
  RentalId  INT NOT NULL,
  PhysicalCopyId INT NOT NULL,
  Price          DECIMAL(10,2) NOT NULL,
  ExpectedReturn DATETIME NOT NULL,
  ReturnedAt     DATETIME NULL,
  LateFee        DECIMAL(10,2) NULL,
  PRIMARY KEY (RentalId, PhysicalCopyId),
  FOREIGN KEY (RentalId) REFERENCES Rental(Id),
  FOREIGN KEY (PhysicalCopyId) REFERENCES PhysicalCopy(Id)
);

-- Kollar vad som finns på "Lager", det som inte är uthyrt
SELECT PhysicalCopy.Id AS ExemplarId, Movie.Title AS Titel
FROM PhysicalCopy
JOIN Movie ON Movie.Id = PhysicalCopy.MovieId
WHERE PhysicalCopy.IsActive
  AND PhysicalCopy.Id NOT IN (
    SELECT PhysicalCopyId
    FROM RentalItem
    WHERE ReturnedAt IS NULL
  );

-- Det som är uthyrt, till vem och när samt när det ska tillbaka
SELECT PhysicalCopy.Id AS ExemplarId, Movie.Title AS Titel, Customer.Name AS Kund,
       Rental.RentedAt AS Uthyrningsdatum, RentalItem.ExpectedReturn AS Återlämningsdatum
FROM RentalItem
JOIN Rental ON Rental.Id = RentalItem.RentalId
JOIN Customer ON Customer.Id = Rental.CustomerId
JOIN PhysicalCopy ON PhysicalCopy.Id = RentalItem.PhysicalCopyId
JOIN Movie ON Movie.Id = PhysicalCopy.MovieId
WHERE RentalItem.ReturnedAt IS NULL;

-- Alla filmer, releasedatum och om de är nya eller gamla
SELECT Title AS Titel, ReleaseDate AS Releasedatum, IsNew AS ÄrNy
FROM Movie
ORDER BY Title;

-- Lediga exemplar per film
SELECT Movie.Title AS Titel, COUNT(PhysicalCopy.Id) AS LedigaExemplar
FROM Movie
LEFT JOIN PhysicalCopy
  ON PhysicalCopy.MovieId = Movie.Id
  AND PhysicalCopy.IsActive
  AND PhysicalCopy.Id NOT IN (
    SELECT PhysicalCopyId
    FROM RentalItem
    WHERE ReturnedAt IS NULL
  )
GROUP BY Movie.Id, Movie.Title
ORDER BY Movie.Title;

-- Vad en kund med kund id 1 hyrt
SELECT Rental.Id AS Kvittonummer, Movie.Title AS Titel, Rental.RentedAt AS Uthyrningsdatum,
       RentalItem.ExpectedReturn AS Återlämningsdatum, RentalItem.ReturnedAt AS Återlämnad
FROM Rental
JOIN RentalItem ON RentalItem.RentalId = Rental.Id
JOIN PhysicalCopy ON PhysicalCopy.Id = RentalItem.PhysicalCopyId
JOIN Movie ON Movie.Id = PhysicalCopy.MovieId
WHERE Rental.CustomerId = 1
ORDER BY Rental.RentedAt DESC;

-- Försenade filmer som inte är tillbaka
SELECT Customer.Name AS Kund, Customer.PhoneNumber AS Telefonnummer, Movie.Title AS Titel,
       RentalItem.ExpectedReturn AS Återlämningsdatum,
       DATEDIFF(NOW(), RentalItem.ExpectedReturn) AS DagarSen
FROM RentalItem
JOIN Rental ON Rental.Id = RentalItem.RentalId
JOIN Customer ON Customer.Id = Rental.CustomerId
JOIN PhysicalCopy ON PhysicalCopy.Id = RentalItem.PhysicalCopyId
JOIN Movie ON Movie.Id = PhysicalCopy.MovieId
WHERE RentalItem.ReturnedAt IS NULL
  AND RentalItem.ExpectedReturn < NOW()
ORDER BY RentalItem.ExpectedReturn;

-- Alla kvitton med pris och förseningsavgift per film
SELECT Rental.Id AS Kvittonummer, Customer.Name AS Kund, Movie.Title AS Titel,
       RentalItem.Price AS Pris, RentalItem.LateFee AS Förseningsavgift
FROM Rental
JOIN Customer ON Customer.Id = Rental.CustomerId
JOIN RentalItem ON RentalItem.RentalId = Rental.Id
JOIN PhysicalCopy ON PhysicalCopy.Id = RentalItem.PhysicalCopyId
JOIN Movie ON Movie.Id = PhysicalCopy.MovieId
ORDER BY Rental.Id;

-- Vad som tjänats totalt per kund (hyra + förseningsavgift)
SELECT Customer.Name AS Kund, SUM(RentalItem.Price + IFNULL(RentalItem.LateFee, 0)) AS TotalIntäkt
FROM Customer
JOIN Rental ON Rental.CustomerId = Customer.Id
JOIN RentalItem ON RentalItem.RentalId = Rental.Id
GROUP BY Customer.Id, Customer.Name
ORDER BY TotalIntäkt DESC;

-- Antal sent återlämnade filmer där ingen förseningsavgift är registrerad
SELECT COUNT(*) AS AntalUtanAvgift
FROM RentalItem
WHERE RentalItem.ReturnedAt > RentalItem.ExpectedReturn
  AND RentalItem.LateFee IS NULL;
-- vilka kunder?
