-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Sze 25. 09:50
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
  MODIFY `D_ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `foglalás`
--
ALTER TABLE `foglalás`
  MODIFY `F_ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `szolgáltatás`
--
ALTER TABLE `szolgáltatás`
  MODIFY `SZ_ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `ügyfél`
--
ALTER TABLE `ügyfél`
  MODIFY `U_ID` int(11) NOT NULL AUTO_INCREMENT;

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
