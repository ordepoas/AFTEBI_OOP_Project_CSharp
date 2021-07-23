using System;
using System.Collections.Generic;

namespace OOP_Project
{
    public class Menu
    {   //---- Método para apresentação do menu de restauro do estado do porto devolve um int com a opção escolhida
        public static int RestoreMenu()
        {
            bool success;
            int option;
            
            Console.WriteLine("\t\tDeseja fazer 'restore' do Estado do porto?");
            Console.WriteLine("\t\t(1) - Sim | (2) - Não");
            //---- recolha e validação de opção
            do
            {
                Console.Write("\n\t\tIndique a sua opção: ");
                success = int.TryParse(Console.ReadLine(), out option);
                
                if(!success) 
                    Console.WriteLine("\t\tOpção inválida!");
                if (success && (option != 1 && option != 2))
                {
                    Console.WriteLine("\t\tOpção inválida!");
                    success = false;
                }
                
            } while (!success);

            return option;
        }
        //---- Método para apresentação do menu de backup do estado do porto devolve um int com a opção escolhida
        public static int BackupMenu()
        {
            bool success;
            int option;
            Console.Clear();
            Console.WriteLine("\t\tDeseja fazer 'backup' do Estado do porto?");
            Console.WriteLine("\t\t(1) - Sim | (2) - Não");
            //---- recolha e validação da opção escolhida
            do
            {
                Console.Write("\n\t\tIndique a sua opção: ");
                success = int.TryParse(Console.ReadLine(), out option);
                if(!success) 
                    Console.WriteLine("\t\tOpção inválida!");
                if (success && (option != 1 && option != 2))
                {
                    Console.WriteLine("\t\tOpção inválida!");
                    success = false;
                }
                
            } while (!success);

            return option;
        }
        //---- Método - Menu Principal, devolve um int com a opção escolhida e recebe um objeto Seaport
        public static int MainMenu(Seaport s)
        {
            bool success;
            int option;
            
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\t\t====== MENU PRINCIPAL ======\t\t");
            Console.ResetColor();
            Console.WriteLine("\t\t(1) Gerir navios");
            Console.WriteLine("\t\t(2) Gerir Contentores");
            Console.WriteLine("\n\t\t(9) Tempo e notícias");
            Console.WriteLine("\t\t(0) Sair");
            //---- Recolha e validação da opção escolhida
            do
            {
                Console.Write("\n\t\tIndique a sua opção: ");
                success = int.TryParse(Console.ReadLine(), out option);
                if(!success) 
                    Console.WriteLine("\t\tOpção inválida!");
                if (success && (option < 0 || option > 2))
                {
                    if (option != 9)
                    {
                        Console.WriteLine("\t\tOpção inválida!");
                        success = false;
                    }
                }
                
            } while (!success);
            //---- chamada de instruções de acordo com as escolhas do menu
            switch (option)
            {
                //Sair do programa / Acede ao Menu de backup
                case 0 :
                    break;
                //Acesso ao Menu GERIR NAVIOS
                case 1 :
                    do
                    {
                        option = MenuShips(s);
                        success = (option == 0) || (option == 9);

                    } while (!success);

                    break;
                //Acesso ao Menu GERIR CONTENTORES
                case 2 :
                    do
                    {
                        option = MenuContainers(s);
                        success = (option == 0) || (option == 9);

                    } while (!success);
                    break;
                case 9 :
                    Console.Clear();
                    Methods.GetWeather();
                    Methods.GetNews();
                    break;
            }

            return option;
        }
        //---- Método - Menu Navios, devolve um int com a opção escolhida e recebe um objeto Seaport
        public static int MenuShips(Seaport s)
        {
            int option;
            
            bool success;

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\t\t=================== GERIR NAVIOS ===================\t\t");
            Console.ResetColor();
            Console.WriteLine("\t\t(1) Entrada de navio no porto");
            Console.WriteLine("\t\t(2) Saída de navio do porto");
            Console.WriteLine("\t\t(3) Chamar navio ao porto");
            Console.WriteLine("\t\t(4) Número Navios no porto");
            Console.WriteLine("\t\t(5) Número Navios ao Largo");
            Console.WriteLine("\t\t(6) Listar Número de contentores de um navio");
            Console.WriteLine("\t\t(7) Listar todos os contentores de um navio");
            Console.WriteLine("\t\t(8) Listar todos os navios");
            Console.WriteLine("\n\t\t(9) Menu Anterior");
            Console.WriteLine("\t\t(0) Sair");
            //recolha e validação da opção
            do
            {
                Console.Write("\n\t\tIndique a sua opção: ");
                success = int.TryParse(Console.ReadLine(), out option);
                if(!success) 
                    Console.WriteLine("\t\tOpção inválida!");
                if (success && (option < 0 || option > 9))
                {
                        Console.WriteLine("\t\tOpção inválida!");
                        success = false;
                }
                
            } while (!success);
            //---- chamada de métodos de acordo com a opção escolhida
            switch (option)
            {
                //Saí do programa acedendo ao Menu de Backup ou retorna ao menu anterior
                case 0:
                case 9:
                    break;
                //Adicionar Navio
                case 1:
                    Methods.AddShip(s);
                    break;
                //Listar e remover navio
                case 2:
                    Methods.RemoveShip(s);
                    break;
                //Chamar navio ao porto
                case 3:
                    Methods.CallShipToSeaport(s);
                    break;
                //Navios no porto
                case 4:
                    Methods.CountShipsAtSeaport(s);
                    break;
                //Navios ao largo
                case 5:
                    Methods.CountShipsAtLarge(s);
                    break;
                //Lista navios e indica o numero de contentores no navio indicado
                case 6:
                    Methods.ListContainerNumberAtShip(s);
                    break;
                //Lista navios e lista os contentores no navio indicado
                case 7:
                    Methods.ListContainersAtShip(s);
                    break;
                case 8:
                    Methods.ListShipAll(s);
                    break;
            }

            return option;
        }
        //---- Método - Menu COntentores, devolve um int com a opção escolhida e recebe um objeto Seaport
        public static int MenuContainers(Seaport s)
        {
            bool success;
            int option;

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\t\t=================== GERIR CONTENTORES ===================");
            Console.ResetColor();
            Console.WriteLine("\t\t(1) Entrada de contentor");
            Console.WriteLine("\t\t(2) Saída de contentores");
            Console.WriteLine("\t\t(3) Atribuir um contentor a um navio");
            Console.WriteLine("\t\t(4) Retirar um contentor de um navio");
            Console.WriteLine("\t\t(5) Listar todos os contentores não atribuídos");
            Console.WriteLine("\t\t(6) Listar todos os contentores");
            Console.WriteLine("\n\t\t(9) Menu Anterior");
            Console.WriteLine("\t\t(0) Sair");
            //recolha e validação da opção escolhida
            do
            {
                Console.Write("\n\t\tIndique a sua opção: ");
                success = int.TryParse(Console.ReadLine(), out option);
                if(!success) 
                    Console.WriteLine("\t\tOpção inválida!");
                if (success && (option < 0 || option > 6))
                {
                    if (option != 9)
                    {
                        Console.WriteLine("\t\tOpção inválida!");
                        success = false;
                    }
                }

            } while (!success);
            //---- chamada de métodos de acordo com a opção escolhida
            switch (option)
            {
                //Sai do programa acedendo ao menu de backup ou fica no menu anterior
                case 0 :
                case 9 :
                    break;
                //Adicionar contentor
                case 1:
                    Methods.AddContainer(s);
                    break;                
                //Remover contentor
                case 2:
                        Methods.RemoveContainer(s);
                    break;
                
                //Atribuir contentor a um navio;
                case 3:
                        Methods.AddContainerToShip(s);
                    break;
                
                //Retirar contentor de um navio;
                case 4:
                    Methods.RemoveContainerFromShip(s);
                    break;

                //Listar contentores não atribuídos
                case 5:
                    Methods.UnassignedContainers(s);
                    break;
                //listar todos os contentores;
                case 6 :
                    Methods.ListContainers(s);
                    break;
            }
            return option;
        }
    }
}