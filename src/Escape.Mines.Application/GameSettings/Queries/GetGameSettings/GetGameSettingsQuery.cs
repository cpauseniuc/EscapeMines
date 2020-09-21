using AutoMapper;
using Escape.Mines.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Escape.Mines.Application.GameSettings.Queries.GetGameSettings
{

    public class GetGameSettingsQuery : IRequest<GameSettingsDto>
    {
    }
    public class GetGameSettingsQueryHandler : IRequestHandler<GetGameSettingsQuery, GameSettingsDto>
    {
        private readonly IMapper mapper;
        private readonly IGameBoardRepository gameBoardRepository;
        private readonly ITurtleRepository turtleRepository;

        public GetGameSettingsQueryHandler(IGameBoardRepository _gameBoardRepository, ITurtleRepository _turtleRepository, IMapper _mapper)
        {
            mapper = _mapper;
            gameBoardRepository = _gameBoardRepository;
            turtleRepository = _turtleRepository;
        }

        public async Task<GameSettingsDto> Handle(GetGameSettingsQuery request, CancellationToken cancellationToken)
        {


            var gameBoard = await gameBoardRepository.GetGameBoard();
            var turtle = await turtleRepository.GetTurtle();
            Domain.Model.GameSettings gameSettings = new Domain.Model.GameSettings
            {
                GameBoard = gameBoard,
                Turtle = turtle
            };
            return mapper.Map<GameSettingsDto>(gameSettings);            

        }
    }
}
