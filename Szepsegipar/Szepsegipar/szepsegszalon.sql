-- 1. Adatbázis létrehozása
CREATE DATABASE IF NOT EXISTS `szepsegszalon` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `szepsegszalon`;

-- 2. Táblák létrehozása

-- Dolgozók tábla
CREATE TABLE IF NOT EXISTS `dolgozók` (
  `D_ID` int(11) NOT NULL AUTO_INCREMENT,
  `D_VezetekNev` varchar(255) DEFAULT NULL,
  `D_KeresztNev` varchar(255) DEFAULT NULL,
  `D_Telefon` varchar(20) DEFAULT NULL,
  `D_Email` varchar(255) DEFAULT NULL,
  `Statusz` tinyint(1) DEFAULT NULL,
  `Szolgáltatása` int(11) DEFAULT NULL,
  `jogkor` int(11) DEFAULT NULL,
  PRIMARY KEY (`D_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- Szolgáltatás tábla
CREATE TABLE IF NOT EXISTS `szolgáltatás` (
  `SZ_ID` int(11) NOT NULL AUTO_INCREMENT,
  `SZ_Kategoria` varchar(255) DEFAULT NULL,
  `SZ_Idotartam` int(11) DEFAULT 30,
  `SZ_Ar` int(11) DEFAULT NULL,
  PRIMARY KEY (`SZ_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- Ügyfél tábla
CREATE TABLE IF NOT EXISTS `ügyfél` (
  `U_ID` int(11) NOT NULL AUTO_INCREMENT,
  `U_VezetekNev` varchar(255) DEFAULT NULL,
  `U_KeresztNev` varchar(255) DEFAULT NULL,
  `U_Telefon` varchar(20) DEFAULT NULL,
  `U_Email` varchar(255) DEFAULT NULL,
  `U_Pontok` int(11) DEFAULT NULL,
  PRIMARY KEY (`U_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- Foglalás tábla
CREATE TABLE IF NOT EXISTS `foglalás` (
  `F_ID` int(11) NOT NULL AUTO_INCREMENT,
  `SZ_ID` int(11) DEFAULT NULL,
  `D_ID` int(11) DEFAULT NULL,
  `U_ID` int(11) DEFAULT NULL,
  `F_Kezdes` datetime DEFAULT NULL,
  `F_Befejezesk` datetime DEFAULT NULL,
  PRIMARY KEY (`F_ID`),
  KEY `SZ_ID` (`SZ_ID`),
  KEY `D_ID` (`D_ID`),
  KEY `U_ID` (`U_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- 3. Tesztadatok feltöltése

-- Dolgozók adatai
INSERT INTO `dolgozók` (`D_VezetekNev`, `D_KeresztNev`, `D_Telefon`, `D_Email`, `Statusz`, `Szolgáltatása`, `jogkor`) VALUES
('Szabó', 'László', '06301112222', 'laszlo.szabo@example.com', 1, 1, 1),
('Fekete', 'Mária', '06302223333', 'maria.fekete@example.com', 1, 2, 1),
('Horváth', 'Béla', '06303334444', 'bela.horvath@example.com', 0, 3, 0),
('Molnár', 'Júlia', '06301231234', 'julia.molnar@example.com', 1, 2, 1),
('Kiss', 'Róbert', '06305556666', 'robert.kiss@example.com', 1, 3, 1),
('Németh', 'Attila', '06301234566', 'attila.nemeth@example.com', 1, 1, 0),
('Török', 'Petra', '06307775555', 'petra.torok@example.com', 0, 1, 0),
('Papp', 'Miklós', '06304443333', 'miklos.papp@example.com', 1, 3, 1);

-- Szolgáltatások adatai
INSERT INTO `szolgáltatás` (`SZ_Kategoria`, `SZ_Idotartam`, `SZ_Ar`) VALUES
('Hajvágás', 30, 5000),
('Manikűr', 30, 8000),
('Masszázs', 30, 12000),
('Pedikűr', 30, 7000),
('Arcápolás', 30, 9000),
('Szemöldökformázás', 30, 4000),
('Hajfestés', 30, 15000),
('Szempillafestés', 30, 3500);

-- Ügyfél adatai
INSERT INTO `ügyfél` (`U_VezetekNev`, `U_KeresztNev`, `U_Telefon`, `U_Email`, `U_Pontok`) VALUES
('Kiss', 'János', '06301234567', 'janos.kiss@example.com', 120),
('Nagy', 'Anna', '06309876543', 'anna.nagy@example.com', 230),
('Tóth', 'Gábor', '06305551234', 'gabor.toth@example.com', 90),
('Kovács', 'Erika', '06307778888', 'erika.kovacs@example.com', 150),
('Szilágyi', 'István', '06304445555', 'istvan.szilagyi@example.com', 310),
('Balogh', 'Bence', '06306669999', 'bence.balogh@example.com', 200),
('Varga', 'Zoltán', '06302221111', 'zoltan.varga@example.com', 50),
('Jakab', 'Tamás', '06303332222', 'tamas.jakab@example.com', 90);

-- Foglalások adatai
INSERT INTO `foglalás` (`SZ_ID`, `D_ID`, `U_ID`, `F_Kezdes`, `F_Befejezesk`) VALUES
(1, 1, 1, '2024-09-26 09:00:00', '2024-09-26 09:30:00'),
(2, 2, 2, '2024-09-26 10:00:00', '2024-09-26 11:00:00'),
(3, 3, 3, '2024-09-26 11:30:00', '2024-09-26 13:00:00'),
(4, 4, 4, '2024-09-27 09:00:00', '2024-09-27 11:00:00'),
(5, 5, 5, '2024-09-27 10:00:00', '2024-09-27 10:30:00'),
(1, 1, 1, '2024-09-27 11:30:00', '2024-09-27 12:00:00'),
(3, 2, 1, '2024-09-27 12:30:00', '2024-09-27 13:15:00'),
(2, 3, 4, '2024-09-27 14:00:00', '2024-09-27 15:00:00'),
(5, 5, 3, '2024-09-27 15:30:00', '2024-09-27 16:00:00'),
(4, 4, 5, '2024-09-27 16:30:00', '2024-09-27 18:30:00');

-- 4. Megkötések beállítása
ALTER TABLE `foglalás`
  ADD CONSTRAINT `foglalás_ibfk_1` FOREIGN KEY (`SZ_ID`) REFERENCES `szolgáltatás` (`SZ_ID`) ON DELETE CASCADE,
  ADD CONSTRAINT `foglalás_ibfk_2` FOREIGN KEY (`D_ID`) REFERENCES `dolgozók` (`D_ID`) ON DELETE CASCADE,
  ADD CONSTRAINT `foglalás_ibfk_3` FOREIGN KEY (`U_ID`) REFERENCES `ügyfél` (`U_ID`) ON DELETE CASCADE;
