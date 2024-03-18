using System;

class Board
{
    private const int BoardSize = 10;
    private char[,] grid;
    private Ship[] ships;

    public Board()
    {
        grid = new char[BoardSize, BoardSize];
        ships = new Ship[]
        {
            new Ship(1), new Ship(1), new Ship(1), new Ship(1),
            new Ship(2), new Ship(2), new Ship(2),
            new Ship(3), new Ship(3),
            new Ship(4)
        };
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                grid[i, j] = '-';
            }
        }
    }

    public void ManualShipPlacement()
    {
        foreach (Ship ship in ships)
        {
            Console.WriteLine($"Ustawianie statku o rozmiarze {ship.Size}");

            bool placed = false;
            while (!placed)
            {
                Display(true);
                Console.WriteLine("Podaj współrzędne początkowe:");
                string startInput = Console.ReadLine().ToUpper();
                int[] startCoords = ConvertInputToCoordinates(startInput);

                Console.WriteLine("Podaj kierunek (H dla poziomo, V dla pionowo):");
                char directionInput = Console.ReadKey().KeyChar;
                bool horizontal = directionInput == 'H' || directionInput == 'h';
                Console.Clear();

                if (CheckShipPlacement(startCoords[0], startCoords[1], ship.Size, horizontal))
                {
                    if (ship.TryPlaceShip(startCoords[0], startCoords[1], horizontal, this))
                    {
                        placed = true;
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowe umieszczenie. Spróbuj ponownie.");
                    }
                }
                else
                {
                    Console.WriteLine("Statki nie mogą się stykać ani wychodzić poza planszę. Spróbuj ponownie.");
                }
            }
        }
    }

    public bool ReceiveAttack(int[] coords)
    {
        int x = coords[0];
        int y = coords[1];

        foreach (Ship ship in ships)
        {
            if (ship.Hit(x, y))
            {
                grid[x, y] = 'X';
                return true;
            }
        }

        grid[x, y] = 'O';
        return false;
    }

    public void Display(bool forPlayer)
    {
        Console.WriteLine("\n");
        Console.WriteLine("   A B C D E F G H I J");
        for (int i = 0; i < BoardSize; i++)
        {

            if (i != 9) Console.Write(" " + (i + 1) + " ");
            else Console.Write("10 ");
            for (int j = 0; j < BoardSize; j++)
            {
                if (forPlayer) Console.Write(grid[i, j] + " ");
                else
                {
                    if (grid[i, j] == '#')
                    {
                        Console.Write("- ");
                    }
                    else Console.Write(grid[i, j] + " ");
                }

            }
            Console.WriteLine();
        }
    }

    public bool AllShipsSunk()
    {
        foreach (Ship ship in ships)
        {
            if (!ship.IsSunk())
                return false;
        }
        return true;
    }

    public bool IsValidCoordinate(int x, int y)
    {
        return x >= 0 && x < BoardSize && y >= 0 && y < BoardSize;
    }

    public bool IsVacant(int x, int y)
    {
        return grid[x, y] == '-';
    }

    public void PlaceShip(int x, int y)
    {
        grid[x, y] = '#';
    }

    public void RemoveShip(int x, int y)
    {
        grid[x, y] = '-';
    }

    public bool CheckShipPlacement(int x, int y, int size, bool horizontal)
    {
        if (horizontal)
        {
            for (int i = y; i < y + size; i++)
            {
                if (!IsValidCoordinate(x, i) || !IsVacant(x, i))
                    return false;
            }

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j < y + size + 1; j++)
                {
                    if (IsValidCoordinate(i, j) && !IsVacant(i, j))
                        return false;
                }
            }
        }
        else
        {
            for (int i = x; i < x + size; i++)
            {
                if (!IsValidCoordinate(i, y) || !IsVacant(i, y))
                    return false;
            }

            for (int i = x - 1; i < x + size + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (IsValidCoordinate(i, j) && !IsVacant(i, j))
                        return false;
                }
            }
        }
        return true;
    }

    public int[] ConvertInputToCoordinates(string input)
    {

        int x = input[0] - 'A';
        int y = int.Parse(input.Substring(1)) - 1;
        return new int[] { y, x };
    }
}
