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
            //---- apresentação do logo de entrada de um ficheiro de texto.
            Console.Clear();
            Console.WriteLine();
            string[] logo = File.ReadAllLines("logo.txt");
            foreach (string line in logo)
            {
                Console.WriteLine(line);
            }
            //---- Pausa no programa
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
            Console.ResetColor();
            
            //---- cria objeto seaport
            Seaport s = new Seaport();
            
            //---- restaura os dados de um ficheiro se forem cumpridas as condições
            if (MainMenu.RestoreMenu() == 1 && (s = MainMenu.Restore()) != null)
            {
                s.Ships = s.GetShips();
                s.Containers = s.GetContainers();

            }
            //---- mantem o menu principal ativo enquanto não for escolhida a opção 0
            while (MainMenu.Menu(s) != 0);
            
            //---- executa o backup do estado do porto se forem cumpridas as condições
            if (MainMenu.BackupMenu() == 1 && (s.Ships.Count > 0 || s.Containers.Count > 0))
            {
                MainMenu.Backup(s);
            }

        }
    }
}