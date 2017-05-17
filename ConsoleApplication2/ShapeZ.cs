using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class ShapeZ : Shape
    {
        public static int[,] Z = new int[2, 3] { { 1, 1, 0 }, { 0, 1, 1 } };//3
        public ShapeZ()
        {
            shape = Z;
        }
    }
}
