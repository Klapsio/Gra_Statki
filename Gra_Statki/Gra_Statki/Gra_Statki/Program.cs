using System;

class Program
{
    static void Main(string[] args)
    {
        Player player1 = new Player();
        Player player2 = new Player();

        Console.WriteLine("Gracz 1, ustaw swoje statki:");
        player1.SetupBoard();
        Console.WriteLine("\nGracz 2, ustaw swoje statki:");
        player2.SetupBoard();

        while (!player1.AllShipsSunk() && !player2.AllShipsSunk())
        {
            Console.Clear();
            Console.WriteLine("Kolej gracza 1:");
            player1.Attack(player2);
            Console.WriteLine("\nNaciśnij Enter, aby kontynuować...");
            Console.ReadLine();

            if (player2.AllShipsSunk())
                break;

            Console.Clear();
            Console.WriteLine("Kolej gracza 2:");
            player2.Attack(player1);
            Console.WriteLine("\nNaciśnij Enter, aby kontynuować...");
            Console.ReadLine();
        }

        Console.Clear();
        if (player1.AllShipsSunk())
        {
            Console.WriteLine("Gracz 2 wygrywa!");
            player2.winCounter++;
        }
        else
        {
            Console.WriteLine("Gracz 1 wygrywa!");
            player1.winCounter++;
        }
    }
}
