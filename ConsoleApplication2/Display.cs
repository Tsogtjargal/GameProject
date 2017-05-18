using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Display
    {
        public static void ClearBoard()
        {
            for (int i = 0; i < Program.grid.GetLength(0); i++)
            {
                for (int j = 0; j < Program.grid.GetLength(1); j++)
                {
                    Program.grid[i, j] = 0;
                }
            }
        }
        public static void ShowInfos()
        {
            Console.SetCursorPosition(Program.grid.GetLength(1) * 2 + 5, 0);
            Console.WriteLine("Level " + Program.level);
            Console.SetCursorPosition(Program.grid.GetLength(1) * 2 + 5, 1);
            Console.WriteLine("Score " + Program.score);
            Console.SetCursorPosition(Program.grid.GetLength(1) * 2 + 5, 2);
            Console.WriteLine("LinesCleared " + Program.linesCleared);
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
        public static void DrawShape()
        {
            for (int i = 0; i < Program.grid.GetLength(0); ++i)
            {
                for (int j = 0; j < Program.grid.GetLength(1); j++)
                {
                    Console.SetCursorPosition(1 + 2 * j, i);
                    if (Program.grid[i, j] == 1 | Program.droppedtetrominoeLocationGrid[i, j] == 1)
                    {
                        Console.SetCursorPosition(1 + 2 * j, i);
                        Console.Write(Program.SQR);
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }

            }
        }
        public void DrawBoard()
        {
            for (int lengthCount = 0; lengthCount <= Program.grid.GetLength(0); ++lengthCount)
            {
                Console.SetCursorPosition(0, lengthCount);
                Console.Write("*");
                Console.SetCursorPosition(Program.grid.GetLength(1) * 2 + 1, lengthCount);
                Console.Write("*");
            }
            Console.SetCursorPosition(0, Program.grid.GetLength(0));
            for (int widthCount = 0; widthCount <= Program.grid.GetLength(1); widthCount++)
            {
                Console.Write("*-");
            }

        }
    }
}
