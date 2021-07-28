using System;
using System.Linq;

namespace OOP_Project
{
    public class MenuContainer
    {
        //---- Método - Menu Contentores, devolve um int com a opção escolhida e recebe um objeto Seaport
        public static int Menu(Seaport s)
        {
            bool success;
            int option;

            Console.Clear();
            Console.WriteLine();
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
                    AddContainer(s);
                    break;                
                //Remover contentor
                case 2:
                    RemoveContainer(s);
                    break;
                
                //Atribuir contentor a um navio;
                case 3:
                    AddContainerToShip(s);
                    break;
                
                //Retirar contentor de um navio;
                case 4:
                    RemoveContainerFromShip(s);
                    break;

                //Listar contentores não atribuídos
                case 5:
                    UnassignedContainers(s);
                    break;
                //listar todos os contentores;
                case 6 :
                    ListContainers(s);
                    break;
            }
            return option;
        }
        
        //---- Método para adicionar um contentor ao parque
        public static void AddContainer(Seaport s)
        {
            bool success, isPlaticExplosive, isRefrigerated;
            string destination, typeChemical, typeExplosive, description;
            int weight;
            ConsoleKeyInfo k;

            Console.WriteLine("\t\tIndique o tipo de contentor (R) Regular, (E) Explosivos ou (Q) Químicos");
            do
            {
                Console.Write("\t\t");
                k = Console.ReadKey();
                success = (k.Key == ConsoleKey.R || k.Key == ConsoleKey.E || k.Key == ConsoleKey.Q);
                if (!success)
                    Console.WriteLine("\t\tOpção inválida! Escolha (R) Regular, (E) Explosivos ou (Q) Químicos");

            } while (!success);

            switch (k.Key)
            {
                case ConsoleKey.R:
                    do
                    {
                        Console.Write("\n\t\tDestino: ");
                        destination = Console.ReadLine();
                        if (string.IsNullOrEmpty(destination))
                        {
                            Console.WriteLine("\t\tCampo de preenchimento obrigatóro!");
                        }
                    } while (string.IsNullOrEmpty(destination));
                    
                    do
                    {
                        Console.Write("\t\tPeso: ");
                        success = int.TryParse(Console.ReadLine(), out weight);

                    } while (!success);

                    do
                    {
                        Console.Write("\t\tDescrição da carga: ");
                        description = Console.ReadLine();
                        if (string.IsNullOrEmpty(description))
                        {
                            Console.WriteLine("\t\tCampo de preenchimento obrigatóro!");
                        }
                        
                    } while (string.IsNullOrEmpty(description));

                    Console.WriteLine("\t\tRefrigerado (S) Sim ou (N) Não");
                    do
                    {
                        Console.Write("\t\t");
                        k = Console.ReadKey();
                        success = (k.Key == ConsoleKey.S || k.Key == ConsoleKey.N);
                        if (!success)
                            Console.WriteLine("\t\tOpção inválida! Escolha (S) Sim ou (N) Não");

                    } while (!success);

                    if (k.Key == ConsoleKey.S)
                    {
                        isRefrigerated = true;
                    }
                    else
                    {
                        isRefrigerated = false;
                    }

                    var r = new Regular(description, isRefrigerated, destination, weight);
                    if (s.Containers.Contains(r))
                    {
                        Console.WriteLine("\t\tEste contentor já existe!!");
                        Console.WriteLine("\t\tContentor não adicionado");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n\t\tPrima uma tecla para continuar");
                        Console.ReadKey();
                        Console.ResetColor();
                    }
                    else
                    {
                        try
                        {
                            s.Containers.Add(r);
                            Console.WriteLine("\n\t\tContentor adicionado com sucesso!!");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("\n\t\tPrima uma tecla para continuar");
                            Console.ReadKey();
                            Console.ResetColor();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("\n\t\tPrima uma tecla para continuar");
                            Console.ReadKey();
                            Console.ResetColor();
                        }
                    }

                    break;

                case ConsoleKey.E:
                    do
                    {
                        Console.Write("\n\t\tDestino: ");
                        destination = Console.ReadLine();
                        if (string.IsNullOrEmpty(destination))
                        {
                            Console.WriteLine("\t\tCampo de preenchimento obrigatóro!");
                        }
                    } while (string.IsNullOrEmpty(destination));
                    
                    do
                    {
                        Console.Write("\t\tPeso: ");
                        success = int.TryParse(Console.ReadLine(), out weight);

                    } while (!success);

                    do
                    {
                        Console.Write("\t\tDescrição do explosivo: ");
                        typeExplosive = Console.ReadLine();

                        if (string.IsNullOrEmpty(typeExplosive))
                        {
                            Console.WriteLine("\t\tCampo de preenchimento obrigatóro!");
                        }
                    } while (string.IsNullOrEmpty(typeExplosive));

                    Console.WriteLine("\t\tExplosivo Plástico (S) Sim ou (N) Não");
                    do
                    {
                        Console.Write("\t\t");
                        k = Console.ReadKey();
                        success = (k.Key == ConsoleKey.S || k.Key == ConsoleKey.N);
                        if (!success)
                            Console.WriteLine("\t\tOpção inválida! Escolha (S) Sim ou (N) Não");

                    } while (!success);

                    if (k.Key == ConsoleKey.S)
                    {
                        isPlaticExplosive = true;
                    }
                    else
                    {
                        isPlaticExplosive = false;
                    }

                    var explosive = new Explosive(typeExplosive, isPlaticExplosive, destination, weight);
                    if (s.Containers.Contains(explosive))
                    {
                        Console.WriteLine("\t\tEste contentor já existe!!");
                        Console.WriteLine("\t\tContentor não adicionado");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n\t\tPrima uma tecla para continuar...");
                        Console.ReadKey();
                        Console.ResetColor();
                    }
                    else
                    {
                        try
                        {
                            s.Containers.Add(explosive);
                            Console.WriteLine("\n\t\tContentor adicionado com sucesso!!");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("\n\t\tPrima uma tecla para continuar...");
                            Console.ReadKey();
                            Console.ResetColor();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("\n\t\tPrima uma tecla para continuar");
                            Console.ReadKey();
                            Console.ResetColor();
                        }
                    }

                    break;

                case ConsoleKey.Q:
                    do
                    {
                        Console.Write("\n\t\tDestino: ");
                        destination = Console.ReadLine();
                        if (string.IsNullOrEmpty(destination))
                        {
                            Console.WriteLine("\t\tCampo de preenchimento obrigatóro!");
                        }
                    } while (string.IsNullOrEmpty(destination));
                    
                    do
                    {
                        Console.Write("\t\tPeso: ");
                        success = int.TryParse(Console.ReadLine(), out weight);

                    } while (!success);

                    do
                    {
                        Console.Write("\t\tDescrição do químico: ");
                        typeChemical = Console.ReadLine();
                        if (string.IsNullOrEmpty(typeChemical))
                        {
                            Console.WriteLine("\t\tCampo de preenchimento obrigatóro!");
                        }
                    } while (string.IsNullOrEmpty(typeChemical));

                    var chemical = new Chemical(typeChemical, destination, weight);
                    if (s.Containers.Contains(chemical))
                    {
                        Console.WriteLine("\t\tEste contentor já existe!!");
                        Console.WriteLine("\n\t\tContentor não adicionado");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n\t\tPrima uma tecla para continuar...");
                        Console.ReadKey();
                        Console.ResetColor();
                    }
                    else
                    {
                        try
                        {
                            s.Containers.Add(chemical);
                            Console.WriteLine("\n\t\tContentor adicionado com sucesso!!");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("\n\t\tPrima uma tecla para continuar...");
                            Console.ReadKey();
                            Console.ResetColor();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("\n\t\tPrima uma tecla para continuar");
                            Console.ReadKey();
                            Console.ResetColor();

                        }
                    }

                    break;
            }
        }
        
