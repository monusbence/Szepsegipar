-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Sze 30. 10:43
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `szepsegszalon`
--
CREATE DATABASE IF NOT EXISTS `szepsegszalon` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `szepsegszalon`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `dolgozók`
--

CREATE TABLE `dolgozók` (
  `D_ID` int(11) NOT NULL,
  `D_VezetekNev` varchar(255) DEFAULT NULL,
  `D_KeresztNev` varchar(255) DEFAULT NULL,
  `D_Telefon` varchar(20) DEFAULT NULL,
  `D_Email` varchar(255) DEFAULT NULL,
  `Statusz` tinyint(1) DEFAULT NULL,
  `Szolgáltatása` int(11) DEFAULT NULL,
  `jogkor` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `dolgozók`
--

INSERT INTO `dolgozók` (`D_ID`, `D_VezetekNev`, `D_KeresztNev`, `D_Telefon`, `D_Email`, `Statusz`, `Szolgáltatása`, `jogkor`) VALUES
(1, 'Szabó', 'László', '06301112222', 'laszlo.szabo@example.com', 1, 1, 1),
(2, 'Fekete', 'Mária', '06302223333', 'maria.fekete@example.com', 1, 2, 1),
(3, 'Horváth', 'Béla', '06303334444', 'bela.horvath@example.com', 0, 3, 0),
(4, 'Molnár', 'Júlia', '06301231234', 'julia.molnar@example.com', 1, 2, 1),
(5, 'Kiss', 'Róbert', '06305556666', 'robert.kiss@example.com', 1, 3, 1),
(6, 'Németh', 'Attila', '06301234566', 'attila.nemeth@example.com', 1, 1, 0),
(7, 'Török', 'Petra', '06307775555', 'petra.torok@example.com', 0, 1, 0),
(8, 'Papp', 'Miklós', '06304443333', 'miklos.papp@example.com', 1, 3, 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `foglalás`
--

CREATE TABLE `foglalás` (
  `F_ID` int(11) NOT NULL,
  `SZ_ID` int(11) DEFAULT NULL,
  `D_ID` int(11) DEFAULT NULL,
  `U_ID` int(11) DEFAULT NULL,
  `F_Kezdes` datetime DEFAULT NULL,
  `F_Befejezesk` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `foglalás`
--

INSERT INTO `foglalás` (`F_ID`, `SZ_ID`, `D_ID`, `U_ID`, `F_Kezdes`, `F_Befejezesk`) VALUES
(1, 1, 1, 1, '2024-09-26 09:00:00', '2024-09-26 09:30:00'),
(2, 2, 2, 2, '2024-09-26 10:00:00', '2024-09-26 11:00:00'),
(3, 3, 3, 3, '2024-09-26 11:30:00', '2024-09-26 13:00:00'),
(4, 4, 4, 4, '2024-09-27 09:00:00', '2024-09-27 11:00:00'),
(5, 5, 5, 5, '2024-09-27 10:00:00', '2024-09-27 10:30:00'),
(6, 1, 1, 2, '2024-09-27 11:30:00', '2024-09-27 12:00:00'),
(7, 3, 2, 1, '2024-09-27 12:30:00', '2024-09-27 13:15:00'),
(8, 2, 3, 4, '2024-09-27 14:00:00', '2024-09-27 15:00:00'),
(9, 5, 5, 3, '2024-09-27 15:30:00', '2024-09-27 16:00:00'),
(10, 4, 4, 5, '2024-09-27 16:30:00', '2024-09-27 18:30:00');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `szolgáltatás`
--

CREATE TABLE `szolgáltatás` (
  `SZ_ID` int(11) NOT NULL,
  `SZ_Kategoria` varchar(255) DEFAULT NULL,
  `SZ_Idotartam` datetime DEFAULT NULL,
  `SZ_Ar` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `szolgáltatás`
--

INSERT INTO `szolgáltatás` (`SZ_ID`, `SZ_Kategoria`, `SZ_Idotartam`, `SZ_Ar`) VALUES
(1, 'Hajvágás', '2024-09-25 00:30:00', 5000),
(2, 'Manikűr', '2024-09-25 01:00:00', 8000),
(3, 'Masszázs', '2024-09-25 01:30:00', 12000),
(4, 'Pedikűr', '2024-09-25 01:00:00', 7000),
(5, 'Arcápolás', '2024-09-25 01:15:00', 9000),
(6, 'Szemöldökformázás', '2024-09-25 00:45:00', 4000),
(7, 'Hajfestés', '2024-09-25 02:00:00', 15000),
(8, 'Szempillafestés', '2024-09-25 00:30:00', 3500);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ügyfél`
--

CREATE TABLE `ügyfél` (
  `U_ID` int(11) NOT NULL,
  `U_VezetekNev` varchar(255) DEFAULT NULL,
  `U_KeresztNev` varchar(255) DEFAULT NULL,
  `U_Telefon` varchar(20) DEFAULT NULL,
  `U_Email` varchar(255) DEFAULT NULL,
  `U_Pontok` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `ügyfél`
--

INSERT INTO `ügyfél` (`U_ID`, `U_VezetekNev`, `U_KeresztNev`, `U_Telefon`, `U_Email`, `U_Pontok`) VALUES
(1, 'Kiss', 'János', '06301234567', 'janos.kiss@example.com', 120),
(2, 'Nagy', 'Anna', '06309876543', 'anna.nagy@example.com', 230),
(3, 'Tóth', 'Gábor', '06305551234', 'gabor.toth@example.com', 90),
(4, 'Kovács', 'Erika', '06307778888', 'erika.kovacs@example.com', 150),
(5, 'Szilágyi', 'István', '06304445555', 'istvan.szilagyi@example.com', 310),
(6, 'Balogh', 'Bence', '06306669999', 'bence.balogh@example.com', 200),
(7, 'Varga', 'Zoltán', '06302221111', 'zoltan.varga@example.com', 50),
(8, 'Jakab', 'Tamás', '06303332222', 'tamas.jakab@example.com', 90);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `dolgozók`
--
ALTER TABLE `dolgozók`
  ADD PRIMARY KEY (`D_ID`);

--
-- A tábla indexei `foglalás`
--
ALTER TABLE `foglalás`
  ADD PRIMARY KEY (`F_ID`),
  ADD KEY `SZ_ID` (`SZ_ID`),
  ADD KEY `D_ID` (`D_ID`),
  ADD KEY `U_ID` (`U_ID`);

--
-- A tábla indexei `szolgáltatás`
--
ALTER TABLE `szolgáltatás`
  ADD PRIMARY KEY (`SZ_ID`);

--
-- A tábla indexei `ügyfél`
--
ALTER TABLE `ügyfél`
  ADD PRIMARY KEY (`U_ID`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `dolgozók`
--
ALTER TABLE `dolgozók`
  MODIFY `D_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT a táblához `foglalás`
--
ALTER TABLE `foglalás`
  MODIFY `F_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT a táblához `szolgáltatás`
--
ALTER TABLE `szolgáltatás`
  MODIFY `SZ_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT a táblához `ügyfél`
--
ALTER TABLE `ügyfél`
  MODIFY `U_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `foglalás`
--
ALTER TABLE `foglalás`
  ADD CONSTRAINT `foglalás_ibfk_1` FOREIGN KEY (`SZ_ID`) REFERENCES `szolgáltatás` (`SZ_ID`),
  ADD CONSTRAINT `foglalás_ibfk_2` FOREIGN KEY (`D_ID`) REFERENCES `dolgozók` (`D_ID`),
  ADD CONSTRAINT `foglalás_ibfk_3` FOREIGN KEY (`U_ID`) REFERENCES `ügyfél` (`U_ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
