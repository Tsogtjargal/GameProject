using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class ShapeL : Shape
    {
        public static int[,] L = new int[2, 3] { { 0, 0, 1 }, { 1, 1, 1 } };//3
        public ShapeL()
        {
            shape = L;
        }
    }
}
