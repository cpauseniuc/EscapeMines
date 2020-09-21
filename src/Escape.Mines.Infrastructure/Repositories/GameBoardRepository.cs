using Escape.Mines.Domain.Interfaces.Repositories;
using Escape.Mines.Domain.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Escape.Mines.Infrastructure.Repositories
{
    public class GameBoardRepository : IGameBoardRepository
    {
        ConfigurationData configurationData;
        public GameBoardRepository(IOptions<ConfigurationData> _configurationData)
        {
            configurationData = _configurationData.Value;
        }
        public Task<GameBoard> GetGameBoard() 
        {            
            return LoadAsync();
        }

        private async Task<GameBoard> LoadAsync()
        {         

            return await Task.Run(() =>
            {
                if (!File.Exists(configurationData.Configuration))
                    throw new Exception("Configuration file not found");
                var rows = File.ReadAllLines(configurationData.Configuration);
                var gameBoard = LoadGameBoard(rows[0]);
                gameBoard.Mines = GetMinesPositions(rows[1]);
                gameBoard.ExitPoint = GetExitPosition(rows[2]);
                return gameBoard;
            });
        }
        private GameBoard LoadGameBoard(string row)
        {
            string[] size = row.Split(" ");
            return new GameBoard() { Width = Convert.ToInt32(size[0]), Height = Convert.ToInt32(size[1]) };
        }
        private List<Position> GetMinesPositions(string row)
        {
            string[] minesString = row.Split(" ");
            List<Position> mines = new List<Position>();
            foreach (string mineString in minesString)
            {
                string[] mine = mineString.Split(",");
                if (mine.Length > 1)
                {
                    mines.Add(new Position() { X = Convert.ToInt32(mine[0]), Y = Convert.ToInt32(mine[1]) });
                }
            }
            return mines;
        }
        private Position GetExitPosition(string row)
        {
            string[] exitString = row.Split(" ");
            return new Position() { X = Convert.ToInt32(exitString[0]), Y = Convert.ToInt32(exitString[1]) };

        }

    }
}