        //--- Métdodo para remover contentores do parque
        public static void RemoveContainer(Seaport s)
        {
            bool success;
            int option;
            string numContainer;
            Container aux;

            option = ListContainers(s);
            if (option == 0) return;

            Console.WriteLine("\n\t\tIndique o número do contentor que quer remover: ");

            do
            {
                Console.Write("\t\t");
                numContainer = Console.ReadLine();

                if (s.Containers.Find(c => c.GetNumber().Equals(numContainer)) == null)
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
                else
                {
                    success = true;
                }

            } while (!success);

            aux = s.Containers.Find(s => s.GetNumber().Equals(numContainer));

            try
            {
                s.Containers.Remove(aux);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();
            }

            Console.WriteLine("\n\t\tContentor removido com sucesso!!");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
            Console.ResetColor();
        }

        //---- Método para adicionar contentor ao novio;
        public static void AddContainerToShip(Seaport s)
        {
            bool success;
            int option;
            string numContainer;
            Container auxContainer;
            Ship auxShip;

            option = UnassignedContainers(s);
            if (option == 0) return;

            Console.WriteLine("\n\t\tIndique o número do contentor: ");

            do
            {
                Console.Write("\t\t");
                numContainer = Console.ReadLine();

                if (s.Containers.Find(c => c.GetNumber().Equals(numContainer)) == null)
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
                else
                {
                    success = true;
                }

            } while (!success);

            auxContainer = s.Containers.Find(s => s.GetNumber().Equals(numContainer));

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

            auxShip = s.Ships.Find(s => s.GetNumber() == option);

            try
            {
                auxContainer.SetShipNumber(auxShip.GetNumber());
                auxShip.AddContainer(auxContainer);
                Console.WriteLine("\n\t\tContentor adicionado com sucesso!!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar");
                Console.ReadKey();
                Console.ResetColor();
            }
            catch (MaxContainersException e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar");
                Console.ReadKey();
                Console.ResetColor();

            }

            catch (MaxExplosiveException e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar");
                Console.ReadKey();
                Console.ResetColor();
            }

            catch (MaxChemicalException e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar");
                Console.ReadKey();
                Console.ResetColor();
            }

        }
        
