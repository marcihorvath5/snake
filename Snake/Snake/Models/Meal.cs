using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    public class Meal: GamePoint // Származtatjuk a GamePointból mert szeretnénk ha ugyan azok a tulajdonságai meglennének x,y, koordináta
    {
        public Meal(int x, int y) : base(x, y)
        {}

        public int Points { get; } = 3;
        
    }
}
