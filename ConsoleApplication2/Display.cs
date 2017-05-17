using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Display
    {
        public static void ShowInfos(int[,] grid, int level, int score, int linesCleared)
        {
            Console.SetCursorPosition(grid.GetLength(1) * 2 + 5, 0);
            Console.WriteLine("Level " + level);
            Console.SetCursorPosition(grid.GetLength(1) * 2 + 5, 1);
            Console.WriteLine("Score " + score);
            Console.SetCursorPosition(grid.GetLength(1) * 2 + 5, 2);
            Console.WriteLine("LinesCleared " + linesCleared);
        }
        public static void PressAnyKey()
        {
            Console.SetCursorPosition(4, 5);
            Console.WriteLine("Press any key");
            Console.ReadKey(true);
        }
        public static bool PromptRestart()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Game Over \n Replay? (Y/N)");
            string input = Console.ReadLine();
            if (input == "y" || input == "Y")
                return true;
            else return false;
        }
        public static void DrawShape(int [,] grid, int [,] droppedtetrominoeLocationGrid, string SQR)
        {
            for (int i = 0; i < grid.GetLength(0); ++i)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.SetCursorPosition(1 + 2 * j, i);
                    if (grid[i, j] == 1 | droppedtetrominoeLocationGrid[i, j] == 1)
                    {
                        Console.SetCursorPosition(1 + 2 * j, i);
                        Console.Write(SQR);
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }

            }
        }
        public static void DrawBoard(int[,] grid)
        {
            for (int lengthCount = 0; lengthCount <= grid.GetLength(0); ++lengthCount)
            {
                Console.SetCursorPosition(0, lengthCount);
                Console.Write("*");
                Console.SetCursorPosition(grid.GetLength(1) * 2 + 1, lengthCount);
                Console.Write("*");
            }
            Console.SetCursorPosition(0, grid.GetLength(0));
            for (int widthCount = 0; widthCount <= grid.GetLength(1); widthCount++)
            {
                Console.Write("*-");
            }

        }
    }
}
