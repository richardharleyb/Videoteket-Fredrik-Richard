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

-- ============================================================================
-- SEED DATA (Året är 1998 - VHS-videotek)
-- ============================================================================

-- Kunder
INSERT INTO Customer (Id, Name, PhoneNumber, Email, Address) VALUES
(1, 'Erik Svensson', '070-1234567', 'erik.svensson@telia.se', 'Kungsgatan 14, Stockholm'),
(2, 'Maria Lindgren', '070-2345678', 'maria.l@swipnet.se', 'Sveavägen 42, Stockholm'),
(3, 'Johan Nilsson', '070-3456789', NULL, 'Storgatan 8, Stockholm'),
(4, 'Karin Berglund', '070-4567890', 'karin.b@posten.se', 'Ringvägen 101, Stockholm');

-- Filmer (Alla släppta 1998 eller tidigare)
-- Stora blockbusters, medelstora filmer och små kult-/indiefilmer
INSERT INTO Movie (Id, Title, ReleaseDate, IsNew) VALUES
-- Stora blockbusters (3-6 exemplar)
(1, 'Titanic', '1997-12-19', TRUE),
(2, 'Armageddon', '1998-07-01', TRUE),
(3, 'Terminator 2: Judgment Day', '1991-07-03', TRUE),
(4, 'The Terminator', '1984-10-26', FALSE),
(5, 'Jurassic Park', '1993-06-11', TRUE),
(6, 'The Lion King', '1994-06-24', TRUE),
(7, 'Lethal Weapon', '1987-03-06', FALSE),
(8, 'Die Hard', '1988-07-15', FALSE),
(9, 'Men in Black', '1997-07-02', TRUE),
-- Medelstora filmer (2-3 exemplar)
(10, 'Scream', '1996-12-20', TRUE),
(11, 'Fargo', '1996-03-08', FALSE),
-- Små filmer / B-filmer / Kultklassiker (1-2 exemplar)
(12, 'Braindead', '1992-08-13', FALSE),
(13, 'Clerks', '1994-10-19', FALSE),
(14, 'Evil Dead II', '1987-03-13', FALSE),
(15, 'Bad Taste', '1987-12-01', FALSE);

-- Fysiska exemplar (PhysicalCopy / VHS)
-- 1-2 exemplar för små filmer, 3-6 exemplar för stora filmer
INSERT INTO PhysicalCopy (Id, Barcode, IsActive, MovieId) VALUES
-- Titanic (Storfilm: 6 exemplar)
(1, 'VHS-TITANIC-01', TRUE, 1),
(2, 'VHS-TITANIC-02', TRUE, 1),
(3, 'VHS-TITANIC-03', TRUE, 1),
(4, 'VHS-TITANIC-04', TRUE, 1),
(5, 'VHS-TITANIC-05', TRUE, 1),
(6, 'VHS-TITANIC-06', TRUE, 1),

-- Armageddon (Storfilm: 5 exemplar)
(7, 'VHS-ARMAGEDDON-01', TRUE, 2),
(8, 'VHS-ARMAGEDDON-02', TRUE, 2),
(9, 'VHS-ARMAGEDDON-03', TRUE, 2),
(10, 'VHS-ARMAGEDDON-04', TRUE, 2),
(11, 'VHS-ARMAGEDDON-05', TRUE, 2),

-- Terminator 2: Judgment Day (Storfilm: 5 exemplar)
(12, 'VHS-TERMINATOR-2-01', TRUE, 3),
(13, 'VHS-TERMINATOR-2-02', TRUE, 3),
(14, 'VHS-TERMINATOR-2-03', TRUE, 3),
(15, 'VHS-TERMINATOR-2-04', TRUE, 3),
(16, 'VHS-TERMINATOR-2-05', TRUE, 3),

-- The Terminator (Stor klassiker: 3 exemplar)
(17, 'VHS-THE-TERMINATOR-01', TRUE, 4),
(18, 'VHS-THE-TERMINATOR-02', TRUE, 4),
(19, 'VHS-THE-TERMINATOR-03', TRUE, 4),

-- Jurassic Park (Storfilm: 4 exemplar)
(20, 'VHS-JURASSIC-PARK-01', TRUE, 5),
(21, 'VHS-JURASSIC-PARK-02', TRUE, 5),
(22, 'VHS-JURASSIC-PARK-03', TRUE, 5),
(23, 'VHS-JURASSIC-PARK-04', TRUE, 5),

