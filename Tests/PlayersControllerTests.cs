using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TennisPlayersStats.Models;

namespace TennisPlayersStats.Tests
{
    [TestClass]
    public class PlayersControllerTests
    {
        private PlayersController? _playersController;

        [TestInitialize]
        public void Setup()
        {
            // Initialisation du controller pour chaque test
            _playersController = new PlayersController();
        }

        [TestMethod]
        public void GetPlayers_ReturnsPlayersListTest()
        {
            var result = _playersController.GetPlayers();

            Assert.IsInstanceOfType(result.Value, typeof(List<Player>));
        }

        [TestMethod]
        public void TestGetSortedPlayers_ReturnsSortedPlayersList()
        {
            var result = _playersController?.GetSortedPlayers();

            // Assert
            Assert.IsInstanceOfType(result.Value, typeof(List<Player>));

            if (result.Value is List<Player> sortedPlayers)
            {
                for (int i = 1; i < sortedPlayers.Count; i++)
                {
                    // On s'assure que chaque joueur a un rang supérieur ou égal au joueur précédent
                    Assert.IsTrue(sortedPlayers[i].Data.Rank >= sortedPlayers[i - 1].Data.Rank);
                }
            }
            else
            {
                Assert.Fail("La valeur retournée n'est pas une liste de joueurs.");
            }
        }

        [TestMethod]
        public void TestGetPlayerById_ExistingId_ReturnsPlayer()
        {
            int existingPlayerId = 52;

            var result = _playersController?.GetPlayerById(existingPlayerId);

            Assert.IsInstanceOfType(result.Value, typeof(Player));
        }

        [TestMethod]
        public void TestGetPlayerById_NonExistingId_ReturnsNotFound()
        {
            int nonExistingPlayerId = -1;

            var result = _playersController?.GetPlayerById(nonExistingPlayerId);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void TestGetStatistics_ReturnsStatisticsObject()
        {
            var result = _playersController?.GetStatistics();

            Assert.IsInstanceOfType(result.Value, typeof(object));

            if (result.Value is object statistics)
            {
                // On verifie que l'objet contient les propriétés attendues
                Assert.IsTrue(statistics.GetType().GetProperty("CountryWithHighestWinRatio") != null);
                Assert.IsTrue(statistics.GetType().GetProperty("AverageIMC") != null);
                Assert.IsTrue(statistics.GetType().GetProperty("MedianHeight") != null);

                Assert.IsInstanceOfType(statistics.GetType().GetProperty("CountryWithHighestWinRatio")?.GetValue(statistics), typeof(string));
                Assert.IsInstanceOfType(statistics.GetType().GetProperty("AverageIMC")?.GetValue(statistics), typeof(double));
                Assert.IsInstanceOfType(statistics.GetType().GetProperty("MedianHeight")?.GetValue(statistics), typeof(int));
            }
            else
            {
                Assert.Fail("L'objet statistiques n'est pas du type attendu.");
            }
        }
    }
}