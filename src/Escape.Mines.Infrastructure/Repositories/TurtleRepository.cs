using Escape.Mines.Common.Enums;
using Escape.Mines.Domain.Interfaces.Repositories;
using Escape.Mines.Domain.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape.Mines.Infrastructure.Repositories
{
    public class TurtleRepository : ITurtleRepository
    {
        ConfigurationData configurationData;
        public TurtleRepository(IOptions<ConfigurationData> _configurationData)
        {
            configurationData = _configurationData.Value;
        }

        public Task<Turtle> GetTurtle()
        {            
            return LoadAsync();
        }
        private async Task<Turtle> LoadAsync()
        {
            if (!File.Exists(configurationData.Configuration))
                throw new Exception("Configuration file not found");
            return await Task.Run(() =>
            {               
                var rows = File.ReadAllLines(configurationData.Configuration);
                var turtle = LoadTurtle(rows[3]);
                turtle.MoveOperations = GetMoveOperations(rows[4..]);
                return turtle;
            });
        }
        private Turtle LoadTurtle(string row)
        {
            string[] turtleData= row.Split(" ");         

            return new Turtle()
            {
                Direction = (CardinalDirection)Enum.Parse(typeof(CardinalDirection), turtleData[2]),
                Position = new Position() { X = Convert.ToInt32(turtleData[0]), Y = Convert.ToInt32(turtleData[1]) }
            };
        }
        private List<List<MoveOperation>> GetMoveOperations(string[] rows)
        {
            List<List<MoveOperation>> moveOperations = new List<List<MoveOperation>>();
            rows.ToList().ForEach(line =>
            {
                List<MoveOperation> movesRow = new List<MoveOperation>();

                line.Split(' ').ToList().ForEach(x =>
                {
                    movesRow.Add((MoveOperation)Enum.Parse(typeof(MoveOperation), x));
                });
                moveOperations.Add(movesRow);
            });

            return moveOperations;
        }
    }
}
