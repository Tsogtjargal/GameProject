using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public abstract class Shape
    {
        static ShapeI shapeI = new ShapeI();
        static ShapeO shapeO = new ShapeO();
        static ShapeT shapeT = new ShapeT();
        static ShapeS shapeS = new ShapeS();
        static ShapeZ shapeZ = new ShapeZ();
        static ShapeJ shapeJ = new ShapeJ();
        static ShapeL shapeL = new ShapeL();
        public int[,] shape;
        public List<int[]> location = new List<int[]>();
        public static Shape[] tetrominoes = new Shape[7] { shapeI, shapeO, shapeT, shapeS, shapeZ, shapeJ, shapeL};

        public void Rotate()
        {
            List<int[]> templocation = new List<int[]>();
            templocation = CopyLocation(templocation);
            for (int i = 0; i < location.Count; i++)
            {
                templocation[i] = TransformMatrix(location[i], location[2], "Clockwise");
            }
            for (int count = 0; isOverlayDir(templocation, 'l') != false | isOverlayDir(templocation, 'r') != false | isOverlayBelow(templocation) != false; count++)
            {
                templocation = LocationChange(templocation);
                if (count == 3)
                {
                    return;
                }
            }
            location = templocation;

        }
        public void Spawn()
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] == 1)
                    {
                        location.Add(new int[] { i, (shape.GetLength(1) - shape.GetLength(1)) / 2 + j });
                    }
                }
            }
            Update();
        }
        public void Drop()
        {

            if (isSomethingBelow())
            {
                for (int i = 0; i < 4; i++)
                {
                    Program.droppedtetrominoeLocationGrid[location[i][0], location[i][1]] = 1;
                }
                Program.isDropped = true;
            }
            else
            {
                for (int numCount = 0; numCount < 4; numCount++)
                {
                    location[numCount][0] += 1;
                }
                Update();
            }
        }
        public void Update()
        {
            Display.ClearBoard();
            for (int i = 0; i < 4; i++)
            {
                Program.grid[location[i][0], location[i][1]] = 1;
            }
            Display.DrawShape();
        }
        public bool isSomethingBelow()
        {
            for (int i = 0; i < 4; i++)
            {
                if (location[i][0] + 1 >= Program.grid.GetLength(0))
                    return true;
                if (location[i][0] + 1 < Program.grid.GetLength(0))
                {
                    if (Program.droppedtetrominoeLocationGrid[location[i][0] + 1, location[i][1]] == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool isSomethingDir(int max, int dir)
        {
            for (int i = 0; i < 4; i++)
            {
                if (location[i][1] == max)
                {
                    return true;
                }
                else if (Program.droppedtetrominoeLocationGrid[location[i][0], location[i][1] + dir] == 1)
                {
                    return true;
                }
            }
            return false;
        }
        private bool? isOverlayBelow(List<int[]> location)
        {
            List<int> ycoords = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                ycoords.Add(location[i][0]);
                if (location[i][0] >= Program.grid.GetLength(0)-1)
                    return true;
                if (location[i][0] < 0)
                    return null;
                if (location[i][1] < 0)
                {
                    return null;
                }
                if (location[i][1] > Program.grid.GetLength(1)-1)
                {
                    return null;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (ycoords.Max() - ycoords.Min() == 3)
                {
                    if (ycoords.Max() == location[i][0] | ycoords.Max() - 1 == location[i][0])
                    {
                        if (Program.droppedtetrominoeLocationGrid[location[i][0], location[i][1]] == 1)
                        {
                            return true;
                        }
                    }

                }
                else
                {
                    if (ycoords.Max() == location[i][0])
                    {
                        if (Program.droppedtetrominoeLocationGrid[location[i][0], location[i][1]] == 1)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        private bool? isOverlayDir(List<int[]> location, char c)
        {
            List<int> xcoords = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                xcoords.Add(location[i][1]);
                if (location[i][1] > Program.grid.GetLength(1)-1)
                    return true;
                if (location[i][1] < 0)
                    return false;
                if (location[i][0] >= Program.grid.GetLength(0)-1)
                    return null;
                if (location[i][0] < 0)
                    return null;
            }

            for (int i = 0; i < 4; i++)
            {
                if (xcoords.Max() - xcoords.Min() == 3)
                {
                    if (c == 'l')
                    {
                        if (xcoords.Min() == location[i][1] | xcoords.Min() + 1 == location[i][1])
                        {
                            if (CheckDropped(i))
                                return true;
                            else
                            {
                                if (xcoords.Min() == location[i][1])
                                    if (CheckDropped(i))
                                        return true;
                            }
                        }

                    }
                    else if (c == 'r')
                    {
                        if (xcoords.Max() == location[i][1] | xcoords.Max() - 1 == location[i][1])
                            if (CheckDropped(i))
                                return true;
                            else
                            {
                                if (xcoords.Max() == location[i][1])
                                    if (CheckDropped(i))
                                        return true;
                            }
                    }
                }
            }
            return false;
        }
        private bool CheckDropped(int i)
        {
            if (Program.droppedtetrominoeLocationGrid[location[i][0], location[i][1]] == 1)
                return true;
            return false;
        }
        private int[] TransformMatrix(int[] coord, int[] axis, string dir)
        {
            int[] pcoord = { coord[0] - axis[0], coord[1] - axis[1] };
            if (dir == "Counterclockwise")
            {
                pcoord = new int[] { -pcoord[1], pcoord[0] };
            }
            else if (dir == "Clockwise")
            {
                pcoord = new int[] { pcoord[1], -pcoord[0] };
            }

            return new int[] { pcoord[0] + axis[0], pcoord[1] + axis[1] };
        }
        private List<int[]> CopyLocation(List<int[]> templocation)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] == 1)
                    {
                        templocation.Add(new int[] { i, (shape.GetLength(1) - shape.GetLength(1)) / 2 + j });
                    }
                }
            }
            return templocation;
        }
        private List<int[]> LocationChange(List<int[]> templocation)
        {

            if (isOverlayDir(templocation, 'l') == true)
            {
                for (int i = 0; i < location.Count; i++)
                {
                    templocation[i][1] += 1;
                }
            }

            if (isOverlayDir(templocation, 'r') == true)
            {
                for (int i = 0; i < location.Count; i++)
                {
                    templocation[i][1] -= 1;
                }
            }
            if (isOverlayBelow(templocation) == true)
            {
                for (int i = 0; i < location.Count; i++)
                {
                    templocation[i][0] -= 1;
                }
            }
            return templocation;

        }
    }

}
