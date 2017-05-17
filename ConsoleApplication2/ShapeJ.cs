using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class ShapeJ : Shape
    {
        public static int[,] J = new int[2, 3] { { 1, 0, 0 }, { 1, 1, 1 } };//3
        public ShapeJ()
        {
            shape = J;
        }
    }
}
