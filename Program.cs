//using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

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
            //------------------------------------------------------------------------------------------------

            
            
            //------------------------------------------------------------------------------------------------

            Seaport s = new Seaport();

            if (Menu.RestoreMenu() == 1 && (s = Methods.Restore()) != null)
            {
                s.Ships = s.GetShips();
                s.Containers = s.GetContainers();

            }

            while (Menu.MainMenu(s) != 0);
            
            if (Menu.BackupMenu() == 1 && (s.Ships.Count > 0 || s.Containers.Count > 0))
            {
                Methods.Backup(s);
            }
        }
    }
}