using System;
using System.Linq;

namespace OOP_Project
{
    public class MenuShip
    {
        //---- Método - Menu Navios, devolve um int com a opção escolhida e recebe um objeto Seaport
        public static int Menu(Seaport s)
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
                    AddShip(s);
                    break;
                //Listar e remover navio
                case 2:
                    RemoveShip(s);
                    break;
                //Chamar navio ao porto
                case 3:
                    CallShipToSeaport(s);
                    break;
                //Navios no porto
                case 4:
                    CountShipsAtSeaport(s);
                    break;
                //Navios ao largo
                case 5:
                    CountShipsAtLarge(s);
                    break;
                //Lista navios e indica o numero de contentores no navio indicado
                case 6:
                    ListContainerNumberAtShip(s);
                    break;
                //Lista navios e lista os contentores no navio indicado
                case 7:
                    ListContainersAtShip(s);
                    break;
                case 8:
                    ListShipAll(s);
                    break;
            }

            return option;
        }
        
        //---- Método para adicionar novos navios, o numero de navio é atribuido
        //automaticamente e o navio fica adicionado ao porto
        public static void AddShip(Seaport s)
        {
            bool success;
            string name, flag;
            int maxContainers, maxExplosive, maxChemical;
            
            Console.WriteLine("\t\tInsira os dados do navio");
            do
            {
                Console.Write("\t\tNome: ");
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("\t\tCampo de preenchimento obrigatório!");
                }
            } while (string.IsNullOrEmpty(name));
    
            do
            {
                Console.Write("\t\tCapacidade total de contentores: ");
                success = int.TryParse(Console.ReadLine(), out maxContainers);

            } while (!success);

            do
            {
                do
                {
                    Console.Write("\t\tCapacidade total para contentores de Explosivos: ");
                    success = int.TryParse(Console.ReadLine(), out maxExplosive);

                } while (!success);

                do
                {
                    Console.Write("\t\tCapacidade total para contentores de Químicos: ");
                    success = int.TryParse(Console.ReadLine(), out maxChemical);

                } while (!success);

                if (maxChemical + maxExplosive > maxContainers)
                {
                    Console.WriteLine("\t\tO número maxímo de contentores Químicos e Explosivos não pode exceder" +
                                      " o total de contentores do navio");
                    success = false;
                }

            } while (!success);

            do
            {
                Console.Write("\t\tBandeira: ");
                flag = Console.ReadLine();
                if (string.IsNullOrEmpty(flag))
                {
                    Console.WriteLine("\t\tCampo de preenchimento obrigatório!!");
                }
            } while (string.IsNullOrEmpty(flag));

            Ship auxShip = new Ship(name, maxContainers, maxExplosive, maxChemical, flag);

            if (s.Ships.Find(x => x.GetNumber() == auxShip.GetNumber()) != null)
            {
                Console.WriteLine("\t\tEste navio já existe!");
                Console.WriteLine("\n\t\tNavio não adicionado!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\t\tPrima ums tecla para continuar");
                Console.Write("\t\t");
                Console.ReadKey();
                Console.ResetColor();
            }
            else
            {
                try
                {
                    s.Ships.Add(auxShip);
                    Console.WriteLine("\t\tNavio adicionado com sucesso!!");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\t\tPrima uma tecla para continuar");
                    Console.Write("\t\t");
                    Console.ReadKey();
                    Console.ResetColor();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\t\tPrima uma tecla para continuar");
                    Console.Write("\t\t");
                    Console.ReadKey();
                    Console.ResetColor();
                }
            }
        }
        
        //---- Método para remover navios
        public static void RemoveShip(Seaport s)
        {
            bool success;
            int option;
            Ship aux;

            option = MainMenu.ListShipAtSeaport(s);
            if (option == 0) return;

            Console.WriteLine("\n\t\tIndique o número do navio: ");

            do
            {
                Console.Write("\t\t");
                success = int.TryParse(Console.ReadLine(), out option);

                if (success && (s.Ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine(
                        "\t\tO número que indicou não existe, prima ENTER para continuar ou prima S para Sair");
                    success = false;

                    ConsoleKeyInfo k;
                    Console.Write("\t\t");

                    do
                    {
                        k = Console.ReadKey();


                    } while (k.Key != ConsoleKey.S && k.Key != ConsoleKey.Enter);

                    if (k.Key == ConsoleKey.S)
                    {
                        return;
                    }
                }

            } while (!success);

            aux = s.Ships.Find(s => s.GetNumber() == option);
            try
            {
                aux.SetIsAtPort(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();
            }

            Console.WriteLine("\n\t\tNavio registado como fora do porto!!");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
            Console.ResetColor();
        }
        
        //---- Método para mudar o estado do navio para estando no porto é reutilizado noutros métods
        public static void CallShipToSeaport(Seaport s)
        {
            bool success;
            int option, shipsAtLarge;
            Ship aux;

            shipsAtLarge = ListShipAtLarge(s);

            if (shipsAtLarge == 0) return;

            Console.WriteLine("\n\t\tIndique o número do navio: ");

            do
            {
                Console.Write("\t\t");
                success = int.TryParse(Console.ReadLine(), out option);

                if (success && (s.Ships.Find(s => s.GetNumber() == option) == null))
                {

                    Console.WriteLine(
                        "\t\tO número que indicou não existe, prima ENTER para continuar ou prima S para Sair");
                    success = false;
                    Console.Write("\t\t");

                    ConsoleKeyInfo k;
                    do
                    {
                        k = Console.ReadKey();

                    } while (k.Key != ConsoleKey.S && k.Key != ConsoleKey.Enter);

                    if (k.Key == ConsoleKey.S)
                    {
                        return;
                    }
                }

            } while (!success);

            aux = s.Ships.Find(s => s.GetNumber() == option);

            try
            {
                aux.SetIsAtPort(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();
                return;
            }

            Console.WriteLine("\n\t\tNavio registado como estando no porto!!");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
            Console.ResetColor();

        }
        
        //---- Método para contar os navios no porto
        public static void CountShipsAtSeaport(Seaport s)
        {
            int counter = 0;

            if (s.Ships.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem navios no porto!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar");
                Console.ReadKey();
                Console.ResetColor();
            }
            else
            {
                foreach (var ship in s.Ships)
                {
                    if (ship.GetIsAtPort())
                    {
                        counter++;
                    }
                }

                Console.WriteLine("\n\t\tNeste momento encontram-se {0} navios no porto", counter);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\t\tPrima qualquer  tecla para continuar...");
                Console.Write("\t\t");
                Console.ReadKey();
                Console.ResetColor();
            }
        }
        
        //---- Método para contar os navios ao largo
        public static void CountShipsAtLarge(Seaport s)
        {
            int counter = 0;

            if (s.Ships.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem navios ao largo!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar");
                Console.ReadKey();
                Console.ResetColor();
            }
            else
            {
                foreach (var ship in s.Ships)
                {
                    if (!ship.GetIsAtPort())
                    {
                        counter++;
                    }
                }

                Console.WriteLine("\n\t\tNeste momento encontram-se {0} navios ao largo", counter);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima qualquer  tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();
            }
        }
        
        //---- Método que lista os números dos contentores existentes num navio
        public static void ListContainerNumberAtShip(Seaport s)
        {
            bool success;
            int option;
            Ship aux;

            option = MainMenu.ListAllShipsWithContainers(s);
            if (option == 0) return;

            Console.WriteLine("\n\t\tIndique o número do navio: ");

            do
            {
                Console.Write("\t\t");
                success = int.TryParse(Console.ReadLine(), out option);

                if (success && (s.Ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine(
                        "\t\tO número que indicou não existe, prima ENTER para continuar ou prima S para Sair");
                    success = false;
                    Console.Write("\t\t");

                    ConsoleKeyInfo k;
                    do
                    {
                        k = Console.ReadKey();

                    } while (k.Key != ConsoleKey.S && k.Key != ConsoleKey.Enter);

                    if (k.Key == ConsoleKey.S)
                    {
                        return;
                    }
                }

            } while (!success);

            aux = s.Ships.Find(s => s.GetNumber() == option);

            Console.Write("\t\t---- Contentores no navio ----\n");
            Console.WriteLine("\t\tTotal: {0}/{1}", aux.GetContainers().Count, aux.GetMaxContainers());
            Console.WriteLine("\t\tExplosivo: {0}/{1}", aux.GetContainers().OfType<Explosive>().Count(),
                aux.GetMaxExplosive());
            Console.WriteLine("\t\tQuímico: {0}/{1}", aux.GetContainers().OfType<Chemical>().Count(),
                aux.GetMaxChemical());

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
            Console.ResetColor();

        } 
        
        //---- Método que lista com detalhe os contentores existentes num navio
        public static void ListContainersAtShip(Seaport s)
        {
            bool success;
            int option, counter = 0;
            Ship aux;

            option = MainMenu.ListAllShipsWithContainers(s);
            if (option == 0) return;

            Console.WriteLine("\n\t\tIndique o número do navio: ");

            do
            {
                Console.Write("\t\t");
                success = int.TryParse(Console.ReadLine(), out option);

                if (success && (s.Ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine(
                        "\t\tO número que indicou não existe, prima ENTER para continuar ou prima S para Sair");
                    success = false;
                    Console.Write("\t\t");

                    ConsoleKeyInfo k;
                    do
                    {
                        k = Console.ReadKey();

                    } while (k.Key != ConsoleKey.S && k.Key != ConsoleKey.Enter);

                    if (k.Key == ConsoleKey.S)
                    {
                        return;
                    }
                }

            } while (!success);

            aux = s.Ships.Find(s => s.GetNumber() == option);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(
                "\t\t===================================== LISTA DE CONTENTORES =====================================\t\t");
            Console.ResetColor();
            
            foreach (var c in aux.GetContainers())
            {
                Console.WriteLine(c);
                counter++;
                if (aux.GetContainers().Count != counter)
                    MainMenu.RecordsPerPage(counter, 3);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
            Console.ResetColor();

        }

        //---- Método para listar todos os navios e reutilizado noutros métodos
        public static int ListShipAll(Seaport s)
        {
            int counter = 0;

            if (s.Ships.Count == 0)
            {
                Console.WriteLine("\t\tNão existem navios associados ao porto!!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(
                    "\t\t===================================== LISTA DE NAVIOS =====================================\t\t");
                Console.ResetColor();
                
                foreach (var ship in s.Ships)
                {
                    Console.WriteLine(ship);
                    counter++;
                    if (s.Ships.Count != counter)
                    {
                        MainMenu.RecordsPerPage(counter, 4);
                    }
                }
            }

            return s.Ships.Count;
        }
        
        //---- Método para listar os navios que estão ao largo
        public static int ListShipAtLarge(Seaport s)
        {
            int counter = 0, shipsAtLarge;

            foreach (var ship in s.Ships)
            {
                if (!ship.GetIsAtPort())
                {
                    counter++;
                }
            }

            shipsAtLarge = counter;

            if (shipsAtLarge > 0)
            {
                counter = 0;
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(
                    "\t\t===================================== LISTA DE NAVIOS =====================================\t\t");
                Console.ResetColor();
                
                foreach (var ship in s.Ships.Where(ship => !ship.GetIsAtPort()))
                {
                    Console.WriteLine(ship);
                    counter++;
                    if (shipsAtLarge != counter)
                        MainMenu.RecordsPerPage(counter, 4);
                }
            }
            else
            {
                Console.WriteLine("\t\tNão existem navios ao largo!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();
            }

            return counter;
        }

    }
}