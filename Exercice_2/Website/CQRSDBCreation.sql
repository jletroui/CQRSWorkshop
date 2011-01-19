IF exists(SELECT * FROM sys.databases WHERE name = 'CQRSWorkshopEventStore')
BEGIN
	DROP DATABASE [CQRSWorkshopEventStore]
END;

CREATE DATABASE [CQRSWorkshopEventStore];

IF exists(SELECT * FROM sys.databases WHERE name = N'CQRSWorkshopReadModel')
BEGIN
	DROP DATABASE [CQRSWorkshopReadModel]
END;

CREATE DATABASE [CQRSWorkshopReadModel];

