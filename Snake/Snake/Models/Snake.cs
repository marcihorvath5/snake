using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    /// <summary>
    /// A kígyót leíró osztály
    /// </summary>
    public class Snake
    {
        public int Length { get; set;  }
        /// <summary>
        /// Mutatja hogy a kígyó feje éppen merre áll
        /// </summary>
        public SnakeDirections Directon { get; set; }

        /// <summary>
        /// A kígyó pontjait tartalmazó lista
        /// </summary>
        public List<GamePoint> Gamepoints { get; set; }
    }
}
