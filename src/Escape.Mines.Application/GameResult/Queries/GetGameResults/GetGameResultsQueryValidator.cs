using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;

namespace Escape.Mines.Application.GameResult.Queries.GetGameResults
{
    public class GetGameResultsQueryValidator : AbstractValidator<GetGameResultsQuery>
    {
        public GetGameResultsQueryValidator()
        {
            RuleFor(v => v.GameSettings)
                .NotNull();
            RuleFor(v => v.GameSettings.GameBoard)
                .NotNull();
            RuleFor(v => v.GameSettings.GameBoard.ExitPoint)
               .NotNull();
            RuleFor(v => v.GameSettings.Turtle)
              .NotNull();
            RuleFor(v => v.GameSettings.Turtle.Position)
              .NotNull();
            RuleFor(v => v.GameSettings.Turtle.Position.X)                
                .NotEqual(v => v.GameSettings.GameBoard.ExitPoint.X)
                .When(v => v.GameSettings.Turtle.Position.Y == v.GameSettings.GameBoard.ExitPoint.Y)                
                .WithMessage("Start position can not be exit position");
            RuleForEach(v => v.GameSettings.GameBoard.Mines)
                .Must((v,minePosition) => minePosition.X != v.GameSettings.Turtle.Position.X || minePosition.Y != v.GameSettings.Turtle.Position.Y)
                .WithMessage("Start position can not be a mine position");            

        }
    }
}
