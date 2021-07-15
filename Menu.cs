using System;
using System.Collections.Generic;

namespace OOP_Project
{
    public class Menu
    {
        public static int RestoreMenu()
        {
            bool success;
            int option;
            
            Console.WriteLine("\t\tDeseja fazer o 'restore' do Estado do porto?");
            Console.WriteLine("\t\t(1) - Sim | (2) - Não");

            do
            {
                Console.Write("\n\t\tIndique a sua opção: ");
                success = int.TryParse(Console.ReadLine(), out option);
                if (success && (option != 1 && option != 2))
                {
                    Console.WriteLine("\t\tOpção inválida!");
                    success = false;
                }
                
            } while (!success);

            return option;
        }
        
        public static int MainMenu(List<Ship> ships, List<Container> containers)
        {
            bool success;
            int option;
            
            Console.Clear();
            Console.WriteLine("\t\t====== MENU PRINCIPAL ======");
            Console.WriteLine("\t\t(1) Gerir navios");
            Console.WriteLine("\t\t(2) Gerir Contentores");
            Console.WriteLine("\n\t\t(0) Sair");
            
            do
            {
                Console.Write("\n\t\tIndique a sua opção: ");
                success = int.TryParse(Console.ReadLine(), out option);
                if (success && (option < 0 || option > 2))
                {
                    Console.WriteLine("\t\tOpção inválida!");
                    success = false;
                }
                
            } while (!success);

            switch (option)
            {
                case 0 :
                    return option;
                case 1 :
                    MenuShips(ships, containers);
                    break;
                case 2 :
                    MenuContainers(ships, containers);
                    break;
            }

            return option;
        }

        public static void MenuShips(List<Ship> ships, List<Container> containers)
        {
            bool success;
            int option;
            
            Console.Clear();
            Console.WriteLine("\t\t=================== GERIR NAVIOS ===================");
            Console.WriteLine("\t\t(1) Entrada de novo navio");
            Console.WriteLine("\t\t(2) Saída de navio");
            Console.WriteLine("\t\t(3) Verificar quantidade de navios no porto");
            Console.WriteLine("\t\t(4) Verificar quantidade de contentores de um navio");
            Console.WriteLine("\t\t(5) Listar todos os contentores de um navio");
            Console.WriteLine("\n\t\t(9) Menu Anterior");
            Console.WriteLine("\t\t(0) Sair");
            
            do
            {
                Console.Write("\n\t\tIndique a sua opção: ");
                success = int.TryParse(Console.ReadLine(), out option);
                if (success && (option < 0 || option > 5))
                {
                    if (option != 9)
                    {
                        Console.WriteLine("\t\tOpção inválida!");
                        success = false;
                    }
                }
                
            } while (!success);

            switch (option)
            {
                case 0 :
                    return;
                case 1 :
                    Methods.addShip(ships);
                    MenuShips(ships,containers);
                    break;
                case 2 :
                    break;
                case 3 :
                    break;
                case 4 :
                    break;
                case 5 :
                    break;
                case 9 :
                    MainMenu(ships, containers);
                    break;
            }
        }

        public static void MenuContainers(List<Ship> ships, List<Container> containers)
        {
            bool success;
            int option;
            
            Console.WriteLine("\t\t=================== GERIR CONTENTORES ===================");
            Console.WriteLine("\t\t(1) Entrada de novo contentor");
            Console.WriteLine("\t\t(2) Saída de contentores");
            Console.WriteLine("\t\t(3) Atribuir um contentor a um navio");
            Console.WriteLine("\t\t(4) Retirar um contentor de um navio");
            Console.WriteLine("\t\t(5) Listar todos os contentores não atríbuidos");
            Console.WriteLine("\n\t\t(9) Menu Anterior");
            Console.WriteLine("\t\t(0) Sair");
            
            do
            {
                Console.Write("\n\t\tIndique a sua opção: ");
                success = int.TryParse(Console.ReadLine(), out option);
                if (success && (option < 0 || option > 5))
                {
                    if (option != 9)
                    {
                        Console.WriteLine("\t\tOpção inválida!");
                        success = false;
                    }
                }
                
            } while (!success);

            switch (option)
            {
                case 0 :
                    return;
                case 1 :
                    break;
                case 2 :
                    break;
                case 3 :
                    break;
                case 4 :
                    break;
                case 5 :
                    break;
                case 9 :
                    MainMenu(ships, containers);
                    break;
            }
        }
    }
}