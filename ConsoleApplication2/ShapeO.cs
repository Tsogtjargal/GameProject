using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class ShapeO : Shape
    {
        public static int[,] O = new int[2, 2] { { 1, 1 }, { 1, 1 } };
        public ShapeO()
        {
            shape = O;
        }
    }
}
