using System;
using System.Collections.Generic;
using System.IO;

namespace OOP_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            string[] logo = File.ReadAllLines("logo.txt");
            foreach (string line in logo)
            {
                Console.WriteLine(line);
            }
            Console.Write("\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
            
            List<Ship> ships = new List<Ship>();
            List<Container> containers = new List<Container>();
            State seaportState = new State(ships, containers);

            if (Menu.RestoreMenu() == 1 && (seaportState = Methods.Restore()) != null)
            {
                ships = seaportState.GetShips();
                containers = seaportState.GetContainers();

            }

            while (Menu.MainMenu(ships, containers) != 0);
            
            if (Menu.BackupMenu() == 1 && (ships.Count > 0 || containers.Count > 0))
            {
                Methods.Backup(seaportState);
            }
        }
    }
}