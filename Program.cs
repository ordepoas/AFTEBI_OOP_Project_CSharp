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
            //---- apresentação do logo de entrada de um ficheiro de texto.
            Console.Clear();
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
            if (Menu.RestoreMenu() == 1 && (s = Methods.Restore()) != null)
            {
                s.Ships = s.GetShips();
                s.Containers = s.GetContainers();

            }
            //---- mantem o menu principal ativo enquanto não for escolhida a opção 0
            while (Menu.MainMenu(s) != 0);
            
            //---- executa o backup do estado do porto se forem cumpridas as condições
            if (Menu.BackupMenu() == 1 && (s.Ships.Count > 0 || s.Containers.Count > 0))
            {
                Methods.Backup(s);
            }
        }
    }
}