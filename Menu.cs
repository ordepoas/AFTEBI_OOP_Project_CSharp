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
            
            Console.WriteLine("\t\tDeseja fazer 'restore' do Estado do porto?");
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
        
        public static int BackupMenu()
        {
            bool success;
            int option;
            
            Console.WriteLine("\t\tDeseja fazer 'backup' do Estado do porto?");
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
                    if (MenuShips(ships, containers) == 0)
                    {
                        return 0;
                    };
                    break;
                case 2 :
                    MenuContainers(ships, containers);
                    break;
            }

            return option;
        }

        public static int MenuShips(List<Ship> ships, List<Container> containers)
        {
            int option;
            
            while (true)
            {
                bool success;

                Console.Clear();
                Console.WriteLine("\t\t=================== GERIR NAVIOS ===================");
                Console.WriteLine("\t\t(1) Entrada de navio");
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
                    case 0:
                        return option;
                    case 1:
                        Methods.addShip(ships);
                        continue;
                    case 2:
                        if (Methods.ListShip(ships) != 0)
                        {
                            Methods.RemoveShip(ships);
                        }
                        continue;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 9:
                        MainMenu(ships, containers);
                        break;
                }

                break;
            }

            return option;
        }

        public static int MenuContainers(List<Ship> ships, List<Container> containers)
        {
            bool success;
            int option;
            
            Console.WriteLine("\t\t=================== GERIR CONTENTORES ===================");
            Console.WriteLine("\t\t(1) Entrada de contentor");
            Console.WriteLine("\t\t(2) Saída de contentores");
            Console.WriteLine("\t\t(3) Atribuir um contentor a um navio");
            Console.WriteLine("\t\t(4) Retirar um contentor de um navio");
            Console.WriteLine("\t\t(5) Listar todos os contentores não atribuídos");
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
                    return option;
                case 1 :
                    Methods.addContainer(containers);
                    MenuContainers(ships, containers);
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

            return option;
        }
    }
}