        //---- Método para remover contentor de um navio
        public static void RemoveContainerFromShip(Seaport s)
        {
            bool success;
            int option;
            string numContainer;
            Container auxContainer;
            Ship auxShip;

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

            auxShip = s.Ships.Find(s => s.GetNumber() == option);

            if (auxShip.GetIsAtPort())
            {
                auxShip.ListContainers();
            }
            else
            {
                Console.WriteLine("\t\tO navio indicado não se encontra no porto");
                Console.WriteLine("\t\tChamar navio ao porto? (S) Sim ou (N) Não");
                ConsoleKeyInfo a;
                do
                {
                    success = false;
                    Console.Write("\t\t");
                    a = Console.ReadKey();

                    if (a.Key == ConsoleKey.S || a.Key == ConsoleKey.N)
                    {
                        success = true;
                    }
                    else
                    {
                        Console.WriteLine("\t\tOpção inválida");
                    }

                } while (!success);

                try
                {
                    auxShip.SetIsAtPort(true);
                    Console.WriteLine("\t\tO navio regressou ao porto!");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\t\tPrima uma tecla para continuar...");
                    Console.ReadKey();
                    Console.ResetColor();
                    
                    auxShip.ListContainers();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\t\tPrima uma tecla para continuar...");
                    Console.ReadKey();
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\n\t\tIndique o número do contentor: ");

            do
            {
                Console.Write("\t\t");
                numContainer = Console.ReadLine();

                if (auxShip.GetContainers().Find(c => c.GetNumber().Equals(numContainer)) == null)
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
                else
                {
                    success = true;
                }

            } while (!success);

            auxContainer = auxShip.GetContainers().Find(c => c.GetNumber().Equals(numContainer));

            try
            {
                auxShip.GetContainers().Remove(auxContainer);

                auxContainer = s.Containers.Find(x => x.GetShipNumber() == auxShip.GetNumber());
                auxContainer.SetShipNumber(-1);

                Console.WriteLine("\t\tContentor removido com sucesso!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar");
                Console.ReadKey();
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();

            }

        }
        
        //---- Método para listar os contentoes sem navio atribuido
        public static int UnassignedContainers(Seaport s)
        {
            int counter = 0, unassignedContainersCount;

            if (s.Containers.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem contentores associados ao porto!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();
                
                return counter;
            }

            foreach (var c in s.Containers)
            {
                if (c.GetShipNumber() == -1)
                {
                    counter++;
                }
            }

            unassignedContainersCount = counter;

            if (unassignedContainersCount > 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(
                    "\t\t===================================== LISTA DE CONTENTORES =====================================\t\t");
                Console.ResetColor();
                counter = 0;
                foreach (var c in s.Containers.Where(c => c.GetShipNumber() == -1))
                {
                    Console.WriteLine(c);
                    counter++;
                    if (unassignedContainersCount != counter)
                        MainMenu.RecordsPerPage(counter, 3);
                }
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima qualquer tecla para continuar...");
                Console.ResetColor();
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("\n\t\tTodos os contentores estão atribuídos!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima qualquer tecla para continuar...");
                Console.ResetColor();
                Console.ReadKey();

            }

            return counter;
        }
        
        //--- Método para listar contentores e reutilizado noutros métodos
        public static int ListContainers(Seaport s)
        {
            int counter = 0;
            if (s.Containers.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem contentores no porto!!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n\t\tPrima uma tecla para continuar");
                Console.ReadKey();
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(
                    "\t\t===================================== LISTA DE CONTENTORES =====================================\t\t");
                Console.ResetColor();
                foreach (var c in s.Containers)
                {

                    Console.WriteLine(c);
                    counter++;
                    if (s.Containers.Count != counter)
                        MainMenu.RecordsPerPage(counter, 3);
                }
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n\t\tPrima uma tecla para continuar");
                Console.ReadKey();
                Console.ResetColor();

            }

            return s.Containers.Count;
        }

    }
}