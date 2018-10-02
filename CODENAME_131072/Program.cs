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
            GameClass game = new GameClass();
            Console.ReadLine();
        }
    }

    class GameClass
    {
        const int xGrid = 4;
        const int yGrid = 4;
        int[,] grid = new int[xGrid, yGrid];

        public GameClass()
        {
            Console.WriteLine("CODENAME_131072 - made by wortelus");
            InitiateStartBlocks();
            RenderGrid();
            
            while(true)
            {
                ConsoleKey ck = Console.ReadKey(true).Key;
                if(CheckMoveDownPossibility() == false && CheckMoveLeftPossibility() == false && CheckMoveRightPossibility() == false && CheckMoveUpPossibility() == false)
                {
                    break;
                }
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
                RenderGrid();
            }

            Console.SetCursorPosition(0, yGrid + 1);
            Console.WriteLine("You have lost... press Enter to exit ;) see you next time..");
        }

        public void RenderGrid()
        {
            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < yGrid; i++)
            {
                Console.SetCursorPosition(0, i + 1);
                for (int a = 0; a < xGrid; a++)
                {
                    Console.BackgroundColor = GetTileColor(grid[a, i]);
                    Console.Write(grid[a, i] + "    ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("\t\t\t\t");
                }
            }

        }

        public void MoveLeft()
        {
            if(CheckMoveLeftPossibility() == false)
            {
                return;
            }

            for(int i = 0; i < yGrid; i++) //cycle for modifying all rows
            {
                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (grid[a, i] == 0)
                    {
                        for (int b = a; b < xGrid; b++)
                        {
                            grid[b, i] = grid[b + 1, i];
                        }
                    }
                }
                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (grid[a, i] == 0)
                    {
                        grid[a, i] = grid[a + 1, i];
                        grid[a + 1, i] = 0;
                    }
                    if (grid[a, i] == grid[a + 1, i])
                    {
                        grid[a, i] = grid[a, i] * 2;
                        grid[a + 1, i] = 0;
                    }
                }
                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (grid[a, i] == 0)
                    {
                        for (int b = a; b < xGrid; b++)
                        {
                            grid[b, i] = grid[b + 1, i];
                        }
                    }
                }
            }

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
                for (int i = 0; i < yGrid - 1; i++)
                {
                    if (grid[a, i] == 0 && grid[a, i + 1] != 0)
                    {
                        grid[a, i] = grid[a, i + 1];
                        grid[a, i + 1] = 0;
                        i--;
                    }
                }
                for (int i = 0; i < yGrid - 1; i++)
                {
                    if (grid[a, i] == 0)
                    {
                        grid[a, i] = grid[a, i + 1];
                        grid[a, i + 1] = 0;
                    }
                    if (grid[a, i] == grid[a, i + 1])
                    {
                        grid[a, i] = grid[a, i] * 2;
                        grid[a, i + 1] = 0;
                    }
                }
                for (int i = 0; i < yGrid - 1; i++)
                {
                    if (grid[a, i] == 0 && grid[a, i + 1] != 0)
                    {
                        grid[a, i] = grid[a, i + 1];
                        grid[a, i + 1] = 0;
                        i--;
                    }
                }
            }

            
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
                for (int i = yGrid - 1; i > 0; i--)
                { 
                    if (grid[a, i] == 0 && grid[a, i - 1] != 0)
                    {
                        grid[a, i] = grid[a, i - 1];
                        grid[a, i - 1] = 0;
                        i++;
                    }
                }
                for (int i = yGrid - 1; i > 0; i--)
                {
                    if (grid[a, i] == 0)
                    {
                        grid[a, i] = grid[a, i - 1];
                        grid[a, i - 1] = 0;
                    }
                    if (grid[a, i] == grid[a, i - 1])
                    {
                        grid[a, i] = grid[a, i] * 2;
                        grid[a, i - 1] = 0;
                    }
                }
                for (int i = yGrid - 1; i > 0; i--)
                {
                    if (grid[a, i] == 0 && grid[a, i - 1] != 0)
                    {
                        grid[a, i] = grid[a, i - 1];
                        grid[a, i - 1] = 0;
                        i++;
                    }
                }
            }


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
                for (int a = xGrid - 1; a > 0; a--)
                {
                    if (grid[a, i] == 0 && grid[a - 1, i] != 0)
                    {
                        grid[a, i] = grid[a - 1, i];
                        grid[a - 1, i] = 0;
                        a++;
                    }
                }
                for (int a = xGrid - 1; a > 0; a--)
                {
                    if (grid[a, i] == 0)
                    {
                        grid[a, i] = grid[a - 1, i];
                        grid[a - 1, i] = 0;
                    }
                    if (grid[a, i] == grid[a - 1, i])
                    {
                        grid[a, i] = grid[a, i] * 2;
                        grid[a - 1, i] = 0;
                    }
                }

                for (int a = xGrid - 1; a > 0; a--)
                {
                    if (grid[a, i] == 0 && grid[a - 1, i] != 0)
                    {
                        grid[a, i] = grid[a - 1, i];
                        grid[a - 1, i] = 0;
                        a++;
                    }
                }
            }
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
                for (int a = xGrid - 1; a > 0; a--)
                {
                    if (tempGrid[a, i] == 0 && tempGrid[a - 1, i] != 0)
                    {
                        tempGrid[a, i] = tempGrid[a - 1, i];
                        tempGrid[a - 1, i] = 0;
                        a++;
                    }
                }
                for (int a = xGrid - 1; a > 0; a--)
                {
                    if (tempGrid[a, i] == 0)
                    {
                        tempGrid[a, i] = tempGrid[a - 1, i];
                        tempGrid[a - 1, i] = 0;
                    }
                    if (tempGrid[a, i] == tempGrid[a - 1, i])
                    {
                        tempGrid[a, i] = tempGrid[a, i] * 2;
                        tempGrid[a - 1, i] = 0;
                    }
                }

                for (int a = xGrid - 1; a > 0; a--)
                {
                    if (tempGrid[a, i] == 0 && tempGrid[a - 1, i] != 0)
                    {
                        tempGrid[a, i] = tempGrid[a - 1, i];
                        tempGrid[a - 1, i] = 0;
                        a++;
                    }
                }
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
                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (tempGrid[a, i] == 0 && tempGrid[a + 1, i] != 0)
                    {
                        tempGrid[a, i] = tempGrid[a + 1, i];
                        tempGrid[a + 1, i] = 0;
                        a--;
                    }
                }
                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (tempGrid[a, i] == 0)
                    {
                        tempGrid[a, i] = tempGrid[a + 1, i];
                        tempGrid[a + 1, i] = 0;
                    }
                    if (tempGrid[a, i] == tempGrid[a + 1, i])
                    {
                        tempGrid[a, i] = tempGrid[a, i] * 2;
                        tempGrid[a + 1, i] = 0;
                    }
                }

                for (int a = 0; a < xGrid - 1; a++)
                {
                    if (tempGrid[a, i] == 0 && tempGrid[a + 1, i] != 0)
                    {
                        tempGrid[a, i] = tempGrid[a + 1, i];
                        tempGrid[a + 1, i] = 0;
                        a--;
                    }
                }
            }

            for(int i = 0; i < yGrid; i++)
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
                for (int i = 0; i < yGrid - 1; i++)
                {
                    if (tempGrid[a, i] == 0 && tempGrid[a, i + 1] != 0)
                    {
                        tempGrid[a, i] = tempGrid[a, i + 1];
                        tempGrid[a, i + 1] = 0;
                        i--;
                    }
                }
                for (int i = 0; i < yGrid - 1; i++)
                {
                    if (tempGrid[a, i] == 0)
                    {
                        tempGrid[a, i] = tempGrid[a, i + 1];
                        tempGrid[a, i + 1] = 0;
                    }
                    if (tempGrid[a, i] == tempGrid[a, i + 1])
                    {
                        tempGrid[a, i] = tempGrid[a, i] * 2;
                        tempGrid[a, i + 1] = 0;
                    }
                }
                for (int i = 0; i < yGrid - 1; i++)
                {
                    if (tempGrid[a, i] == 0 && tempGrid[a, i + 1] != 0)
                    {
                        tempGrid[a, i] = tempGrid[a, i + 1];
                        tempGrid[a, i + 1] = 0;
                        i--;
                    }
                }
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
                for (int i = yGrid - 1; i > 0; i--)
                {
                    if (tempGrid[a, i] == 0 && tempGrid[a, i - 1] != 0)
                    {
                        tempGrid[a, i] = tempGrid[a, i - 1];
                        tempGrid[a, i - 1] = 0;
                        i++;
                    }
                }
                for (int i = yGrid - 1; i > 0; i--)
                {
                    if (tempGrid[a, i] == 0)
                    {
                        tempGrid[a, i] = tempGrid[a, i - 1];
                        tempGrid[a, i - 1] = 0;
                    }
                    if (tempGrid[a, i] == tempGrid[a, i - 1])
                    {
                        tempGrid[a, i] = tempGrid[a, i] * 2;
                        tempGrid[a, i - 1] = 0;
                    }
                }
                for (int i = yGrid - 1; i > 0; i--)
                {
                    if (tempGrid[a, i] == 0 && tempGrid[a, i - 1] != 0)
                    {
                        tempGrid[a, i] = tempGrid[a, i - 1];
                        tempGrid[a, i - 1] = 0;
                        i++;
                    }
                }
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
                    return ConsoleColor.Yellow;
                case 512:
                    return ConsoleColor.Magenta;
                case 1024:
                    return ConsoleColor.Blue;
                case 2048:
                    return ConsoleColor.Cyan;
                case 4096:
                    return ConsoleColor.Green;
                case 8192:
                    return ConsoleColor.Yellow;
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
