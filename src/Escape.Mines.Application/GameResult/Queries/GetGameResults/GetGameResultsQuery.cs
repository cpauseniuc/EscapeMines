using AutoMapper;
using Escape.Mines.Application.GameSettings;
using Escape.Mines.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Escape.Mines.Application.GameResult.Queries.GetGameResults
{

    public class GetGameResultsQuery : IRequest<List<GameResultDto>>
    {
        public GameSettingsDto GameSettings { get; set; }
    }
    public class GetGameResultsQueryHandler : IRequestHandler<GetGameResultsQuery, List<GameResultDto>>
    {
        private readonly IMapper mapper;

        public GetGameResultsQueryHandler(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public async Task<List<GameResultDto>> Handle(GetGameResultsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await Task.CompletedTask;

                List<Domain.Model.GameResult> gameResults = new List<Domain.Model.GameResult>();
                Status status;
                PositionDto startingPosition = new PositionDto() { X = request.GameSettings.Turtle.Position.X, Y = request.GameSettings.Turtle.Position.Y };
                CardinalDirection startingDirection = request.GameSettings.Turtle.Direction;

                foreach (var moveOperations in request.GameSettings.Turtle.MoveOperations)
                {
                    status = Status.StillInDanger;
                    request.GameSettings.Turtle.Position = startingPosition;
                    request.GameSettings.Turtle.Direction = startingDirection;

                    foreach (MoveOperation move in moveOperations)
                    {
                        Move(move, request.GameSettings);

                        status = GetNewStatus(request.GameSettings);

                        if (status != Status.StillInDanger)
                        {
                            break;
                        }
                    }

                    gameResults.Add(new Domain.Model.GameResult() { MoveOperations = moveOperations, Status = status });
                }




                return mapper.Map<List<GameResultDto>>(gameResults);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        private Status GetNewStatus(GameSettingsDto gameSetting)
        {
            if (gameSetting.GameBoard.Mines.Where(m => m.X == gameSetting.Turtle.Position.X && m.Y == gameSetting.Turtle.Position.Y).Any())
                return Status.MineHit;
            else
                if (gameSetting.Turtle.Position.X == gameSetting.GameBoard.ExitPoint.X && gameSetting.Turtle.Position.Y == gameSetting.GameBoard.ExitPoint.Y)
                return Status.Success;
            return Status.StillInDanger;



        }

        private void Move(MoveOperation move, GameSettingsDto gameSettings)
        {
            switch (move)
            {
                case MoveOperation.M:
                    MoveFront(move, gameSettings);
                    break;
                case MoveOperation.R:
                    MoveRight(gameSettings);
                    break;
                case MoveOperation.L:
                    MoveLeft(gameSettings);
                    break;
            }
        }
        private void MoveRight(GameSettingsDto gameSettings)
        {
            CardinalDirection[] cardinalDirections = (CardinalDirection[])Enum.GetValues(typeof(CardinalDirection));
            int newIndex = Array.IndexOf(cardinalDirections, gameSettings.Turtle.Direction) + 1;
            gameSettings.Turtle.Direction = (newIndex == cardinalDirections.Length) ? cardinalDirections[0] : cardinalDirections[newIndex];
        }
        private void MoveLeft(GameSettingsDto gameSettings)
        {
            CardinalDirection[] cardinalDirections = (CardinalDirection[])Enum.GetValues(typeof(CardinalDirection));
            int newIndex = Array.IndexOf((CardinalDirection[])Enum.GetValues(typeof(CardinalDirection)), gameSettings.Turtle.Direction) - 1;
            gameSettings.Turtle.Direction = (newIndex == -1) ? cardinalDirections[cardinalDirections.Length - 1] : cardinalDirections[newIndex];
        }
        private void MoveFront(MoveOperation move, GameSettingsDto gameSettings)
        {
            PositionDto position = GetNewFrontPosition(gameSettings.Turtle);

            bool isValidMove = position.X >= 0 && position.X <= gameSettings.GameBoard.Width && position.Y >= 0 && position.Y <= gameSettings.GameBoard.Height;
            if (isValidMove)
            {
                gameSettings.Turtle.Position = position;
            }

        }
        private PositionDto GetNewFrontPosition(TurtleDto turtle)
        {
            PositionDto position;

            switch (turtle.Direction)
            {
                case (CardinalDirection.N):
                    position = new PositionDto() { X = turtle.Position.X, Y = turtle.Position.Y - 1 };
                    break;
                case (CardinalDirection.E):
                    position = new PositionDto() { X = turtle.Position.X + 1, Y = turtle.Position.Y };
                    break;
                case (CardinalDirection.S):
                    position = new PositionDto() { X = turtle.Position.X, Y = turtle.Position.Y + 1 };
                    break;
                case (CardinalDirection.W):
                    position = new PositionDto() { X = turtle.Position.X - 1, Y = turtle.Position.Y };
                    break;
                default:
                    throw new Exception("Invalid direction");
            }

            return position;
        }
    }
}
