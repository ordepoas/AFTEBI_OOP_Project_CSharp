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
                //Sair do programa / Acede ao Menu de backup
                case 0 :
                    return option;
                //Acesso ao Menu GERIR NAVIOS
                case 1 :
                    if (MenuShips(ships, containers) == 0)
                    {
                        return 0;
                    }
                    break;
                //Acesso ao Menu GERIR CONTENTORES
                case 2 :
                    if (MenuContainers(ships, containers) == 0)
                    {
                        return 0;
                    }
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
                Console.WriteLine("\t\t(3) Quantidade de navios no porto");
                Console.WriteLine("\t\t(4) Quantidade de contentores de um navio");
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
                    //Saí do programa acedendo ao Menu de Backup
                    case 0:
                        return option;
                    //Adicionar Navio
                    case 1:
                        Methods.AddShip(ships);
                        break;
                    //Listar e remover navio
                    case 2:
                        if (Methods.ListShip(ships) != 0)
                        {
                            Methods.RemoveShip(ships);
                        }
                        break;
                    //Navios no porto
                    case 3:
                        Methods.CheckShipsAtPort(ships);
                        break;
                    //Lista navios e indica o numero de contentores no navio indicado
                    case 4:
                        if (Methods.ListShip(ships) != 0)
                        {
                            Methods.CountContainersAtShip(ships);
                        }
                        break;
                    //Lista navios e lista os contentores no navio indicado
                    case 5:
                        if (Methods.ListShip(ships) != 0)
                        {
                            Methods.ListContainersAtShip(ships);
                        }
                        break;
                    case 9:
                        MainMenu(ships, containers);
                        break;
                }
            }
        }

        public static int MenuContainers(List<Ship> ships, List<Container> containers)
        {
            bool success;
            int option;
            
            while (true)
            {
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
                    case 0:
                        return option;
                    
                    //Adicionar contentor
                    case 1:
                        Methods.AddContainer(containers);
                        MenuContainers(ships, containers);
                        continue;
                    
                    //Remover contentor
                    case 2:
                        if (Methods.ListContainers(containers) != 0)
                        {
                            Methods.RemoveContainer(containers);
                        }
                        continue;
                    
                    //Atribuir contentor a um navio;
                    case 3:
                        if (Methods.ListContainers(containers) != 0)
                        {
                            Methods.AddContainerToShip(ships, containers);
                        }
                        continue;
                    
                    //Retirar contentor de um navio;
                    case 4:
                        if (Methods.ListShip(ships) != 0)
                        {
                            Methods.RemoveContainerFromShip(ships, containers);
                        }
                        continue;

                    //Listar contentores não atribuídos
                    case 5:
                        foreach (var c in containers)
                        {
                            if (c.GetShipNumber() == -1)
                            {
                                Console.WriteLine(c);
                            }
                        }
                        break;
                    
                    case 9:
                        MainMenu(ships, containers);
                        break;
                }
                
                return option;
            }
        }
    }
}