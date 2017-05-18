using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;
using System.Media;
using System.Resources;
using System.IO;
using System.Reflection;
namespace Tetris
{
    class Program
    {
        //consts
        public const string SQR = "■";
        
        //public
        public static int[,] grid = new int[23, 10];
        public static int[,] droppedtetrominoeLocationGrid = new int[grid.GetLength(0), grid.GetLength(1)];
        public static ConsoleKeyInfo key;
        public static bool isDropped = false;
        public static int linesCleared = 0, score = 0, level = 1;

        //default
        static Shape currentShapeObject = null;
        static Shape nextShapeObject = null;
        static Random random = new Random();

        //private
        private static Stopwatch dropTimer = new Stopwatch();
        private static int dropTime, dropRate = 300;
        private static bool isKeyPressed = false;

        public static void Main()
        {
            Display display = new Display();
            display.DrawBoard();
            GameStart();
            Display.ShowInfos();
            Update();
            PromptStart();
        }
        private static void Update()
        {
            while (true)//Update Loop
            {
                dropTime = (int)dropTimer.ElapsedMilliseconds;

                if (dropTime > dropRate)
                {
                    dropTime = 0;
                    dropTimer.Restart();
                    currentShapeObject.Drop();
                }
                if (isDropped == true)
                {
                    NextPiece();
                    isDropped = false;
                }

                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (droppedtetrominoeLocationGrid[0, j] == 1)
                        return;
                }
                Input();
                CheckClear();
            } //end Update
        }
        private static void Input()
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey();
                isKeyPressed = true;
                Debug.Assert(isKeyPressed == true, "false");
            }
            else
                isKeyPressed = false;
            if (isKeyPressed)
            {
                if (Program.key.Key == ConsoleKey.LeftArrow & !currentShapeObject.isSomethingDir(0,-1))
                    MoveDir(-1);
                else if (Program.key.Key == ConsoleKey.RightArrow & !currentShapeObject.isSomethingDir(Program.grid.GetLength(1)-1, 1))
                    MoveDir(1);
                if (Program.key.Key == ConsoleKey.DownArrow)
                    currentShapeObject.Drop();
                if (Program.key.Key == ConsoleKey.Spacebar)
                {
                    for (; currentShapeObject.isSomethingBelow() != true; )
                    {
                        currentShapeObject.Drop();
                    }
                    Debug.Assert(isDropped == false, "shape is dropped");
                }
                if (Program.key.Key == ConsoleKey.UpArrow)
                {
                    currentShapeObject.Rotate();
                    currentShapeObject.Update();
                }
            }

        }
        private static void GameStart()
        {
            Display.PressAnyKey();
            dropTimer.Start();
            nextShapeObject = Shape.tetrominoes[random.Next(0, Shape.tetrominoes.Length)];
            NextPiece();
        }
        private static void PromptStart()
        {
            if (Display.PromptRestart())
                Restart();
        }
        private static void Restart()
        {
            int[,] grid = new int[droppedtetrominoeLocationGrid.GetLength(0), droppedtetrominoeLocationGrid.GetLength(1)];
            droppedtetrominoeLocationGrid = new int[grid.GetLength(0), grid.GetLength(1)];
            dropTimer = new Stopwatch();
            dropRate = 300;
            isDropped = false;
            isKeyPressed = false;
            linesCleared = 0;
            score = 0;
            level = 1;
            GC.Collect();
            Console.Clear();
            Main();
        }
        private static void CheckClear()
        {
            int combo = 0;
            for (int column = 0; column < grid.GetLength(0); column++)
            {
                int row;
                for (row = 0; row < grid.GetLength(1); row++)
                {
                    if (droppedtetrominoeLocationGrid[column, row] == 0)
                        break;
                }
                if (row == grid.GetLength(1))
                {
                    combo++;
                    linesCleared++;

                    for (row = 0; row < grid.GetLength(1); row++)
                    {
                        droppedtetrominoeLocationGrid[column, row] = 0;
                    }
                    int[,] newdroppedtetrominoeLocationGrid = new int[grid.GetLength(0), grid.GetLength(1)];
                    for (int k = 1; k < column; k++)
                    {
                        for (int l = 0; l < grid.GetLength(1); l++)
                        {
                            newdroppedtetrominoeLocationGrid[k + 1, l] = droppedtetrominoeLocationGrid[k, l];
                        }
                    }
                    for (int k = 1; k < column; k++)
                    {
                        for (int l = 0; l < grid.GetLength(1); l++)
                        {
                            droppedtetrominoeLocationGrid[k, l] = 0;
                        }
                    }
                    for (int k = 0; k < grid.GetLength(0); k++)
                        for (int l = 0; l < grid.GetLength(1); l++)
                            if (newdroppedtetrominoeLocationGrid[k, l] == 1)
                                droppedtetrominoeLocationGrid[k, l] = 1;
                    Display.DrawShape();
                }
            }
            ClearBlock(0, 0, combo);
        }
        private static void ClearBlock(int column, int row, int combo)
        {
            if (combo > 0)
            {
                ScoreCount(combo);
                LevelCheck();
                Display.ShowInfos();
            }
            dropRate = 300 - 22 * level;
            Debug.Assert(0<300 - 22 * level, "droprate is negative");
            
        }
        private static void ScoreCount(int combo)
        {
            if (combo == 1)
                score += 40;
            else if (combo == 2)
                score += 100;
            else if (combo == 3)
                score += 300;
            else if (combo > 3)
                score += 300 * combo / 2;

        }
        private static void LevelCheck()
        {
            if (linesCleared < 5) level = 1;
            else if (linesCleared < 10) level = 2;
            else if (linesCleared < 15) level = 3;
            else if (linesCleared < 25) level = 4;
            else if (linesCleared < 35) level = 5;
            else if (linesCleared < 50) level = 6;
            else if (linesCleared < 70) level = 7;
            else if (linesCleared < 90) level = 8;
            else if (linesCleared < 110) level = 9;
            else if (linesCleared < 150) level = 10;
            Debug.Assert(linesCleared < 150, "Level wont increase");
        }
        private static void NextPiece()
        {
            try
            {
                currentShapeObject = nextShapeObject;
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine(e+" shape is null");
            }
            try
            {
                nextShapeObject = Shape.tetrominoes[random.Next(0, Shape.tetrominoes.Length)];
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e+" next shape is null");
            }
            currentShapeObject.Spawn();
        }
        private static void MoveDir(int dir)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    currentShapeObject.location[i][1] += dir;
                }

            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine(e + " is out of range");
            }
            currentShapeObject.Update();
        }

    }

}
