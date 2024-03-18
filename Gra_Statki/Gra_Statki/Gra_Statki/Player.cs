using System;

class Player
{
    public Board Board { get; }

    public int winCounter;

    public Player()
    {
        Board = new Board();
    }

    public void SetupBoard()
    {
        Board.ManualShipPlacement();
    }

    public void Attack(Player opponent)
    {
        opponent.Board.Display(false);
        Console.WriteLine("Gracz atakuje. Podaj współrzędne:");
        bool validInput = false;
        int[] coords = null;
        while (!validInput)
        {
            string input = Console.ReadLine().ToUpper();
            if (!(input.Length != 2 || input[0] < 'A' || input[0] > 'J' || input[1] < '1' || input[1] > '0'))
            {
                Console.WriteLine("Podano nieprawidłowe współrzędne. Spróbuj ponownie.");
            }
            else
            {
                coords = Board.ConvertInputToCoordinates(input);
                if (!opponent.Board.IsValidCoordinate(coords[0], coords[1]))
                {
                    Console.WriteLine("Podane współrzędne są poza planszą. Spróbuj ponownie.");
                }
                else
                {
                    validInput = true;
                }
            }
        }

        bool result = opponent.Board.ReceiveAttack(coords);
        if (result)
            Console.WriteLine("Trafiony!");
        else
            Console.WriteLine("Pudło!");

        Console.WriteLine("\nPlansza przeciwnika:");
        opponent.Board.Display(false);
    }

    public bool AllShipsSunk()
    {
        return Board.AllShipsSunk();
    }
}
