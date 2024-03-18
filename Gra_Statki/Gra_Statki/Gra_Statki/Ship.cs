using System;

class Ship
{
    private int size;
    private int hits;
    private bool[,] segments;

    public Ship(int size)
    {
        this.size = size;
        hits = 0;
        segments = new bool[10, 10];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                segments[i, j] = false;
        }
    }

    public int Size { get { return size; } }

    public bool TryPlaceShip(int x, int y, bool horizontal, Board board)
    {
        if (!board.CheckShipPlacement(x, y, size, horizontal))
            return false;

        if (horizontal)
        {
            for (int i = y; i < y + size; i++)
            {
                board.PlaceShip(x, i);
                segments[x, i] = true;
            }
        }
        else
        {
            for (int i = x; i < x + size; i++)
            {
                board.PlaceShip(i, y);
                segments[i, y] = true;
            }
        }

        return true;
    }

    public bool Hit(int x, int y)
    {
        if (!segments[x, y])
            return false;

        hits++;
        segments[x, y] = true;
        return hits == size;
    }

    public bool IsSunk()
    {
        return hits == size;
    }
}

