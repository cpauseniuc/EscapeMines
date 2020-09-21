using Escape.Mines.Application.GameResult;
using Escape.Mines.Application.GameResult.Queries.GetGameResults;
using Escape.Mines.Application.GameSettings;
using Escape.Mines.Application.GameSettings.Queries.GetGameSettings;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape.Mines.Console
{
    public interface IGame
    {
        public Task Start();
    }
    public class Game : IGame
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        public Game(ILogger<IGame> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Start()
        {
           await Task.Delay(TimeSpan.FromSeconds(1));
           var gameSettings = await GetGameSettings();
           foreach (var result in await GetGameResults(gameSettings))
            {
                _logger.LogInformation($"The status for the move operations: {string.Join(' ', result.MoveOperations.Select(m => m.ToString()))} is: " + result.Status.ToString());
            }
        }
        private async Task<GameSettingsDto> GetGameSettings()
        {
            try
            {
                return await _mediator.Send(new GetGameSettingsQuery());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return new GameSettingsDto();
        }
        private async Task<List<GameResultDto>> GetGameResults( GameSettingsDto gameSettings)
        {
            try
            {
                return await _mediator.Send(new GetGameResultsQuery() { GameSettings = gameSettings });
            }
            catch(Escape.Mines.Application.Common.Exceptions.ValidationException vex)
            {
                var test = vex.Failures.Select(v => v.Value).ToList();
                foreach (var failure in vex.Failures)
                {
                    _logger.LogError("Validation Error: " + string.Join(' ', failure.Value));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

            }
            return new List<GameResultDto>();
            
            
        }
    }
}
