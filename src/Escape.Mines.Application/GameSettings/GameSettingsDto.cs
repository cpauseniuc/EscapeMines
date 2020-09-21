using AutoMapper;
using Escape.Mines.Application.Common.Mapping;
using Escape.Mines.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Escape.Mines.Application.GameSettings
{
    public class GameSettingsDto : IMapFrom<Domain.Model.GameSettings>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.GameSettings, GameSettingsDto>();
        }
        public GameBoardDto GameBoard { get; set; }
        public TurtleDto Turtle { get; set; }
       
    }
    public class GameBoardDto : IMapFrom<Domain.Model.GameBoard>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.GameBoard, GameBoardDto>();
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<PositionDto> Mines { get; set; } = new List<PositionDto>();
        public PositionDto ExitPoint { get; set; }
    }
    public class PositionDto : IMapFrom<Domain.Model.Position>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Position, PositionDto>();
        }
        public int X { get; set; }
        public int Y { get; set; }

    }
    public class TurtleDto : IMapFrom<Domain.Model.Turtle>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Turtle, TurtleDto>();
        }
        public CardinalDirection Direction { get; set; }
        public PositionDto Position { get; set; }
        public List<List<MoveOperation>> MoveOperations { get; set; } = new List<List<MoveOperation>>();
    }
}
