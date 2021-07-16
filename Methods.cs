using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace OOP_Project
{
    public class Methods
    {
        public static void Backup(State s)
        {
            IFormatter formatador = new BinaryFormatter();
            Stream stream = new FileStream("Estado.txt", FileMode.Create, FileAccess.Write);

            formatador.Serialize(stream, s);
            stream.Close();
        }
        
        public static State Restore()
        {
            try
            {
                IFormatter formatador = new BinaryFormatter();
                Stream stream = new FileStream("Estado.txt", FileMode.Open, FileAccess.Read);
                State e = (State) formatador.Deserialize(stream);
                stream.Close();

                e.GetShips().ForEach(s => Console.WriteLine(s));
                e.GetContainers().ForEach(c => Console.WriteLine(c));

                return e;
            }
            catch (Exception e)
            {
                Console.WriteLine("\t\t" + e.Message);
            }

            return null;
        }

        public static void AddShip(List<Ship> ships)
        {
            bool success;
            string name, flag;
            int maxContainers, maxExplosive, maxChemical;
            
            Console.WriteLine("\t\tInsira os dados do navio");
            Console.Write("\t\tNome: ");
            name = Console.ReadLine();
            
            do
            {
                Console.Write("\t\tCapacidade total geral de contentores: ");
                success = int.TryParse(Console.ReadLine(), out maxContainers);

            } while (!success);

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
            
            Console.Write("\t\tBandeira: ");
            flag = Console.ReadLine();

            Ship s = new Ship(name, maxContainers, maxExplosive, maxChemical, flag);

            if (ships.Find(x => x.GetNumber() == s.GetNumber()) != null)
            {
                Console.WriteLine("\t\tEste navio já existe!");
                Console.WriteLine("\n\t\tNavio não adicionado!");
            }
            else
            {
                try
                {
                    ships.Add(s);
                    Console.WriteLine("\t\tNavio adicionado com sucesso!!");
                    Console.WriteLine("\n\t\tPrima um enter para continuar");
                    Console.Write("\t\t");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static int ListShip(List<Ship> ships)
        {
            if (ships.Count == 0)
            {
                Console.WriteLine("\t\tNão existem navios no porto!!");
                Console.Write("\n\n\t\tPrima enter para continuar\t\t");
                Console.ReadLine();
            }
            else
            {
                foreach (var s in ships)
                {
                    Console.WriteLine(s);
                }
            }

            return ships.Count;
        }

        public static void RemoveShip(List<Ship> ships)
        {
            bool success;
            int option;
            Ship aux;
            
            Console.Write("\n\t\tIndique o número do navio: ");

            do
            {
                Console.Write("\t\t");
                success = int.TryParse(Console.ReadLine(), out option);
                
                if (success && (ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    success = false;

                    ConsoleKeyInfo k;
                    k = Console.ReadKey();
                    
                    if (k.Key == ConsoleKey.S)
                    {
                        return;
                    }
                }
                
            } while (!success);

            aux = ships.Find(s => s.GetNumber() == option);

            aux.SetIsAtPort(false);
            Console.WriteLine("\n\t\tNavio registado como fora do porto!!");
        }
        
        public static void AddContainer(List<Container> containers)
        {
            bool success, isPlaticExplosive, isRefrigerated;
            string destination, typeChemical, typeExplosive, description;
            int weight;
            ConsoleKeyInfo k;
            
            Console.WriteLine("\t\tIndique o tipo de contentor (R) Regular, (E) Explosivos ou (Q) Químicos");
            do
            {
                k = Console.ReadKey();
                success = (k.Key == ConsoleKey.R || k.Key == ConsoleKey.E || k.Key == ConsoleKey.Q);
                if(!success)
                    Console.WriteLine("\t\tOpção inválida! Escolha (R) Regular, (E) Explosivos ou (Q) Químicos");

            } while (!success);

            switch (k.Key)
            {
                case ConsoleKey.R :
                    Console.Write("\t\tDestino: ");
                    destination = Console.ReadLine();
                    do
                    {
                        Console.Write("\t\tPeso: ");
                        success = int.TryParse(Console.ReadLine(), out weight);

                    } while (!success);
                    
                    Console.Write("\t\tDescrição da carga: ");
                    description = Console.ReadLine();
                    
                    Console.WriteLine("\t\tRefrigerado (S) Sim ou (N) Não");
                    do
                    {
                        k = Console.ReadKey();
                        success = (k.Key == ConsoleKey.S || k.Key == ConsoleKey.N);
                        if(!success)
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
                    if (containers.Contains(r))
                    {
                        Console.WriteLine("\t\tEste contentor já existe!!");
                        Console.WriteLine("\n\t\tContentor não adicionado");
                    }
                    else
                    {
                        try
                        {
                            containers.Add(r);
                            Console.WriteLine("\t\tContentor adicionado com sucesso!!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    break;
                
                case ConsoleKey.E :
                    Console.Write("\t\tDestino: ");
                    destination = Console.ReadLine();
                    do
                    {
                        Console.Write("\t\tPeso: ");
                        success = int.TryParse(Console.ReadLine(), out weight);

                    } while (!success);
                    
                    Console.Write("\t\tDescrição do explosivo: ");
                    typeExplosive = Console.ReadLine();
                    
                    Console.WriteLine("\t\tExplosivo Plástico (S) Sim ou (N) Não");
                    do
                    {
                        k = Console.ReadKey();
                        success = (k.Key == ConsoleKey.S || k.Key == ConsoleKey.N);
                        if(!success)
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
                    if (containers.Contains(explosive))
                    {
                        Console.WriteLine("\t\tEste contentor já existe!!");
                        Console.WriteLine("\n\t\tContentor não adicionado");
                    }
                    else
                    {
                        try
                        {
                            containers.Add(explosive);
                            Console.WriteLine("\t\tContentor adicionado com sucesso!!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    break;
                
                case ConsoleKey.Q:
                    Console.Write("\t\tDestino: ");
                    destination = Console.ReadLine();
                    do
                    {
                        Console.Write("\t\tPeso: ");
                        success = int.TryParse(Console.ReadLine(), out weight);

                    } while (!success);
                    
                    Console.Write("\t\tDescrição do químico: ");
                    typeChemical = Console.ReadLine();
                    
                    var chemical = new Chemical(typeChemical, destination, weight);
                    if (containers.Contains(chemical))
                    {
                        Console.WriteLine("\t\tEste contentor já existe!!");
                        Console.WriteLine("\n\t\tContentor não adicionado");
                    }
                    else
                    {
                        try
                        {
                            containers.Add(chemical);
                            Console.WriteLine("\t\tContentor adicionado com sucesso!!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    break;
            }
        }
        
        public static int ListContainers(List<Container> containers)
        {
            if (containers.Count == 0)
            {
                Console.WriteLine("\t\tNão existem contentores no porto!!");
                Console.Write("\n\n\t\tPrima enter para continuar\t\t");
                Console.ReadLine();
            }
            else
            {
                foreach (var c in containers)
                {
                    Console.WriteLine(c);
                }
            }

            return containers.Count;
        }
        
        public static void RemoveContainer(List<Container> containers)
        {
            bool success;
            int option;
            string numContainer;
            Container aux;
            
            Console.WriteLine("\n\t\tIndique o número do contentor que quer remover: ");

            do
            {
                Console.WriteLine("\t\t");
                numContainer = Console.ReadLine();
                
                if (containers.Find(c => c.GetNumber().Equals(numContainer)) == null)
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    Console.WriteLine("\t\t");
                    success = false;

                    ConsoleKeyInfo k;
                    k = Console.ReadKey();
                    
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

            aux = containers.Find(s => s.GetNumber().Equals(numContainer));

            containers.Remove(aux);
            Console.WriteLine("\n\t\tContentor removido com sucesso!!");
        }

        public static void CheckShipsAtPort(List<Ship> ships)
        {
            int counter = 0;

            if (ships.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem navios no porto!");
            }
            else
            {
                foreach (var ship in ships)
                {
                    if (ship.GetIsAtPort() == true)
                    {
                        counter++;
                    }
                }

                Console.WriteLine("\t\tNeste momento encontram-se no porto {0} navios", counter);
                Console.WriteLine("\n\t\tPrima qualquer  tecla para continuar...");
                Console.Write("\t\t");
                Console.ReadLine();
            }
        }
        
        public static void ListContainersAtShip(List<Ship> ships)
        {
            bool success;
            int option;
            Ship aux;
            
            Console.WriteLine("\n\t\tIndique o número do navio: ");

            do
            {
                Console.WriteLine("\t\t");
                success = int.TryParse(Console.ReadLine(), out option);
                
                if (success && (ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    Console.WriteLine("\t\t");
                    success = false;

                    ConsoleKeyInfo k;
                    k = Console.ReadKey();
                    
                    if (k.Key == ConsoleKey.S)
                    {
                        return;
                    }
                }
                
            } while (!success);

            aux = ships.Find(s => s.GetNumber() == option);
            if (aux.GetContainers().Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem contentores no navio {0}", aux.GetName());
            }
            else
            {
                aux.ListContainers();
            }
            
            Console.WriteLine("Prima qualquer tecla para continuar...");
            Console.ReadLine();

        }
    
        public static void CountContainersAtShip(List<Ship> ships)
        {
            bool success;
            int option, counter;
            Ship aux;
            
            Console.WriteLine("\n\t\tIndique o número do navio: ");

            do
            {
                Console.WriteLine("\t\t");
                success = int.TryParse(Console.ReadLine(), out option);
                
                if (success && (ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    Console.WriteLine("\t\t");
                    success = false;

                    ConsoleKeyInfo k;
                    k = Console.ReadKey();
                    
                    if (k.Key == ConsoleKey.S)
                    {
                        return;
                    }
                }
                
            } while (!success);

            aux = ships.Find(s => s.GetNumber() == option);

            if ((counter = aux.GetContainers().Count) == 0)
            {
                Console.WriteLine("\n\t\tNão existem contentores no navio {0}", aux.GetName());
            }
            else
            {
                Console.WriteLine("\t\tO navio {0} tem {1} contentores", aux.GetName(), counter);
            }
            Console.WriteLine("Prima qualquer tecla para continuar...");
            Console.ReadLine();

        }

        public static void AddContainerToShip(List<Ship> ships, List<Container> containers)
        {
            bool success;
            int option;
            string numContainer;
            Container auxContainer;
            Ship auxShip;
            
            Console.WriteLine("\n\t\tIndique o número do contentor: ");

            do
            {
                Console.WriteLine("\t\t");
                numContainer = Console.ReadLine();
                
                if (containers.Find(c => c.GetNumber().Equals(numContainer)) == null)
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    Console.WriteLine("\t\t");
                    success = false;

                    ConsoleKeyInfo k;
                    k = Console.ReadKey();
                    
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

            auxContainer = containers.Find(s => s.GetNumber().Equals(numContainer));

            ships.ForEach(Console.WriteLine);
            Console.WriteLine("\n\t\tIndique o número do navio: ");

            do
            {
                Console.WriteLine("\t\t");
                success = int.TryParse(Console.ReadLine(), out option);
                
                if (success && (ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    Console.WriteLine("\t\t");
                    success = false;

                    ConsoleKeyInfo k;
                    k = Console.ReadKey();
                    
                    if (k.Key == ConsoleKey.S)
                    {
                        return;
                    }
                }
                
            } while (!success);

            auxShip = ships.Find(s => s.GetNumber() == option);
            try
            {
                auxContainer.SetShipNumber(auxShip.GetNumber());
                auxShip.AddContainer(auxContainer);
                Console.WriteLine("\n\t\tContentor adicionado com sucesso!!");
            }
            catch (MaxContainersException e)
            {
                Console.WriteLine(e.Message);
            }
            
            catch (MaxExplosiveException e)
            {
                Console.WriteLine(e.Message);
            }
            
            catch (MaxChemicalException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public static void RemoveContainerFromShip(List<Ship> ships, List<Container> containers)
        {
            bool success;
            int option;
            string numContainer;
            Container auxContainer;
            Ship auxShip;
            
            Console.WriteLine("\n\t\tIndique o número do navio: ");

            do
            {
                Console.WriteLine("\t\t");
                success = int.TryParse(Console.ReadLine(), out option);
                
                if (success && (ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    Console.WriteLine("\t\t");
                    success = false;

                    ConsoleKeyInfo k;
                    k = Console.ReadKey();
                    
                    if (k.Key == ConsoleKey.S)
                    {
                        return;
                    }
                }
                
            } while (!success);

            auxShip = ships.Find(s => s.GetNumber() == option);
            
            auxShip.ListContainers();
            Console.WriteLine("\n\t\tIndique o número do contentor: ");

            do
            {
                Console.WriteLine("\t\t");
                numContainer = Console.ReadLine();
                
                if (auxShip.GetContainers().Find(c => c.GetNumber().Equals(numContainer)) == null)
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    Console.WriteLine("\t\t");
                    success = false;

                    ConsoleKeyInfo k;
                    k = Console.ReadKey();
                    
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

                auxContainer = containers.Find(x => x.GetShipNumber() == auxShip.GetNumber());
                auxContainer.SetShipNumber(-1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        
        
    }
}