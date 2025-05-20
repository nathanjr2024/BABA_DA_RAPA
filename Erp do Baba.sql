-- --------------------------------------------------------
-- Servidor:                     127.0.0.1
-- Versão do servidor:           11.4.5-MariaDB - mariadb.org binary distribution
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              12.10.0.7000
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Copiando estrutura do banco de dados para erp do baba
CREATE DATABASE IF NOT EXISTS `erp do baba` /*!40100 DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci */;
USE `erp do baba`;

-- Copiando estrutura para tabela erp do baba.jogadores
CREATE TABLE IF NOT EXISTS `jogadores` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `Idade` int(11) NOT NULL,
  `Posicao` varchar(20) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Copiando dados para a tabela erp do baba.jogadores: ~0 rows (aproximadamente)
INSERT INTO `jogadores` (`Id`, `Nome`, `Idade`, `Posicao`) VALUES
	(2, 'Cleive', 21, 'Atacante'),
	(3, 'Nathan', 23, 'Goleiro');

-- Copiando estrutura para tabela erp do baba.partidas
CREATE TABLE IF NOT EXISTS `partidas` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `TimeA` varchar(50) DEFAULT NULL,
  `TimeB` varchar(50) DEFAULT NULL,
  `GolsTimeA` int(11) NOT NULL,
  `GolsTimeB` int(11) NOT NULL,
  `DataDoJogo` date DEFAULT NULL,
  `Local` varchar(70) DEFAULT NULL,
  `TipoDeCampo` int(11) DEFAULT NULL,
  `QuantidadeDeJogadores` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Copiando dados para a tabela erp do baba.partidas: ~0 rows (aproximadamente)

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
