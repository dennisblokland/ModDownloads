  IF EXISTS(SELECT 1 FROM information_schema.tables
  WHERE table_name = '
__EFMigrationsHistory' AND table_schema = DATABASE())
BEGIN
CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

END;

CREATE TABLE `Mod` (
    `ID` int NOT NULL,
    `Name` text NOT NULL,
    `URL` text NOT NULL,
    PRIMARY KEY (`ID`)
);

CREATE TABLE `Download` (
    `ID` int NOT NULL,
    `Downloads` int NOT NULL,
    `ModId` int NOT NULL,
    `Timestamp` datetime NOT NULL,
    PRIMARY KEY (`ID`),
    CONSTRAINT `FK_Download_Mod_ModId` FOREIGN KEY (`ModId`) REFERENCES `Mod` (`ID`) ON DELETE CASCADE
);

CREATE INDEX `IX_Download_ModId` ON `Download` (`ModId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190208140655_InitialCreate', '2.2.0-rtm-35687');

CREATE TABLE `DownloadIncrease` (
    `ID` int NOT NULL,
    `Increase` int NOT NULL,
    `ModId` int NOT NULL,
    `Timestamp` datetime NOT NULL,
    PRIMARY KEY (`ID`),
    CONSTRAINT `FK_DownloadIncrease_Mod_ModId` FOREIGN KEY (`ModId`) REFERENCES `Mod` (`ID`) ON DELETE CASCADE
);

CREATE INDEX `IX_DownloadIncrease_ModId` ON `DownloadIncrease` (`ModId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190210121005_add increase', '2.2.0-rtm-35687');

DROP TABLE `DownloadIncrease`;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190210122510_remove increase', '2.2.0-rtm-35687');