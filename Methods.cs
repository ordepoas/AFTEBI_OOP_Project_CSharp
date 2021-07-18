using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace OOP_Project
{
    public class Methods
    {
        public static void Backup(State s)
        {
            //------- BACKUP PARA TXT
            try
            {
                IFormatter formatador = new BinaryFormatter();
                Stream streamTxt = new FileStream("backup.txt", FileMode.Create, FileAccess.Write);

                formatador.Serialize(streamTxt, s);
                
                streamTxt.Close();
                Console.WriteLine("\t\tBackup efetuado com sucesso!");

            }
            catch (Exception e)
            {
                Console.Write("\t\t");
                Console.WriteLine(e.Message);
                Console.Write("\t\tPrima qualquer tecla para continuar...");
                Console.ReadLine();

            }

            //-------- BACKUP PARA JSON
            try
            {
                
                DataContractJsonSerializer djs;
                FileStream stream = File.Create("backup.json");
                djs = new DataContractJsonSerializer(typeof(State));
                djs.WriteObject(stream, s);
                
                stream.Close();
                Console.WriteLine("\t\tBackup efetuado com sucesso!");
                
            }

            catch (Exception e)
            {
                Console.Write("\t\t");
                Console.WriteLine(e.Message);
                Console.Write("\t\tPrima qualquer tecla para continuar...");
                Console.ReadLine();

            }

            //-------- BACKUP PARA XML
            try
            {
                DataContractSerializer ds;
                FileStream stream = File.Create("backup.xml");
                ds = new DataContractSerializer(typeof(State));
                ds.WriteObject(stream, s);
                
                stream.Close();
                Console.WriteLine("\t\tBackup efetuado com sucesso!");


            }
            catch (Exception e)
            {
                Console.Write("\t\t");
                Console.WriteLine(e.Message);
                Console.Write("\t\tPrima qualquer tecla para continuar...");
                Console.ReadLine();
            }
            
        }

        public static State Restore()
        {
            // ----- Restore from JSON
            State s = new State();

            if (File.Exists("backup.json"))
            {
                var stream = File.Open("backup.json", FileMode.Open);

                try
                {
                    var ds = new DataContractJsonSerializer(typeof(State));
                    s = (State) ds.ReadObject(stream);

                    stream.Close();
                }
                catch (Exception e)
                {
                    Console.Write("\t\t");
                    Console.WriteLine(e.Message);
                    Console.Write("\t\tPrima qualquer tecla para continuar...");
                    Console.ReadLine();
                }

                Console.WriteLine("\t\tRestore efetuado com sucesso!");
                Console.Write("\t\tPrima uma tecla para continuar...");
                Console.ReadLine();
                return s;
            }
            
            Console.WriteLine("O ficheiro JSON para 'restore' não existe!");
            Console.Write("\t\tPrima uma tecla para continuar...");
            Console.ReadLine();
            
            //---- Restore from TXT

            if (File.Exists("backup.txt"))
            {
                try
                {
                    IFormatter formatador = new BinaryFormatter();
                    Stream stream = new FileStream("backup.txt", FileMode.Open, FileAccess.Read);
                    s = (State) formatador.Deserialize(stream);
                    stream.Close();

                    s.GetShips().ForEach(s => Console.WriteLine(s));
                    s.GetContainers().ForEach(c => Console.WriteLine(c));
                    
                }
                catch (Exception e)
                {
                    Console.Write("\t\t");
                    Console.WriteLine(e.Message);
                    Console.Write("\t\tPrima qualquer tecla para continuar...");
                    Console.ReadLine();
                }
            }
            
            Console.WriteLine("O ficheiro TXT para 'restore' não existe!");
            Console.Write("\t\tPrima uma tecla para continuar...");
            Console.ReadLine();

            //---- Restore from XML
        
            if (File.Exists("backup.xml"))
            {
                var stream = File.Open("backup.xml", FileMode.Open);
                
                try
                {
                    var reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
                    var ds = new DataContractSerializer(typeof(State));
                    
                    s = (State) ds.ReadObject(reader, true);
                    
                    stream.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

                Console.WriteLine("\t\tRestore efetuado com sucesso!");
                Console.Write("\t\tPrima uma tecla para continuar...");
                Console.ReadLine();

            }
                
            Console.WriteLine("O ficheiro para 'restore' não existe!");
            Console.Write("\t\tPrima uma tecla para continuar...");
            Console.ReadLine();

            return s;
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
                Console.Write("\t\tCapacidade total de contentores: ");
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

        public static int ListShipAll(List<Ship> ships)
        {
            int counter = 0;
            
            if (ships.Count == 0)
            {
                Console.WriteLine("\t\tNão existem navios associados ao porto!!");
                Console.Write("\n\n\t\tPrima enter para continuar...");
                Console.ReadLine();
            }
            else
            {
                foreach (var s in ships)
                {
                    Console.WriteLine(s);
                    counter++;
                    RecordsPerPage(counter, 4);
                }
            }

            return ships.Count;
        }

        public static int ListAllShipsWithContainers(List<Ship> ships)
        {
            int counter = 0, shipsWithContainers = 0;
            
            if (ships.Count == 0)
            {
                Console.WriteLine("\t\tNão existem navios associados ao porto!!");
                Console.Write("\n\n\t\tPrima enter para continuar...");
                Console.ReadLine();
            }
            else
            {
                foreach (var s in ships)
                {
                    if (s.GetContainers().Count > 0)
                    {
                        shipsWithContainers++;
                    }
                }

                if (shipsWithContainers == 0)
                {
                    Console.WriteLine("\t\tNão existem navios com contentores!");
                    Console.Write("\n\n\t\tPrima enter para continuar...");
                    Console.ReadLine();
                }
                
                foreach (var s in ships)
                {
                    if (s.GetContainers().Count > 0)
                    {
                        Console.WriteLine(s);
                        counter++;
                        RecordsPerPage(counter, 4);
                    }
                }
            }

            return ships.Count;
        }

        public static int ListShipAtSeaport(List<Ship> ships)
        {
            int counter = 0;

            foreach (var s in ships)
            {
                if (s.GetIsAtPort())
                {
                    counter++;
                }
            }

            if (counter > 0)
            {
                counter = 0;
                foreach (var s in ships)
                {
                    if (s.GetIsAtPort())
                    {
                        Console.WriteLine(s);
                        counter++;
                        RecordsPerPage(counter, 4);

                    }
                }
            }
            else
            {
                Console.WriteLine("\t\tNão existem navios no porto!!");
                Console.Write("\n\n\t\tPrima enter para continuar...");
                Console.ReadLine();
            }

            return counter;
        }

        public static int ListShipAtLarge(List<Ship> ships)
        {
            int counter = 0;

            foreach (var s in ships)
            {
                if (!s.GetIsAtPort())
                {
                    counter++;
                }
            }

            if (counter > 0)
            {
                counter = 0;
                Console.WriteLine();
                foreach (var s in ships)
                {
                    if (!s.GetIsAtPort())
                    {
                        Console.WriteLine(s);
                        counter++;
                        RecordsPerPage(counter, 4);
                    }
                }
            }
            else
            {
                Console.WriteLine("\t\tNão existem navios no porto!!");
                Console.Write("\n\n\t\tPrima enter para continuar...");
                Console.ReadLine();
            }

            return counter;
        }
        
        public static void RemoveShip(List<Ship> ships)
        {
            bool success;
            int option;
            Ship aux;

            if (ListShipAtSeaport(ships) == 0)
            {
                return;
            }
            
            Console.Write("\n\t\tIndique o número do navio: ");

            do
            {
                success = int.TryParse(Console.ReadLine(), out option);
                
                if (success && (ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    success = false;
                    Console.Write("\t\t");


                    ConsoleKeyInfo k;
                    k = Console.ReadKey();
                    
                    if (k.Key == ConsoleKey.S)
                    {
                        return;
                    }
                    Console.WriteLine("\t\t");

                }
                
            } while (!success);
            
            aux = ships.Find(s => s.GetNumber() == option);
            try
            {
                aux.SetIsAtPort(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadLine();
            }
            
            Console.WriteLine("\n\t\tNavio registado como fora do porto!!");
            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadLine();
        }
        
        public static void CallShipToSeaport(List<Ship> ships)
        {
            bool success;
            int option;
            Ship aux;

            if (ListShipAtLarge(ships) == 0)
            {
                return;
            }
            
            Console.Write("\n\t\tIndique o número do navio: ");

            do
            {
                success = int.TryParse(Console.ReadLine(), out option);
                
                if (success && (ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    success = false;
                    Console.Write("\t\t");


                    ConsoleKeyInfo k;
                    k = Console.ReadKey();
                    
                    if (k.Key == ConsoleKey.S)
                    {
                        return;
                    }
                    Console.WriteLine("\t\t");

                }
                
            } while (!success);

            aux = ships.Find(s => s.GetNumber() == option);

            try
            {
                aux.SetIsAtPort(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine("\n\t\tNavio registado como estando no porto!!");
            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadLine();

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
                Console.Write("\t\t");
                k = Console.ReadKey();
                success = (k.Key == ConsoleKey.R || k.Key == ConsoleKey.E || k.Key == ConsoleKey.Q);
                if(!success)
                    Console.WriteLine("\t\tOpção inválida! Escolha (R) Regular, (E) Explosivos ou (Q) Químicos");

            } while (!success);

            switch (k.Key)
            {
                case ConsoleKey.R :
                    Console.Write("\n\t\tDestino: ");
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
                        Console.Write("\t\t");
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
                        Console.WriteLine("\t\tContentor não adicionado");
                        Console.Write("\n\t\tPrima uma tecla para continuar");
                        Console.ReadLine();
                    }
                    else
                    {
                        try
                        {
                            containers.Add(r);
                            Console.WriteLine("\n\t\tContentor adicionado com sucesso!!");
                            Console.Write("\n\t\tPrima uma tecla para continuar");
                            Console.ReadLine();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    break;
                
                case ConsoleKey.E :
                    Console.Write("\n\t\tDestino: ");
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
                        Console.Write("\t\t");
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
                        Console.WriteLine("\t\tContentor não adicionado");
                        Console.Write("\n\t\tPrima uma tecla para continuar...");
                        Console.ReadLine();
                    }
                    else
                    {
                        try
                        {
                            containers.Add(explosive);
                            Console.WriteLine("\t\tContentor adicionado com sucesso!!");
                            Console.Write("\n\t\tPrima uma tecla para continuar...");
                            Console.ReadLine();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    break;
                
                case ConsoleKey.Q:
                    Console.Write("\n\t\tDestino: ");
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
                        Console.Write("\n\t\tPrima uma tecla para continuar...");
                        Console.ReadLine();                    }
                    else
                    {
                        try
                        {
                            containers.Add(chemical);
                            Console.WriteLine("\t\tContentor adicionado com sucesso!!");
                            Console.Write("\n\t\tPrima uma tecla para continuar...");
                            Console.ReadLine();
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
            int counter = 0;
            if (containers.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem contentores no porto!!");
                Console.Write("\n\n\t\tPrima enter para continuar");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine();
                foreach (var c in containers)
                {
                    Console.WriteLine(c);
                    counter++;
                    RecordsPerPage(counter, 3);
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

            if (ListContainers(containers) == 0)
            {
                return;
            }
            
            Console.WriteLine("\n\t\tIndique o número do contentor que quer remover: ");

            do
            {
                Console.Write("\t\t");
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

            try
            {
                containers.Remove(aux);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadLine();
            }
            
            Console.WriteLine("\n\t\tContentor removido com sucesso!!");
            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadLine();
        }

        public static void CountShipsAtSeaport(List<Ship> ships)
        {
            int counter = 0;

            if (ships.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem navios no porto!");
                Console.Write("\n\t\tPrima uma tecla para continuar");
                Console.ReadLine();
            }
            else
            {
                foreach (var ship in ships)
                {
                    if (ship.GetIsAtPort())
                    {
                        counter++;
                    }
                }

                Console.WriteLine("\n\t\tNeste momento encontram-se {0} navios no porto", counter);
                Console.WriteLine("\n\t\tPrima qualquer  tecla para continuar...");
                Console.Write("\t\t");
                Console.ReadLine();
            }
        }
        
        public static void CountShipsAtLarge(List<Ship> ships)
        {
            int counter = 0;

            if (ships.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem navios ao largo!");
                Console.Write("\n\t\tPrima uma tecla para continuar");
                Console.ReadLine();
            }
            else
            {
                foreach (var ship in ships)
                {
                    if (!ship.GetIsAtPort())
                    {
                        counter++;
                    }
                }

                Console.WriteLine("\n\t\tNeste momento encontram-se {0} navios ao largo", counter);
                Console.Write("\n\t\tPrima qualquer  tecla para continuar...");
                Console.ReadLine();
            }
        }
        
        public static void ListContainerNumberAtShip(List<Ship> ships)
        {
            bool success;
            int option;
            Ship aux;
            
            if (ListShipAll(ships) == 0)
            {
                return;
            }

            
            Console.WriteLine("\n\t\tIndique o número do navio: ");

            do
            {
                Console.Write("\t\t");
                success = int.TryParse(Console.ReadLine(), out option);
                
                if (success && (ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    success = false;
                    
                    Console.Write("\t\t");
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
                foreach (var c in aux.GetContainers())
                {
                    Console.Write("\n\t\t");
                    Console.WriteLine(c.GetNumber());
                }
            }
            
            Console.Write("\n\t\tPrima qualquer tecla para continuar...");
            Console.ReadLine();

        }
    
        public static void ListContainersAtShip(List<Ship> ships)
        {
            bool success;
            int option, counter = 0;
            Ship aux;
            
            if (ListAllShipsWithContainers(ships) == 0)
            {
                return;
            }

            
            Console.WriteLine("\n\t\tIndique o número do navio: ");

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

                Console.WriteLine();
                foreach (var c in aux.GetContainers())
                {
                    Console.WriteLine(c);
                    counter++;
                    RecordsPerPage(counter, 3);
                }
            Console.Write("\t\tPrima qualquer tecla para continuar...");
            Console.ReadLine();

        }

        public static void AddContainerToShip(List<Ship> ships, List<Container> containers)
        {
            bool success;
            int option;
            string numContainer;
            Container auxContainer;
            Ship auxShip;

            if (UnassignedContainers(containers) == 0)
            {
                return;
            }
            
            Console.WriteLine("\n\t\tIndique o número do contentor: ");

            do
            {
                Console.Write("\t\t");
                numContainer = Console.ReadLine();
                
                if (containers.Find(c => c.GetNumber().Equals(numContainer)) == null)
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    Console.Write("\t\t");
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

            ListShipAtSeaport(ships);
            Console.WriteLine("\n\t\tIndique o número do navio: ");

            do
            {
                Console.Write("\t\t");
                success = int.TryParse(Console.ReadLine(), out option);
                
                if (success && (ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    Console.Write("\t\t");
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
                Console.Write("\n\t\tPrima um tecla para continuar");
                Console.ReadLine();
            }
            catch (MaxContainersException e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\n\t\tPrima um tecla para continuar");
                Console.ReadLine();
            }
            
            catch (MaxExplosiveException e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\n\t\tPrima um tecla para continuar");
                Console.ReadLine();
            }
            
            catch (MaxChemicalException e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\n\t\tPrima um tecla para continuar");
                Console.ReadLine();
            }
            
        }

        public static void RemoveContainerFromShip(List<Ship> ships, List<Container> containers)
        {
            bool success;
            int option;
            string numContainer;
            Container auxContainer;
            Ship auxShip;
            
            if (ListAllShipsWithContainers(ships) == 0)
            {
                return;
            }
            
            Console.WriteLine("\n\t\tIndique o número do navio: ");

            do
            {
                Console.Write("\t\t");
                success = int.TryParse(Console.ReadLine(), out option);
                
                if (success && (ships.Find(s => s.GetNumber() == option) == null))
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    Console.Write("\t\t");
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
                    Console.Write("\t\tPrima uma tecla para continuar...");
                    Console.ReadLine();
                    auxShip.ListContainers();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.Write("\t\tPrima uma tecla para continuar...");
                    Console.ReadLine();
                }
            }
            
            Console.WriteLine("\n\t\tIndique o número do contentor: ");

            do
            {
                Console.Write("\t\t");
                numContainer = Console.ReadLine();
                
                if (auxShip.GetContainers().Find(c => c.GetNumber().Equals(numContainer)) == null)
                {
                    Console.WriteLine("\t\tO número que indicou não existe, insira novo número ou prima S para Sair");
                    Console.Write("\t\t");
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

        public static int UnassignedContainers(List<Container> containers)
        {
            int counter = 0;
            
            if (containers.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem contentores associados ao porto!");
                Console.Write("\n\t\tPrima qualquer tecla para continuar...");
                Console.ReadLine();
                
                return counter;
            }

            foreach (var c in containers)
            {
                if (c.GetShipNumber() == -1)
                {
                    counter++;
                }
            }

            if (counter > 0)
            {
                foreach (var c in containers.Where(c => c.GetShipNumber() == -1))
                {
                    Console.WriteLine();
                    Console.WriteLine(c);
                }
            }
            else
            {
                Console.WriteLine("\n\t\tTodos os contentores estão atribuídos!");
                Console.Write("\n\t\tPrima qualquer tecla para continuar...");
                Console.ReadLine();
            
            }

            return counter;
        }

        public static void RecordsPerPage(int numberOfRecords, int recordsPerPage)
        {
            if (numberOfRecords % recordsPerPage == 0)
            {
                Console.Write("\n\t\tPrima uma tecla para mais registos... ");
                Console.ReadLine();
                Console.WriteLine("\t\t-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-");
            }
        }
    }
}