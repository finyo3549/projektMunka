-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: localhost
-- Létrehozás ideje: 2024. Ápr 22. 09:33
-- Kiszolgáló verziója: 10.4.28-MariaDB-log
-- PHP verzió: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `kviz`
--

DELIMITER $$
--
-- Eljárások
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `update_user_ranks_view_proc` ()   BEGIN
    DROP VIEW IF EXISTS user_ranks;
    CREATE VIEW user_ranks AS
    SELECT user_id,name,email,score FROM `users` 
	JOIN ranks ON users.id = ranks.user_id;
	END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `answers`
--

CREATE TABLE `answers` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `question_id` bigint(20) UNSIGNED NOT NULL,
  `answer_text` text NOT NULL,
  `is_correct` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- A tábla adatainak kiíratása `answers`
--

INSERT INTO `answers` (`id`, `question_id`, `answer_text`, `is_correct`, `created_at`, `updated_at`) VALUES
(1, 1, '7', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(2, 1, '8', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(3, 1, '9', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(4, 1, '10', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(5, 2, 'Franciaország', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(6, 2, 'Németország', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(7, 2, 'Olaszország', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(8, 2, 'Spanyolország', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(9, 3, 'I. István', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(10, 3, 'Mátyás király', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(11, 3, 'I. Károly', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(12, 3, 'IV. Béla', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(13, 4, 'H2O', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(14, 4, 'CO2', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(15, 4, 'O2', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(16, 4, 'H2SO4', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(17, 5, 'H.G. Wells', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(18, 5, 'Charles Dickens', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(19, 5, 'Jules Verne', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(20, 5, 'Mark Twain', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(21, 6, '9', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(22, 6, '8', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(23, 6, '7', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(24, 6, '6', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(25, 7, 'Dél-Amerika', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(26, 7, 'Afrika', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(27, 7, 'Ázsia', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(28, 7, 'Európa', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(29, 8, '1939', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(30, 8, '1945', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(31, 8, '1914', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(32, 8, '1936', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(33, 9, 'Higany', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(34, 9, 'Ólom', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(35, 9, 'Arany', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(36, 9, 'Vas', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(37, 10, 'Arany János', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(38, 10, 'Vörösmarty Mihály', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(39, 10, 'Petőfi Sándor', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(40, 10, 'Jókai Mór', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(41, 11, 'Hajszálerek kitágulása', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(42, 11, 'Hajózás a Holdra', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(43, 11, 'Gyöngy képződése', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(44, 11, 'Napenergia felhasználása', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(45, 12, '-100 Celsius fok', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(46, 12, '-273,15 Celsius fok', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(47, 12, '100 Celsius fok', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(48, 12, '0 Celsius fok', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(49, 13, 'Kaktusz', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(50, 13, 'Tölgyfa', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(51, 13, 'Sequoia fa', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(52, 13, 'Fenyőfa', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(53, 14, 'Neil Armstrong', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(54, 14, 'Yuri Gagarin', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(55, 14, 'Buzz Aldrin', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(56, 14, 'John Glenn', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(57, 15, 'Környezetvédelem támogatása', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(58, 15, 'Közlekedési dugók csökkentése', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(59, 15, 'Tudományos kutatások ösztönzése', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(60, 15, 'Közlekedési balesetek csökkentése', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(61, 16, 'Kamra', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(62, 16, 'Pitvar', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(63, 16, 'Balkamra', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(64, 16, 'Vitorlás széria', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(65, 17, 'Mars', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(66, 17, 'Vénusz', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(67, 17, 'Jupiter', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(68, 17, 'Szaturnusz', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(69, 18, 'Nitrogén', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(70, 18, 'Szén-dioxid', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(71, 18, 'Oxigén', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(72, 18, 'Hélium', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(73, 19, 'Viktória királynő', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(74, 19, 'Erzsébet királynő', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(75, 19, 'Mária Terézia', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(76, 19, 'Katalin cárnő', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(77, 20, 'Zöld', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(78, 20, 'Kék', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(79, 20, 'Piros', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(80, 20, 'Sárga', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(81, 21, '1980', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(82, 21, 'soha', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(83, 21, '2000', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(84, 21, '1992', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(85, 22, 'Alpok', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(86, 22, 'Himalája', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(87, 22, 'Andok', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(88, 22, 'Rocky Mountains', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(89, 23, 'Marie Curie', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(90, 23, 'Teréz Anya', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(91, 23, 'Thomas Edison', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(92, 23, 'Nikola Tesla', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(93, 24, 'Forrás', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(94, 24, 'Olvadás', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(95, 24, 'Fagyás', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(96, 24, 'Párolgás', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(97, 25, '1917', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(98, 25, 'Parasite', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(99, 25, 'Joker', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(100, 25, 'Once Upon a Time in Hollywood', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(101, 26, 'Neil Armstrong', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(102, 26, 'Yuri Gagarin', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(103, 26, 'Buzz Aldrin', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(104, 26, 'John Glenn', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(105, 27, 'Tömeg és energia kapcsolata', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(106, 27, 'Fénysebesség mérése', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(107, 27, 'Atombomba kifejlesztése', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(108, 27, 'Tömeg és erő kapcsolata', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(109, 28, 'Mars', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(110, 28, 'Vénusz', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(111, 28, 'Jupiter', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(112, 28, 'Hold', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(113, 29, 'Galápagoszi óriásteknős', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(114, 29, 'Kakadu', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(115, 29, 'Koala', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(116, 29, 'Kenguru', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(117, 30, '10', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(118, 30, '11', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(119, 30, '12', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(120, 30, '13', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(121, 31, '10', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(122, 31, '12', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(123, 31, '14', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(124, 31, '16', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(125, 32, '10', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(126, 32, '12', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(127, 32, '14', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(128, 32, '16', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(129, 33, '0', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(130, 33, '1', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(131, 33, '2', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(132, 33, '3', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(133, 34, '0', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(134, 34, '1', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(135, 34, '2', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(136, 34, '3', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(137, 35, '6', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(138, 35, '9', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(139, 35, '12', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(140, 35, '15', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(141, 36, '10', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(142, 36, '12', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(143, 36, '14', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(144, 36, '16', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(145, 37, 'Dél-Amerika', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(146, 37, 'Afrika', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(147, 37, 'Európa', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(148, 37, 'Ázsia', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(149, 38, 'Himalája', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(150, 38, 'Alpok', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(151, 38, 'Andok', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(152, 38, 'Rocky Mountains', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(153, 39, 'Franciaország', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(154, 39, 'Németország', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(155, 39, 'Olaszország', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(156, 39, 'Oroszország', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(157, 40, 'Fekete-tenger', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(158, 40, 'Vörös-tenger', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(159, 40, 'Földközi-tenger', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(160, 40, 'Kaszpi-tenger', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(161, 41, 'Sahara', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(162, 41, 'Góbi', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(163, 41, 'Kalahári', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(164, 41, 'Atacama', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(165, 42, 'Afrika', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(166, 42, 'Ázsia', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(167, 42, 'Európa', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(168, 42, 'Dél-Amerika', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(169, 43, 'New York', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(170, 43, 'Washington', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(171, 43, 'Chicago', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(172, 43, 'Houston', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(173, 44, '1776', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(174, 44, '1781', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(175, 44, '1789', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(176, 44, '1799', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(177, 45, 'Thomas Jefferson', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(178, 45, 'George Washington', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(179, 45, 'Abraham Lincoln', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(180, 45, 'James Madison', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(181, 46, '1914', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(182, 46, '1918', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(183, 46, '1922', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(184, 46, '1926', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(185, 47, 'J.R.R. Tolkien', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(186, 47, 'J.K. Rowling ', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(187, 47, 'George R.R. Martin', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(188, 47, 'Stephen King', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(189, 48, 'Macbeth', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(190, 48, 'Romeo és Júlia', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(191, 48, 'Hamlet', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(192, 48, 'Othello', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(193, 49, 'Achilles', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(194, 49, 'Herkules', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(195, 49, 'Odüsszeusz', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(196, 49, 'Thészeusz', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(197, 50, 'Nagy remények', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(198, 50, 'A két torony', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(199, 50, 'A gyűrűk ura', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(200, 50, 'Oliver Twist', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(201, 51, 'Leo Tolstoy', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(202, 51, 'Fyodor Dostoevsky', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(203, 51, 'Anton Chekhov', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(204, 51, 'Ivan Turgenev', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(205, 52, 'Heidi', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(206, 52, 'Pollyanna', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(207, 52, 'A titkos kert', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(208, 52, 'Az elveszett világ', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(209, 53, 'George Orwell', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(210, 53, 'Aldous Huxley', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(211, 53, 'Ray Bradbury', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(212, 53, 'H.G. Wells', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(213, 54, 'Lancelot', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(214, 54, 'Galahad', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(215, 54, 'Percival', 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(216, 54, 'Robin Hood', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `boosters`
--

CREATE TABLE `boosters` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `booster_name` varchar(255) NOT NULL,
  `booster_description` varchar(255) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- A tábla adatainak kiíratása `boosters`
--

INSERT INTO `boosters` (`id`, `booster_name`, `booster_description`, `created_at`, `updated_at`) VALUES
(1, 'Telefonhívás', 'A telefonhívás segítségével egy barátját hívhatja fel a játékos, aki segítséget nyújthat a válaszhoz. A barát nem minden esetben tudja a jó választ', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(2, 'Közönség', 'A közönség segítségével a játékos megtekintheti, hogy a közönség melyik válaszlehetőséget támogatja. Néha a közönség is tévedhet...', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(3, 'Felező', 'A felező segítségével két rossz válaszlehetőséget elvesz a gép, így már csak kettőből kell választania a játékosnak', '2024-04-22 04:46:49', '2024-04-22 04:46:49');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `failed_jobs`
--

CREATE TABLE `failed_jobs` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `uuid` varchar(255) NOT NULL,
  `connection` text NOT NULL,
  `queue` text NOT NULL,
  `payload` longtext NOT NULL,
  `exception` longtext NOT NULL,
  `failed_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `migrations`
--

CREATE TABLE `migrations` (
  `id` int(10) UNSIGNED NOT NULL,
  `migration` varchar(255) NOT NULL,
  `batch` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- A tábla adatainak kiíratása `migrations`
--

INSERT INTO `migrations` (`id`, `migration`, `batch`) VALUES
(1, '2019_08_19_000000_create_failed_jobs_table', 1),
(2, '2019_12_14_000001_create_personal_access_tokens_table', 1),
(3, '2024_02_03_151700_create_users_table', 1),
(4, '2024_02_03_152107_create_topic_table', 1),
(5, '2024_02_04_151722_create_ranks_table', 1),
(6, '2024_02_04_152152_create_questions_table', 1),
(7, '2024_02_04_153624_create_booster_table', 1),
(8, '2024_02_04_153749_create_userboost_table', 1),
(9, '2024_03_25_112719_create_answers_table', 1),
(10, '2024_04_21_143529_admin_user', 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `personal_access_tokens`
--

CREATE TABLE `personal_access_tokens` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `tokenable_type` varchar(255) NOT NULL,
  `tokenable_id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(255) NOT NULL,
  `token` varchar(64) NOT NULL,
  `abilities` text DEFAULT NULL,
  `last_used_at` timestamp NULL DEFAULT NULL,
  `expires_at` timestamp NULL DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- A tábla adatainak kiíratása `personal_access_tokens`
--

INSERT INTO `personal_access_tokens` (`id`, `tokenable_type`, `tokenable_id`, `name`, `token`, `abilities`, `last_used_at`, `expires_at`, `created_at`, `updated_at`) VALUES
(1, 'App\\Models\\User', 17, 'AuthToken', '3afbe922281b772038a279635747fe46a6290216ab8eec44a7511071de536eee', '[\"*\"]', NULL, NULL, '2024-04-22 04:56:14', '2024-04-22 04:56:14');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `questions`
--

CREATE TABLE `questions` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `question_text` varchar(255) NOT NULL,
  `topic_id` bigint(20) UNSIGNED NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- A tábla adatainak kiíratása `questions`
--

INSERT INTO `questions` (`id`, `question_text`, `topic_id`, `created_at`, `updated_at`) VALUES
(1, 'Mennyi 3+4?', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(2, 'Melyik ország fővárosa Párizs?', 2, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(3, 'Ki volt Magyarország első királya?', 3, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(4, 'Mi a víz kémiailag kifejezett formulája?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(5, 'Ki írta a \'Láthatatlan ember\' című művet?', 5, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(6, 'Mennyi a négyzetgyök 81-ből?', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(7, 'Melyik kontinensen található az Amazonas?', 2, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(8, 'Mikor kezdődött a második világháború?', 3, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(9, 'Milyen elem a Hg?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(10, 'Ki írta a \'Május Tizenharmadikát\'?', 5, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(11, 'Milyen folyamat során keletkezik a fotoszintézis?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(12, 'Mi az abszolút nulla hőmérséklete?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(13, 'Melyik növény a legnagyobb szárazföldi?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(14, 'Ki volt az első ember a Holdon?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(15, 'Mi a Kossuth-díj célja?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(16, 'Hogyan nevezzük az emberi szív legnagyobb üregét?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(17, 'Melyik bolygó a Naprendszer legnagyobbja?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(18, 'Mi a fő összetevője a levegőnek?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(19, 'Ki volt a leghosszabb ideig uralkodó brit királynő?', 3, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(20, 'Milyen színű a rubin általában?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(21, 'Melyik évben nyerte meg Magyarország utoljára a labdarúgó Európa-bajnokságot?', 3, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(22, 'Mi a világ legnagyobb hegyrendszere?', 2, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(23, 'Ki volt az első nő aki elnyerte a Nobel-békedíjat?', 3, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(24, 'Milyen folyamat során alakul ki a cseppfolyós halmazállapot a víznél?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(25, 'Melyik film nyerte el 2020-ban az Oscar-díjat a legjobb film kategóriában?', 3, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(26, 'Ki volt az első ember aki megkerülte a Földet űrhajóval?', 3, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(27, 'Mi az E=mc^2 jelentése Albert Einstein relativitáselmélete szerint?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(28, 'Milyen égitest a legközelebb a Földhöz?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(29, 'Melyik állat a leghosszabb életű a Földön?', 4, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(30, 'Mi az eredménye a következő műveletnek: 5 + 3 * 2?', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(31, 'Mennyi 15 százaléka 80-nak?', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(32, 'Ha egy téglalap oldalai 3 és 5 egység hosszúak akkor mi a területe?', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(33, 'Mennyi az eredménye a következő műveletnek: 8 - (4 + 2)?', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(34, 'Mennyi az 1/4 és a 3/4 összege?', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(35, 'Mi az eredménye: 3 a négyzeten?', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(36, 'Melyik szám a következő sorozatban: 2 - 4 - 6 - 8 ...?', 1, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(37, 'Melyik kontinens a legnagyobb területű?', 2, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(38, 'Hol található a Föld legmagasabb hegycsúcsa a Mount Everest?', 2, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(39, 'Melyik ország Európa legnagyobb folyójának a Volgának a partján fekszik?', 2, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(40, 'Melyik tenger határolja Görögországot?', 2, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(41, 'Melyik a világ legnagyobb sivataga?', 2, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(42, 'Melyik kontinensen található Brazília?', 2, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(43, 'Melyik város az Egyesült Államok fővárosa?', 2, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(44, 'Mikor volt az Amerikai Függetlenségi Háború vége?', 3, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(45, 'Ki volt az Egyesült Államok első elnöke?', 3, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(46, 'Melyik évben kezdődött az első világháború?', 3, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(47, 'Ki írta a Harry Potter könyvsorozatot?', 5, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(48, 'Melyik William Shakespeare darabja kezdi azzal a híres sorral: \"Két háborúzó család Montague és Capulet...', 5, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(49, 'Ki a főszereplő az \"Odüsszeia\" című görög epikus költeményben?', 5, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(50, 'Melyik Charles Dickens regényben szerepel Twist Olivér?', 5, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(51, 'Ki írta a \"Háború és béke\" című regényt?', 5, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(52, 'Melyik klasszikus könyv szól egy kislányról aki egy csodálatos kertet fedez fel egy nagy angol birtokon?', 5, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(53, 'Ki írta az \"Állatfarm\" című politikai mese regényt?', 5, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(54, 'Melyik szereplő neve az \"Íjász Robin\" című angol legendában?', 5, '2024-04-22 04:46:49', '2024-04-22 04:46:49');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ranks`
--

CREATE TABLE `ranks` (
  `user_id` bigint(20) UNSIGNED NOT NULL,
  `score` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- A tábla adatainak kiíratása `ranks`
--

INSERT INTO `ranks` (`user_id`, `score`, `created_at`, `updated_at`) VALUES
(1, 0, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(7, 465, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(11, 208, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(1, 593, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(10, 260, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(13, 341, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(15, 124, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(4, 728, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(14, 258, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(8, 647, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(2, 484, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(9, 935, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(12, 189, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(16, 417, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(5, 665, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(6, 704, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(17, 0, '2024-04-22 04:55:42', '2024-04-22 04:55:42');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `topics`
--

CREATE TABLE `topics` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `topicname` varchar(255) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- A tábla adatainak kiíratása `topics`
--

INSERT INTO `topics` (`id`, `topicname`, `created_at`, `updated_at`) VALUES
(1, 'Matematika', NULL, NULL),
(2, 'Földrajz', NULL, NULL),
(3, 'Történelem', NULL, NULL),
(4, 'Tudomány', NULL, NULL),
(5, 'Irodalom', NULL, NULL);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `userboosts`
--

CREATE TABLE `userboosts` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `userid` bigint(20) UNSIGNED NOT NULL,
  `boosterid` bigint(20) UNSIGNED NOT NULL,
  `used` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `email_verified_at` timestamp NULL DEFAULT NULL,
  `password` varchar(255) NOT NULL,
  `avatar` varchar(255) DEFAULT '1',
  `gender` varchar(255) DEFAULT 'male',
  `is_active` tinyint(1) NOT NULL DEFAULT 1,
  `is_admin` tinyint(1) NOT NULL DEFAULT 0,
  `remember_token` varchar(100) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`id`, `name`, `email`, `email_verified_at`, `password`, `avatar`, `gender`, `is_active`, `is_admin`, `remember_token`, `created_at`, `updated_at`) VALUES
(1, 'admin', 'admin@petrik.hu', NULL, '$2y$12$NI4IVlD8uHP3tNmYINdXZuuDwyPdeRSOoguDhxF8m6dcvPG30Dcie', '1', 'male', 1, 1, NULL, '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(2, 'Dr. Péter Johanna PhD', 'qhorvath@example.net', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'female_avatar.jpg', 'female', 1, 0, 'N5EPBrdbED', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(3, 'Id. Szabó Judit PhD', 'emoke.racz@example.org', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'male_avatar.jpg', 'female', 1, 0, 'gUAZ1aA1zI', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(4, 'Gróf Gál Soma PhD', 'pasztor.kevin@example.com', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'female_avatar.jpg', 'male', 1, 0, 'p5uT6475Nj', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(5, 'Dr. Somogyi Géza PhD', 'peter.beatrix@example.net', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'female_avatar.jpg', 'male', 1, 0, '7TY2NsiXR8', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(6, 'Özv. Major Áron PhD', 'gabor.magyar@example.com', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'male_avatar.jpg', 'male', 1, 0, 'CTQBSmyOBS', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(7, 'Tóth Géza PhD', 'pjonas@example.net', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'male_avatar.jpg', 'male', 1, 0, 'RVNgCa7mqj', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(8, 'Budai Barnabás', 'gitta.jakab@example.org', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'male_avatar.jpg', 'male', 1, 0, 'JDdgkULRfC', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(9, 'Mészáros István', 'dorottya.szoke@example.com', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'male_avatar.jpg', 'male', 1, 0, 'gf3Soy4xbZ', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(10, 'Szűcsné Kelemen Szabina', 'zeteny.hajdu@example.net', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'male_avatar.jpg', 'female', 1, 0, '4oadEoeqJX', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(11, 'Dr. Jónás Péter PhD', 'bendeguz27@example.net', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'female_avatar.jpg', 'male', 1, 0, '9g74Ul43Ri', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(12, 'Szalai Milán', 'benedek73@example.net', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'female_avatar.jpg', 'female', 1, 0, 'Tw6xBDjYUZ', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(13, 'Magyar Gitta PhD', 'dorottya90@example.com', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'female_avatar.jpg', 'male', 1, 0, 'P5trwyUZof', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(14, 'Id. Major Alexa', 'toth.katinka@example.org', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'female_avatar.jpg', 'male', 1, 0, 'EWVsfqQnWn', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(15, 'Dr. Kis Vilmos', 'olah.szabina@example.net', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'female_avatar.jpg', 'female', 1, 0, 'orLfCiWPOr', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(16, 'Vörös Albertné', 'kfeher@example.net', '2024-04-22 04:46:49', '$2y$12$FOC1zJRPPpHbDAhVeI7GLeJMbiXGDU4s7zGDiRSGSg0G8IIELvWZ2', 'male_avatar.jpg', 'female', 1, 0, 'Pdy46EWOZj', '2024-04-22 04:46:49', '2024-04-22 04:46:49'),
(17, 'ati', 'ati@ati.hu', NULL, '$2y$10$u4h1m9he5qK6AwgLInULduU8rnFw7iYMt4VbIuFHG0s2VihlugB02', '1', 'male', 1, 0, NULL, '2024-04-22 04:55:42', '2024-04-22 04:55:42');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `answers`
--
ALTER TABLE `answers`
  ADD PRIMARY KEY (`id`),
  ADD KEY `answers_question_id_foreign` (`question_id`);

--
-- A tábla indexei `boosters`
--
ALTER TABLE `boosters`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `failed_jobs`
--
ALTER TABLE `failed_jobs`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `failed_jobs_uuid_unique` (`uuid`);

--
-- A tábla indexei `migrations`
--
ALTER TABLE `migrations`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `personal_access_tokens`
--
ALTER TABLE `personal_access_tokens`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `personal_access_tokens_token_unique` (`token`),
  ADD KEY `personal_access_tokens_tokenable_type_tokenable_id_index` (`tokenable_type`,`tokenable_id`);

--
-- A tábla indexei `questions`
--
ALTER TABLE `questions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `questions_topic_id_foreign` (`topic_id`);

--
-- A tábla indexei `ranks`
--
ALTER TABLE `ranks`
  ADD KEY `ranks_user_id_foreign` (`user_id`);

--
-- A tábla indexei `topics`
--
ALTER TABLE `topics`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `userboosts`
--
ALTER TABLE `userboosts`
  ADD PRIMARY KEY (`id`),
  ADD KEY `userboosts_userid_foreign` (`userid`),
  ADD KEY `userboosts_boosterid_foreign` (`boosterid`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `users_email_unique` (`email`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `answers`
--
ALTER TABLE `answers`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=217;

--
-- AUTO_INCREMENT a táblához `boosters`
--
ALTER TABLE `boosters`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `failed_jobs`
--
ALTER TABLE `failed_jobs`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `migrations`
--
ALTER TABLE `migrations`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT a táblához `personal_access_tokens`
--
ALTER TABLE `personal_access_tokens`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `questions`
--
ALTER TABLE `questions`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=55;

--
-- AUTO_INCREMENT a táblához `topics`
--
ALTER TABLE `topics`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT a táblához `userboosts`
--
ALTER TABLE `userboosts`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `answers`
--
ALTER TABLE `answers`
  ADD CONSTRAINT `answers_question_id_foreign` FOREIGN KEY (`question_id`) REFERENCES `questions` (`id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `questions`
--
ALTER TABLE `questions`
  ADD CONSTRAINT `questions_topic_id_foreign` FOREIGN KEY (`topic_id`) REFERENCES `topics` (`id`) ON DELETE CASCADE;

--
-- Megkötések a táblához `ranks`
--
ALTER TABLE `ranks`
  ADD CONSTRAINT `ranks_user_id_foreign` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

--
-- Megkötések a táblához `userboosts`
--
ALTER TABLE `userboosts`
  ADD CONSTRAINT `userboosts_boosterid_foreign` FOREIGN KEY (`boosterid`) REFERENCES `boosters` (`id`),
  ADD CONSTRAINT `userboosts_userid_foreign` FOREIGN KEY (`userid`) REFERENCES `users` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