-- The Lion King (Storfilm: 5 exemplar)
(24, 'VHS-THE-LION-KING-01', TRUE, 6),
(25, 'VHS-THE-LION-KING-02', TRUE, 6),
(26, 'VHS-THE-LION-KING-03', TRUE, 6),
(27, 'VHS-THE-LION-KING-04', TRUE, 6),
(28, 'VHS-THE-LION-KING-05', TRUE, 6),

-- Lethal Weapon (Storfilm: 4 exemplar)
(29, 'VHS-LETHAL-WEAPON-01', TRUE, 7),
(30, 'VHS-LETHAL-WEAPON-02', TRUE, 7),
(31, 'VHS-LETHAL-WEAPON-03', TRUE, 7),
(32, 'VHS-LETHAL-WEAPON-04', TRUE, 7),

-- Die Hard (Storfilm: 4 exemplar)
(33, 'VHS-DIE-HARD-01', TRUE, 8),
(34, 'VHS-DIE-HARD-02', TRUE, 8),
(35, 'VHS-DIE-HARD-03', TRUE, 8),
(36, 'VHS-DIE-HARD-04', TRUE, 8),

-- Men in Black (Storfilm: 4 exemplar)
(37, 'VHS-MEN-IN-BLACK-01', TRUE, 9),
(38, 'VHS-MEN-IN-BLACK-02', TRUE, 9),
(39, 'VHS-MEN-IN-BLACK-03', TRUE, 9),
(40, 'VHS-MEN-IN-BLACK-04', TRUE, 9),

-- Scream (Medelstor film: 3 exemplar)
(41, 'VHS-SCREAM-01', TRUE, 10),
(42, 'VHS-SCREAM-02', TRUE, 10),
(43, 'VHS-SCREAM-03', TRUE, 10),

-- Fargo (Medelstor film: 2 exemplar)
(44, 'VHS-FARGO-01', TRUE, 11),
(45, 'VHS-FARGO-02', TRUE, 11),

-- Braindead (Liten kult/splatter: 1 exemplar)
(46, 'VHS-BRAINDEAD-01', TRUE, 12),

-- Clerks (Liten indiefilm: 1 exemplar)
(47, 'VHS-CLERKS-01', TRUE, 13),

-- Evil Dead II (Liten kultfilm: 2 exemplar)
(48, 'VHS-EVIL-DEAD-2-01', TRUE, 14),
(49, 'VHS-EVIL-DEAD-2-02', TRUE, 14),

-- Bad Taste (Liten kultfilm: 1 exemplar)
(50, 'VHS-BAD-TASTE-01', TRUE, 15);

-- Uthyrningar (Rental)
INSERT INTO Rental (Id, CustomerId, RentedAt) VALUES
(1, 1, '1998-11-01 18:30:00'),
(2, 2, '1998-11-05 19:15:00'),
(3, 1, '1998-11-08 14:00:00'),
(4, 3, '1998-11-10 20:00:00');

-- Uthyrda artiklar (RentalItem)
INSERT INTO RentalItem (RentalId, PhysicalCopyId, Price, ExpectedReturn, ReturnedAt, LateFee) VALUES
-- Order 1 (Kund 1 - Återlämnad i tid)
(1, 1, 49.00, '1998-11-02 18:30:00', '1998-11-02 17:00:00', NULL),
(1, 12, 39.00, '1998-11-02 18:30:00', '1998-11-02 17:00:00', NULL),

-- Order 2 (Kund 2 - Återlämnad för sent med förseningsavgift)
(2, 7, 49.00, '1998-11-06 19:15:00', '1998-11-08 12:00:00', 40.00),

-- Order 3 (Kund 1 - Uthyrd just nu, inte återlämnad än)
(3, 2, 49.00, '1998-11-09 14:00:00', NULL, NULL),
(3, 46, 39.00, '1998-11-09 14:00:00', NULL, NULL),

-- Order 4 (Kund 3 - Uthyrd och sen, ingen avgift registrerad än)
(4, 20, 39.00, '1998-11-11 20:00:00', NULL, NULL);

-- ============================================================================
-- SQL QUERIES (Frågor och rapporter)
-- ============================================================================

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
