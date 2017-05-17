using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris    
{
    class ShapeI : Shape
    {
        public static int[,] I = new int[1, 4] { { 1, 1, 1, 1 } };//3
        public ShapeI()
        {
            shape = I;
        }
    }
}
