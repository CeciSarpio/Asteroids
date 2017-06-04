using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.Logic
{
   public class Asteroid
    {
        public Guid guid  { get; set; }
        public int row { get; set; }
        public int col { get; set; }
    }
}
