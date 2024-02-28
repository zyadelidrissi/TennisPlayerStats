using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using TennisPlayersStats.Models;
using System;
using System.Linq;


[Route("api/[controller]")]
[ApiController]
public class PlayersController : ControllerBase
{
    private readonly List<Player> _players;

    public PlayersController()
    {
        try
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "headtohead.json");

            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                var playerList = JsonConvert.DeserializeObject<PlayerList>(json);

                if (playerList != null && playerList.Players != null)
                {
                    _players = playerList.Players;
                }
                else
                {
                    _players = new List<Player>();
                }
            }
            else
            {
                _players = new List<Player>();
            }
        }
        catch (Exception ex)
        {
            // Log the exception 
            Console.WriteLine($"An error occurred during initialization: {ex.Message}");
            _players = new List<Player>();
        }
    }


    [HttpGet]
    public ActionResult<IEnumerable<Player>> GetPlayers()
    {
        try
        {
            return _players;
        }
        catch (Exception ex)
        {
            // Logs or handles the exception
            Console.WriteLine($"An error occurred in GetPlayers: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("sortedByRank")]
    public ActionResult<IEnumerable<Player>> GetSortedPlayers()
    {
        try
        {
            var sortedPlayers = _players.OrderBy(p => p.Data.Rank).ToList();
            return sortedPlayers;
        }
        catch (Exception ex)
        {
            // Logs or handles the exception
            Console.WriteLine($"An error occurred in GetSortedPlayers: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Player> GetPlayerById(int id)
    {
        try
        {
            var player = _players.FirstOrDefault(p => p.Id == id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in GetPlayerById: {ex.Message}");
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    [HttpGet("statistics")]
    public ActionResult<object> GetStatistics()
    {
        try
        {
            if (_players.Count == 0)
            {
                return NotFound("No players available.");
            }

            // Calcul du pays avec le plus grand ratio de parties gagnées
            var countryWithHighestWinRatio = GetCountryWithHighestWinRatio();

            // Calcul de l'IMC moyen de tous les joueurs
            var averageIMC = GetAverageIMC();

            // Calcul de la médiane de la taille des joueurs
            var medianHeight = GetMedianHeight();

            var statistics = new
            {
                CountryWithHighestWinRatio = countryWithHighestWinRatio,
                AverageIMC = averageIMC,
                MedianHeight = medianHeight
            };

            return statistics;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in GetStatistics: {ex.Message}");
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    private double GetAverageIMC()
    {
        try
        {
            if (_players.Any())
            {
                double totalIMC = 0;

                foreach (var player in _players)
                {
                    // Formule de l'IMC : poids en kg / taille * taille en mètres
                    double heightInMeters = player.Data.Height / 100.0; // On converti la taille de cm à mètres
                    double weightInKilograms = player.Data.Weight / 1000.0; // Convert weight from grams to kilograms

                    double imc = weightInKilograms / (heightInMeters * heightInMeters);
                    totalIMC += imc;
                }

                double averageIMC = totalIMC / _players.Count;

                // On se limite a un chiffre après la virgule
                return Math.Round(averageIMC, 1);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in GetAverageIMC: {ex.Message}");
            return 0.0;
        }
        
        return 0.0;
    }

    private string? GetCountryWithHighestWinRatio()
    {
        try
        {
            if (_players.Any())
            {
                // On cherche le ratio de parties gagnés par pays
                var countryWinRatios = _players
                    .GroupBy(player => player?.Country?.Code)
                    .Select(group => new
                    {
                        CountryCode = group.Key,
                        AverageWinRatio = group.Average(player => player.Data.Last.Any() ? player.Data.Last.Average() : 0)
                    });

                // Trouve le pays avec le plus grand ratio moyen
                var countryWithHighestWinRatio = countryWinRatios.OrderByDescending(result => result.AverageWinRatio).FirstOrDefault();

                // Retourne le code du pays avec le plus grand ratio moyen
                return countryWithHighestWinRatio?.CountryCode;
            }
            return null;
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            Console.WriteLine($"An error occurred in GetCountryWithHighestWinRatio: {ex.Message}");
            return null;
        }
    }

    private int GetMedianHeight()
    {
        try
        {
            if (_players.Any())
            {
                // Calcule la taille médiane en fonction des hauteurs des joueurs
                var sortedHeights = _players.Select(player => player.Data.Height).OrderBy(height => height).ToList();
                int count = sortedHeights.Count;

                if (count % 2 == 0)
                {
                    // Si le nombre est pair, on prend la moyenne des deux valeurs du milieu
                    int middleIndex1 = count / 2 - 1;
                    int middleIndex2 = count / 2;
                    return (sortedHeights[middleIndex1] + sortedHeights[middleIndex2]) / 2;
                }
                else
                {
                    // Si le nombre est impair, on prend la valeur du milieu
                    int middleIndex = count / 2;
                    return sortedHeights[middleIndex];
                }
            }

            // Retourne une valeur par défaut si aucun joueur n'est disponible
            return 0;
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            Console.WriteLine($"An error occurred in GetMedianHeight: {ex.Message}");
            return 0;
        }
    }
}