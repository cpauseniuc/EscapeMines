using AutoMapper;
using Escape.Mines.Application.Common.Mapping;
using Escape.Mines.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Escape.Mines.Application.GameResult
{
    public class GameResultDto : IMapFrom<Domain.Model.GameResult>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.GameResult, GameResultDto>();
        }
        public List<MoveOperation> MoveOperations { get; set; }
        public Status Status { get; set; }
    }
}
