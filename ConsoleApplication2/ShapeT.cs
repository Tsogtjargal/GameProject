using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class ShapeT : Shape
    {
        public static int[,] T = new int[2, 3] { { 0, 1, 0 }, { 1, 1, 1 } };//3
        public ShapeT()
        {
            shape = T;
        }
    }
}
