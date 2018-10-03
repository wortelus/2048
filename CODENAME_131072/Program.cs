using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODENAME_131072
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                GameClass game = new GameClass();
            }
        }
    }

    class GameClass
    {
        const int xGrid = 8;
        const int yGrid = 8;
        const int tileLength = 4;
        int score = 0;
        int moves = 0;
        int[,] grid = new int[xGrid, yGrid];

        public GameClass()
        {
            RemoveLostMessage();
            Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight); // for easy change
            Console.WriteLine("CODENAME_131072 - made by wortelus");
            Console.WriteLine("-----------------------------------");
            InitiateStartBlocks();
            RenderGrid();
            
            while(true)
            {
                ConsoleKey ck = Console.ReadKey(true).Key;
                if (ck == ConsoleKey.LeftArrow)
                {
                    //main loop
                    MoveLeft();
                }
                else if(ck == ConsoleKey.DownArrow)
                {
                    MoveDown();
                }
                else if(ck == ConsoleKey.UpArrow)
                {
                    MoveUp();
                }
                else if (ck == ConsoleKey.RightArrow)
                {
                    MoveRight();
                }
                else if (ck == ConsoleKey.Insert)
                {
                    StupidAI();
                }
                RenderGrid();
                if (CheckMoveDownPossibility() == false && CheckMoveLeftPossibility() == false && CheckMoveRightPossibility() == false && CheckMoveUpPossibility() == false)
                {
                    break;
                }
            }

            LostMessage();
        }

        public void LostMessage()
        {
            Console.SetCursorPosition(0, yGrid + 6);
            Console.WriteLine("You have lost... press Enter to exit ;) see you next time..");
            Console.WriteLine("Thank you for playing my clone of the original 2048 game.");
            Console.WriteLine("If you want to contact me or report bug, e-mail me at: \twortelus@gmail.com");
            Console.WriteLine("Created by Daniel Slavík alias wortelus");
            Console.WriteLine("--------Press ENTER to play again--------");
            Console.ReadLine();
        }

        public void RemoveLostMessage()
        {
            Console.SetCursorPosition(0, yGrid + 6);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, yGrid + 7);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, yGrid + 8);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, yGrid + 9);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, yGrid + 10);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
        }
        public void RenderGrid()
        {
            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < yGrid; i++)
            {
                Console.SetCursorPosition(0, i + 2);
                for (int a = 0; a < xGrid; a++)
                {
                    int length = grid[a, i].ToString().Length;
                    string emptyBuffer = string.Empty;
                    for (int j = tileLength - length; j > 0; j--)
                    {
                        emptyBuffer += " ";
                    }

                    Console.BackgroundColor = GetTileColor(grid[a, i]);
                    Console.Write("{0,-" + tileLength * 2 + "}", emptyBuffer + grid[a, i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }

            Console.SetCursorPosition(0, yGrid + 3);
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Score: " + score + "\t | Moves: " + moves + "\t ");
            Console.WriteLine("-----------------------------------");
        }

        public void StupidAI()
        {
            while (true)
            {
                MoveLeft();
                MoveUp();
                MoveRight();
                MoveUp();
                if (CheckMoveDownPossibility() == false && CheckMoveLeftPossibility() == false && CheckMoveRightPossibility() == false && CheckMoveUpPossibility() == false)
                {
                    break;
                }
                else if (CheckMoveLeftPossibility() == false && CheckMoveRightPossibility() == false && CheckMoveUpPossibility() == false)
                {
                    MoveDown();
                    MoveUp();
                }
            }
        }

        public void MoveLeft()
        {
            if(CheckMoveLeftPossibility() == false)
            {
                return;
            }

            for (int i = 0; i < yGrid; i++) //cycle for modifying all rows
            {
                for (int a = 0; a < xGrid; a++)
                {
                    if (grid[a, i] != 0)
                    {
                        break;
                    }
                    else if (a == xGrid - 1)
                    {
                        goto Footer;
                    }
                }

                for (int a = xGrid - 1; a > 0; a--)
                {
                    if (grid[a - 1, i] == 0)
                    {
                        for (int b = a; b < xGrid; b++)
                        {
                            grid[b - 1, i] = grid[b, i];
                            grid[b, i] = 0;
                        }
                    }
                }

                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (grid[a, i] == grid[a + 1, i])
                    {
                        grid[a, i] = grid[a, i] * 2;
                        grid[a + 1, i] = 0;
                        score += grid[a, i];
                    }
                }

                for (int a = xGrid - 1; a > 0; a--)
                {
                    if (grid[a - 1, i] == 0)
                    {
                        for (int b = a; b < xGrid; b++)
                        {
                            grid[b - 1, i] = grid[b, i];
                            grid[b, i] = 0;
                        }
                    }
                }

                Footer:;
            }

            moves++;
            GenerateNewBlock();
            
            /*
            int xNewBlock = newBlockGrid.Item1;
            int yNewBlock = newBlockGrid.Item2;
            if(xNewBlock > 0)
            {
                while (grid[xNewBlock - 1, yNewBlock] == 0)
                {
                    grid[xNewBlock - 1, yNewBlock] = grid[xNewBlock, yNewBlock];
                    grid[xNewBlock, yNewBlock] = 0;
                    xNewBlock--;
                    if(xNewBlock <= 0)
                    {
                        break;
                    }
                }
            }
            */
        }

        public void MoveUp()
        {
            if(CheckMoveUpPossibility() == false)
            {
                return;
            }

            for (int a = 0; a < xGrid; a++) //cycle for modifying all rows
            {
                for (int i = 0; i < yGrid; i++)
                {
                    if (grid[a, i] != 0)
                    {
                        break;
                    }
                    else if (i == yGrid - 1)
                    {
                        goto Footer;
                    }
                }

                for (int i = yGrid - 1; i > 0; i--)
                {
                    if (grid[a, i - 1] == 0)
                    {
                        for (int j = i; j < yGrid; j++)
                        {
                            grid[a, j - 1] = grid[a, j];
                            grid[a, j] = 0;
                        }
                    }
                }
                for (int i = 0; i < yGrid - 1; i++)
                {
                    if (grid[a, i] == grid[a, i + 1])
                    {
                        grid[a, i] = grid[a, i] * 2;
                        grid[a, i + 1] = 0;
                        score += grid[a, i];
                    }
                }
                for (int i = yGrid - 1; i > 0; i--)
                {
                    if (grid[a, i - 1] == 0)
                    {
                        for (int j = i; j < yGrid; j++)
                        {
                            grid[a, j - 1] = grid[a, j];
                            grid[a, j] = 0;
                        }
                    }
                }

                Footer:;
            }

            moves++;
            GenerateNewBlock();
            
            /*
            int xNewBlock = newBlockGrid.Item1;
            int yNewBlock = newBlockGrid.Item2;
            if (yNewBlock > 0)
            {
                while (grid[xNewBlock, yNewBlock - 1] == 0)
                {
                    grid[xNewBlock, yNewBlock - 1] = grid[xNewBlock, yNewBlock];
                    grid[xNewBlock, yNewBlock] = 0;
                    yNewBlock--;
                    if (yNewBlock <= 0)
                    {
                        break;
                    }
                }
            }
            */
        }

        public void MoveDown()
        {
            if(CheckMoveDownPossibility() == false)
            {
                return;
            }

            for (int a = 0; a < xGrid; a++) //cycle for modifying all rows
            {
                for (int i = 0; i < yGrid; i++)
                {
                    if (grid[a, i] != 0)
                    {
                        break;
                    }
                    else if (i == yGrid - 1)
                    {
                        goto Footer;
                    }
                }

                for (int i = 0; i < yGrid; i++)
                {
                    if (grid[a, i] == 0)
                    {
                        for (int j = i; j > 0; j--)
                        {
                            grid[a, j] = grid[a, j - 1];
                            grid[a, j - 1] = 0;
                        }                       
                    }
                }
                for (int i = yGrid - 1; i > 0; i--)
                {
                    if (grid[a, i] == grid[a, i - 1])
                    {
                        grid[a, i] = grid[a, i] * 2;
                        grid[a, i - 1] = 0;
                        score += grid[a, i];
                    }
                }
                for (int i = 0; i < yGrid; i++)
                {
                    if (grid[a, i] == 0)
                    {
                        for (int j = i; j > 0; j--)
                        {
                            grid[a, j] = grid[a, j - 1];
                            grid[a, j - 1] = 0;
                        }
                    }
                }

                Footer:;
            }

            moves++;
            GenerateNewBlock();
            
            /*
            int xNewBlock = newBlockGrid.Item1;
            int yNewBlock = newBlockGrid.Item2;
            if (yNewBlock < yGrid - 1)
            {
                while (grid[xNewBlock, yNewBlock + 1] == 0)
                {
                    grid[xNewBlock, yNewBlock + 1] = grid[xNewBlock, yNewBlock];
                    grid[xNewBlock, yNewBlock] = 0;
                    yNewBlock++;
                    if (yNewBlock >= yGrid - 1)
                    {
                        break;
                    }
                }
            }
            */
        }

        public void MoveRight()
        {
            if(CheckMoveRightPossibility() == false)
            {
                return;
            }

            for (int i = 0; i < yGrid; i++) //cycle for modifying all rows
            {
                for (int a = 0; a < xGrid; a++)
                {
                    if (grid[a, i] != 0)
                    {
                        break;
                    }
                    else if (a == xGrid - 1)
                    {
                        goto Footer;
                    }
                }

                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (grid[a + 1, i] == 0)
                    {
                        for (int b = a; b >= 0; b--)
                        {
                            grid[b + 1, i] = grid[b, i];
                            grid[b, i] = 0;
                        }
                    }
                }

                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (grid[a, i] == grid[a + 1, i])
                    {
                        grid[a, i] = grid[a, i] * 2;
                        grid[a + 1, i] = 0;
                        score += grid[a, i];
                    }
                }

                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (grid[a + 1, i] == 0)
                    {
                        for (int b = a; b >= 0; b--)
                        {
                            grid[b + 1, i] = grid[b, i];
                            grid[b, i] = 0;
                        }
                    }
                }

                Footer:;
            }

            moves++;
            GenerateNewBlock();
        }

        public void InitiateStartBlocks()
        {
            Random r = new Random();
            int random1 = (int)Math.Pow(2, r.Next(1, 3));
            Tuple<int, int> gridPosition1 = new Tuple<int, int>(r.Next(xGrid), r.Next(yGrid));
            AddToGrid(gridPosition1.Item1, gridPosition1.Item2, random1);

            int random2 = (int)Math.Pow(2, r.Next(1, 3));
            Tuple<int, int> position2 = GetRandomFreeGridPosition();
            AddToGrid(position2.Item1, position2.Item2, random2);
        }

        public Tuple<int, int> GenerateNewBlock()
        {
            Random r = new Random();
            int random2 = (int)Math.Pow(2, r.Next(1, 3));
            Tuple<int, int> position2 = GetRandomFreeGridPosition();
            AddToGrid(position2.Item1, position2.Item2, random2);
            return position2;
        }

        public bool CheckMoveRightPossibility()
        {
            int[,] tempGrid = new int[xGrid, yGrid];
            for (int i = 0; i < yGrid; i++)
            {
                for (int a = 0; a < xGrid; a++)
                {
                    tempGrid[a, i] = grid[a, i];
                }
            }

            for (int i = 0; i < yGrid; i++) //cycle for modifying all rows
            {
                for (int a = 0; a < xGrid; a++)
                {
                    if (tempGrid[a, i] != 0)
                    {
                        break;
                    }
                    else if (a == xGrid - 1)
                    {
                        goto Footer;
                    }
                }

                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (tempGrid[a + 1, i] == 0)
                    {
                        for (int b = a; b >= 0; b--)
                        {
                            tempGrid[b + 1, i] = tempGrid[b, i];
                            tempGrid[b, i] = 0;
                        }
                    }
                }

                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (tempGrid[a, i] == tempGrid[a + 1, i])
                    {
                        tempGrid[a, i] = tempGrid[a, i] * 2;
                        tempGrid[a + 1, i] = 0;
                    }
                }

                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (tempGrid[a + 1, i] == 0)
                    {
                        for (int b = a; b >= 0; b--)
                        {
                            tempGrid[b + 1, i] = tempGrid[b, i];
                            tempGrid[b, i] = 0;
                        }
                    }
                }

                Footer:;
            }

            for (int i = 0; i < yGrid; i++)
            {
                for (int a = 0; a < xGrid; a++)
                {
                    if (grid[a, i] != tempGrid[a, i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckMoveLeftPossibility()
        {
            int[,] tempGrid = new int[xGrid, yGrid];
            for (int i = 0; i < yGrid; i++)
            {
                for (int a = 0; a < xGrid; a++)
                {
                    tempGrid[a, i] = grid[a, i];
                }
            }

            for (int i = 0; i < yGrid; i++) //cycle for modifying all rows
            {
                for (int a = 0; a < xGrid; a++)
                {
                    if (tempGrid[a, i] != 0)
                    {
                        break;
                    }
                    else if (a == xGrid - 1)
                    {
                        goto Footer;
                    }
                }

                for (int a = xGrid - 1; a > 0; a--)
                {
                    if (tempGrid[a - 1, i] == 0)
                    {
                        for (int b = a; b < xGrid; b++)
                        {
                            tempGrid[b - 1, i] = tempGrid[b, i];
                            tempGrid[b, i] = 0;
                        }
                    }
                }

                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (tempGrid[a, i] == tempGrid[a + 1, i])
                    {
                        tempGrid[a, i] = tempGrid[a, i] * 2;
                        tempGrid[a + 1, i] = 0;
                    }
                }

                for (int a = xGrid - 1; a > 0; a--)
                {
                    if (tempGrid[a - 1, i] == 0)
                    {
                        for (int b = a; b < xGrid; b++)
                        {
                            tempGrid[b - 1, i] = tempGrid[b, i];
                            tempGrid[b, i] = 0;
                        }
                    }
                }

                Footer:;
            }

            for (int i = 0; i < yGrid; i++)
            {
                for(int a = 0; a < xGrid; a++)
                {
                    if(grid[a, i] != tempGrid[a, i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckMoveUpPossibility()
        {
            int[,] tempGrid = new int[xGrid, yGrid];
            for (int i = 0; i < yGrid; i++)
            {
                for (int a = 0; a < xGrid; a++)
                {
                    tempGrid[a, i] = grid[a, i];
                }
            }

            for (int a = 0; a < xGrid; a++) //cycle for modifying all rows
            {
                for (int i = 0; i < yGrid; i++)
                {
                    if (tempGrid[a, i] != 0)
                    {
                        break;
                    }
                    else if (i == yGrid - 1)
                    {
                        goto Footer;
                    }
                }

                for (int i = yGrid - 1; i > 0; i--)
                {
                    if (tempGrid[a, i - 1] == 0)
                    {
                        for (int j = i; j < yGrid; j++)
                        {
                            tempGrid[a, j - 1] = tempGrid[a, j];
                            tempGrid[a, j] = 0;
                        }
                    }
                }
                for (int i = 0; i < yGrid - 1; i++)
                {
                    if (tempGrid[a, i] == tempGrid[a, i + 1])
                    {
                        tempGrid[a, i] = tempGrid[a, i] * 2;
                        tempGrid[a, i + 1] = 0;
                    }
                }
                for (int i = yGrid - 1; i > 0; i--)
                {
                    if (tempGrid[a, i - 1] == 0)
                    {
                        for (int j = i; j < yGrid; j++)
                        {
                            tempGrid[a, j - 1] = tempGrid[a, j];
                            tempGrid[a, j] = 0;
                        }
                    }
                }

                Footer:;
            }

            for (int i = 0; i < yGrid; i++)
            {
                for (int a = 0; a < xGrid; a++)
                {
                    if (grid[a, i] != tempGrid[a, i])
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        public bool CheckMoveDownPossibility()
        {
            int[,] tempGrid = new int[xGrid, yGrid];
            for (int i = 0; i < yGrid; i++)
            {
                for (int a = 0; a < xGrid; a++)
                {
                    tempGrid[a, i] = grid[a, i];
                }
            }

            for (int a = 0; a < xGrid; a++) //cycle for modifying all rows
            {
                for (int i = 0; i < yGrid; i++)
                {
                    if (tempGrid[a, i] != 0)
                    {
                        break;
                    }
                    else if (i == yGrid - 1)
                    {
                        goto Footer;
                    }
                }

                for (int i = 0; i < yGrid; i++)
                {
                    if (tempGrid[a, i] == 0)
                    {
                        for (int j = i; j > 0; j--)
                        {
                            tempGrid[a, j] = tempGrid[a, j - 1];
                            tempGrid[a, j - 1] = 0;
                        }
                    }
                }
                for (int i = yGrid - 1; i > 0; i--)
                {
                    if (tempGrid[a, i] == tempGrid[a, i - 1])
                    {
                        tempGrid[a, i] = tempGrid[a, i] * 2;
                        tempGrid[a, i - 1] = 0;
                    }
                }
                for (int i = 0; i < yGrid; i++)
                {
                    if (tempGrid[a, i] == 0)
                    {
                        for (int j = i; j > 0; j--)
                        {
                            tempGrid[a, j] = tempGrid[a, j - 1];
                            tempGrid[a, j - 1] = 0;
                        }
                    }
                }

                Footer:;
            }

            for (int i = 0; i < yGrid; i++)
            {
                for (int a = 0; a < xGrid; a++)
                {
                    if (grid[a, i] != tempGrid[a, i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void AddToGrid(int x, int y, int value)
        {
            grid[x, y] = value;
        }

        public ConsoleColor GetTileColor(int value)
        {
            switch(value)
            {
                case 0:
                    return ConsoleColor.Black;
                case 2:
                    return ConsoleColor.DarkGray;
                case 4:
                    return ConsoleColor.DarkYellow;
                case 8:
                    return ConsoleColor.DarkMagenta;
                case 16:
                    return ConsoleColor.DarkBlue;
                case 32:
                    return ConsoleColor.DarkCyan;
                case 64:
                    return ConsoleColor.DarkGreen;
                case 128:
                    return ConsoleColor.DarkRed;
                case 256:
                    return ConsoleColor.Magenta;
                case 512:
                    return ConsoleColor.Blue;
                case 1024:
                    return ConsoleColor.Cyan;
                case 2048:
                    return ConsoleColor.Green;
                case 4096:
                    return ConsoleColor.Yellow;
                case 8192:
                    return ConsoleColor.Red;
                case 16384:
                    return ConsoleColor.Gray;
                default:
                    return ConsoleColor.Black;
            }
        }

        public Tuple<int, int> GetRandomFreeGridPosition()
        {
            List<Tuple<int, int>> freeList = new List<Tuple<int, int>>();

            /*
             * i = y axis    
             * a = x axis
             */

            for(int i = 0; i < yGrid; i++)
            {
                for(int a = 0; a < xGrid; a++)
                {
                    if (grid[a, i] == 0)
                    {
                        freeList.Add(new Tuple<int, int>(a, i));
                    }
                }
            }

            int random = new Random().Next(0, freeList.Count);
            return freeList[random];
        }
    }
}
