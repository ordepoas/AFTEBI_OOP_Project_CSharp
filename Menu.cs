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
                    break;
                //Acesso ao Menu GERIR NAVIOS
                case 1 :
                    do
                    {
                        option = MenuShips(ships, containers);
                        success = (option == 0) || (option == 9);

                    } while (!success);

                    break;
                //Acesso ao Menu GERIR CONTENTORES
                case 2 :
                    do
                    {
                        option = MenuContainers(ships, containers);
                        success = (option == 0) || (option == 9);

                    } while (!success);

                    break;
            }

            return option;
        }

        public static int MenuShips(List<Ship> ships, List<Container> containers)
        {
            int option;
            
            bool success;

            Console.Clear();
            Console.WriteLine("\t\t=================== GERIR NAVIOS ===================");
            Console.WriteLine("\t\t(1) Entrada de navio");
            Console.WriteLine("\t\t(2) Saída de navio");
            Console.WriteLine("\t\t(3) Chamar navio ao porto");
            Console.WriteLine("\t\t(4) Número Navios no porto");
            Console.WriteLine("\t\t(5) Número Navios ao Largo");
            Console.WriteLine("\t\t(6) Listar Números de contentores de um navio");
            Console.WriteLine("\t\t(7) Listar todos os contentores de um navio");
            Console.WriteLine("\n\t\t(9) Menu Anterior");
            Console.WriteLine("\t\t(0) Sair");

            do
            {
                Console.Write("\n\t\tIndique a sua opção: ");
                success = int.TryParse(Console.ReadLine(), out option);
                Console.WriteLine();
                if (success && (option < 0 || option > 7))
                {
                    if (option != 9)
                    {
                        Console.WriteLine("\t\tOpção inválida!");
                        success = false;
                    }
                }
            } while (!success);
            
            Console.WriteLine();

            switch (option)
            {
                //Saí do programa acedendo ao Menu de Backup ou retorna ao menu anterior
                case 0:
                case 9:
                    break;
                //Adicionar Navio
                case 1:
                    Methods.AddShip(ships);
                    break;
                //Listar e remover navio
                case 2:
                    Methods.RemoveShip(ships);
                    break;
                //Chamar navio ao porto
                case 3:
                    Methods.CallShipToSeaport(ships);
                    break;
                //Navios no porto
                case 4:
                    Methods.CountShipsAtSeaport(ships);
                    break;
                //Navios ao largo
                case 5:
                    Methods.CountShipsAtLarge(ships);
                    break;
                //Lista navios e indica o numero de contentores no navio indicado
                case 6:
                    Methods.ListContainerNumberAtShip(ships);
                    break;
                //Lista navios e lista os contentores no navio indicado
                case 7:
                    Methods.ListContainersAtShip(ships);
                    break;
            }

            return option;
        }

        public static int MenuContainers(List<Ship> ships, List<Container> containers)
        {
            bool success;
            int option, counter = 0;

            Console.Clear();
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
            
            Console.WriteLine();
            
            switch (option)
            {
                //Sai do programa acedendo ao menu de backup ou fica no menu anterior
                case 0 :
                case 9 :
                    break;
                //Adicionar contentor
                case 1:
                    Methods.AddContainer(containers);
                    break;                
                //Remover contentor
                case 2:
                        Methods.RemoveContainer(containers);
                    break;
                
                //Atribuir contentor a um navio;
                case 3:
                        Methods.AddContainerToShip(ships, containers);
                    break;
                
                //Retirar contentor de um navio;
                case 4:
                    Methods.RemoveContainerFromShip(ships, containers);
                    break;

                //Listar contentores não atribuídos
                case 5:
                    Methods.UnassignedContainers(containers);
                    break;
            }
            return option;
        }
    }
}