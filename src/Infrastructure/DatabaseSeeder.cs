using System.Data;
using Dapper;
using DotNetEnv;
using MySql.Data.MySqlClient;

namespace Videoteket.src.Infrastructure;

internal static class DatabaseSeeder
{
  public static void Seed()
  {
    Env.Load();

    string _connectionString =
      Environment.GetEnvironmentVariable("CONNECTION_STRING")
      ?? throw new InvalidOperationException("Did not find a CONNECTION_STRING!");

    //skapa anslutningsobjekt till databasen
    using IDbConnection connection = new MySqlConnection(_connectionString);

    //lägg in det som Gustav pratade om att man ska kunna starta en seed och om något failar så rullas det tillbaka
    connection.Execute(
      """
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
      """
    );
  }
}
