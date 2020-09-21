using Escape.Mines.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Escape.Mines.Domain.Interfaces.Repositories
{
    public interface ITurtleRepository
    {
        Task<Turtle> GetTurtle();
    }
}
