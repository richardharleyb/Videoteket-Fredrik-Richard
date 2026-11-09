-- Kollar vad som finns på "Lager", Det som inte är uthyrt
SELECT RentalItem.Id AS ExemplarId, Movie.Title AS Titel
FROM RentalItem
JOIN Movie ON Movie.Id = RentalItem.MovieId
WHERE RentalItem.Id NOT IN (
    SELECT CustomerOrder.RentalItemId
    FROM CustomerOrder
    WHERE CustomerOrder.RentedReturn IS NULL
);

-- Det som är uthyrda till vem och när samt när dom ska tillbaka
SELECT RentalItem.Id AS ExemplarId, Movie.Title AS Titel, Customer.Name AS Kund,
CustomerOrder.RentedAt AS Uthyrningsdatum, CustomerOrder.RentedDue AS Återlämningsdatum
FROM CustomerOrder
JOIN RentalItem ON RentalItem.Id = CustomerOrder.RentalItemId
JOIN Movie ON Movie.Id = RentalItem.MovieId
JOIN Customer ON Customer.Id = CustomerOrder.CustomerId
WHERE CustomerOrder.RentedReturn IS NULL;

-- Alla filmer, pris och releasedatum
SELECT Movie.Title AS Titel, Movie.ReleaseDate AS Releasedatum, DailyRate.Name AS Pristyp, DailyRate.Amount AS PrisPerDag
FROM Movie
JOIN DailyRate ON DailyRate.Id = Movie.DailyRateId
ORDER BY Movie.Title;

-- Lediga filmer
SELECT Movie.Title AS Titel, COUNT(RentalItem.Id) AS LedigaExemplar
FROM Movie
LEFT JOIN RentalItem
    ON RentalItem.MovieId = Movie.Id
    AND RentalItem.Id NOT IN (
        SELECT CustomerOrder.RentalItemId
        FROM CustomerOrder
        WHERE CustomerOrder.RentedReturn IS NULL
    )
GROUP BY Movie.Id, Movie.Title
ORDER BY Movie.Title;

-- Vad en kund hyrt
SELECT CustomerOrder.Id AS Ordernummer, Movie.Title AS Titel, CustomerOrder.RentedAt AS Uthyrningsdatum,
CustomerOrder.RentedDue AS Återlämningsdatum, CustomerOrder.RentedReturn AS Återlämnad
FROM CustomerOrder
JOIN RentalItem ON RentalItem.Id = CustomerOrder.RentalItemId
JOIN Movie ON Movie.Id = RentalItem.MovieId
WHERE CustomerOrder.CustomerId = 1
ORDER BY CustomerOrder.RentedAt DESC;

-- Försenade filmer som inte är tillbaka
SELECT Customer.Name AS Kund, Customer.PhoneNumber AS Telefonnummer, Movie.Title AS Titel,
CustomerOrder.RentedDue AS Återlämningsdatum, DATEDIFF(NOW(), CustomerOrder.RentedDue) AS DagarSen
FROM CustomerOrder
JOIN Customer ON Customer.Id = CustomerOrder.CustomerId
JOIN RentalItem ON RentalItem.Id = CustomerOrder.RentalItemId
JOIN Movie ON Movie.Id = RentalItem.MovieId
WHERE CustomerOrder.RentedReturn IS NULL
  AND CustomerOrder.RentedDue < NOW()
ORDER BY CustomerOrder.RentedDue;

-- Alla kvitton
SELECT Receipt.Id AS Kvittonummer, Receipt.ReceiptType AS Kvittotyp, Receipt.OrderCost AS Belopp,
CustomerOrder.Id AS Ordernummer, Customer.Name AS Kund, Movie.Title AS Titel
FROM Receipt
JOIN CustomerOrder ON CustomerOrder.Id = Receipt.CustomerOrderId
JOIN Customer ON Customer.Id = CustomerOrder.CustomerId
JOIN RentalItem ON RentalItem.Id = CustomerOrder.RentalItemId
JOIN Movie ON Movie.Id = RentalItem.MovieId
ORDER BY Receipt.Id;

-- Vad som tjänats totalt per kund
SELECT Customer.Name AS Kund, SUM(Receipt.OrderCost) AS TotalIntäkt
FROM Customer
JOIN CustomerOrder ON CustomerOrder.CustomerId = Customer.Id
JOIN Receipt ON Receipt.CustomerOrderId = CustomerOrder.Id
GROUP BY Customer.Id, Customer.Name
ORDER BY TotalIntäkt DESC;

-- Ordrar utan kvitto, (Dom som inte betalat än)
SELECT CustomerOrder.Id AS Ordernummer, Customer.Name AS Kund, CustomerOrder.RentedAt AS Uthyrningsdatum
FROM CustomerOrder
JOIN Customer ON Customer.Id = CustomerOrder.CustomerId
LEFT JOIN Receipt ON Receipt.CustomerOrderId = CustomerOrder.Id
WHERE Receipt.Id IS NULL;
