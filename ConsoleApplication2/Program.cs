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
    static class Program
    {
        //public

        public static string SQR = "■";
        public static int[,] grid = new int[23, 10];
        public static int[,] droppedtetrominoeLocationGrid = new int[grid.GetLength(0), grid.GetLength(1)];
        public static ConsoleKeyInfo key;
        public static bool isDropped = false;

        //default
        static Tetrominoe tet;
        static Tetrominoe nexttet;

        //private
        private static Stopwatch dropTimer = new Stopwatch();
        private static int dropTime, dropRate = 300;
        private static int linesCleared = 0, score = 0, level = 1;
        private static bool isKeyPressed = false;

        static void Main()
        {
            Display.DrawBoard(grid);
            GameStart();
            Display.ShowInfos(grid, level, score, linesCleared);
            Update();
            PromptStart();
        }
        private static void GameStart()
        {
            Display.PressAnyKey();
            dropTimer.Start();
            nexttet = new Tetrominoe();
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

        private static void Update()
        {
            while (true)//Update Loop
            {
                dropTime = (int)dropTimer.ElapsedMilliseconds;
                
                if (dropTime > dropRate)
                {
                    dropTime = 0;
                    dropTimer.Restart();
                    tet.Drop();
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
        private static void ClearBlock(int column, int row, int combo)
        {
            if (combo > 0)
            {
                ScoreCount(combo);
                LevelCheck();
                Display.ShowInfos(grid, level, score, linesCleared);
            }
            dropRate = 300 - 22 * level;

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
                    Display.DrawShape(grid, droppedtetrominoeLocationGrid, SQR);
                }
            }
            ClearBlock(0, 0, combo);
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
        }
        private static void ScoreCount(int combo)
        {
            if (combo == 1)
                score += 40 ;
            else if (combo == 2)
                score += 100 ;
            else if (combo == 3)
                score += 300 ;
            else if (combo > 3)
                score += 300 * combo / 2 ;

        }
        private static void Input()
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey();
                isKeyPressed = true;
            }
            else
                isKeyPressed = false;
            if (isKeyPressed)
            {
                if (Program.key.Key == ConsoleKey.LeftArrow & !tet.isSomethingLeft())
                    MoveLeft();
                else if (Program.key.Key == ConsoleKey.RightArrow & !tet.isSomethingRight())
                    MoveRight();
                if (Program.key.Key == ConsoleKey.DownArrow)
                    tet.Drop();
                if (Program.key.Key == ConsoleKey.Spacebar)
                {
                    for (; tet.isSomethingBelow() != true; )
                    {
                        tet.Drop();
                    }
                }
                if (Program.key.Key == ConsoleKey.UpArrow)
                {
                    tet.Rotate();
                    tet.Update();
                }
            }
            
        }
        private static void NextPiece()
        {
            tet = nexttet;
            nexttet = new Tetrominoe();
            tet.Spawn();
        }


        public static void MoveLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                tet.location[i][1] -= 1;
            }
            tet.Update();
        }
        public static void MoveRight()
        {
            for (int i = 0; i < 4; i++)
            {
                tet.location[i][1] += 1;
            }
            tet.Update();
        }
    }

}
