using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class ShapeS : Shape
    {
        public static int[,] S = new int[2, 3] { { 0, 1, 1 }, { 1, 1, 0 } };//4
        public ShapeS()
        {
            shape = S;
        }
    }
}
