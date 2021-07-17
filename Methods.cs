using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace OOP_Project
{
    public class Methods
    {
        public static void Backup(State s)
        {
            /*
            IFormatter formatador = new BinaryFormatter();
            Stream stream = new FileStream("Estado.txt", FileMode.Create, FileAccess.Write);

            formatador.Serialize(stream, s);
            stream.Close();
            */
            //write JSON File
            /*
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(s));
            MemoryStream msObj = new MemoryStream();
            js.WriteObject(msObj, s);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);
            
            string json = sr.ReadToEnd();

            sr.Close();
            msObj.Close();            
            */
            
            //write JSON File -------------------- THIS WORKS
            /*
            DataContractJsonSerializer data = new DataContractJsonSerializer(typeof(State));
            MemoryStream memory = new MemoryStream();
            data.WriteObject(memory, s);
            memory.Position = 0;

            using (FileStream stream = new FileStream("backup.json", FileMode.Open))
            {
                memory.CopyTo(stream);
                stream.Flush();
            }

            memory.Position = 0;
            StreamReader streamReader = new StreamReader(memory);
            Console.WriteLine("JSON: " + streamReader.ReadToEnd());
            streamReader.Close();
            memory.Close(); 
            */
            
            //-------- CREATE JSON
            try
            {
                var memoryStream = new MemoryStream();
                var data = new DataContractJsonSerializer(typeof(State));

                data.WriteObject(memoryStream, s);
                memoryStream.Position = 0;

                FileStream filestream = File.Create("backup.json");
                memoryStream.CopyTo(filestream);
                filestream.Flush();


                memoryStream.Close();
                filestream.Close();
                
                Console.WriteLine("\t\tBackup efetuado com sucesso!");

            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //-------- CREATE XML
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
                Console.WriteLine(e.Message);
            }
            
        }
        
        public static State Restore()
        {
            /*
            string fileName = "portState.json";
            string jsonString = File.ReadAllText(fileName);
            State sJson = JsonSerializer.Deserialize<State>(jsonString);
            */
            
            /*
            State obj = new State();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer((typeof(State)));
            using (FileStream fs = new FileStream("backup.json", FileMode.Open))
            {
                using (XmlDictionaryReader jsonr = JsonReaderWriterFactory.CreateJsonReader(fs, Encoding.GetEncoding("utf-8"),XmlDictionaryReaderQuotas.Max,null))
                {
                    obj = (State) serializer.ReadObject(jsonr);
                }
            }
            return obj;
            */
            /*
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
            */
            /*
            using (StreamReader file = File.OpenText(@"c:\videogames.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject) JToken.ReadFrom(reader);
            }
            */
            /*
            State s;
            
            using (StreamReader r = new StreamReader("backup.json"))
            {
                string json = r.ReadToEnd();
                s = JsonConvert.DeserializeObject<State>(json);
            }*/
            
            State s = new State();
            
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
            else
            {
                Console.WriteLine("O ficheiro para 'restore' não existe!");
                Console.Write("\t\tPrima uma tecla para continuar...");
                Console.ReadLine();

            }


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
                foreach (var s in ships)
                {
                    if (s.GetIsAtPort())
                    {
                        Console.WriteLine(s);
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
                foreach (var s in ships)
                {
                    if (!s.GetIsAtPort())
                    {
                        Console.WriteLine();
                        Console.WriteLine(s);
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

            aux.SetIsAtPort(false);
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

            aux.SetIsAtPort(true);
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
                        Console.WriteLine("\n\t\tPrima uma tecla para continuar");
                        Console.ReadLine();
                    }
                    else
                    {
                        try
                        {
                            containers.Add(r);
                            Console.WriteLine("\n\t\tContentor adicionado com sucesso!!");
                            Console.WriteLine("\n\t\tPrima uma tecla para continuar");
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
            if (containers.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem contentores no porto!!");
                Console.Write("\n\n\t\tPrima enter para continuar");
                Console.ReadLine();
            }
            else
            {
                foreach (var c in containers)
                {
                    Console.WriteLine();
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

            containers.Remove(aux);
            Console.WriteLine("\n\t\tContentor removido com sucesso!!");
            Console.WriteLine();
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
            int option, counter;
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
                foreach (var c in aux.GetContainers())
                {
                    Console.WriteLine("\n");
                    Console.WriteLine(c);
                }
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
            
            if (ListShipAtSeaport(ships) == 0)
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

            if (auxShip.ListContainers() == 0)
            {
                return;
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
                Console.Write("\n\t\tPrima qualquer tecla para continuar...");
                Console.ReadLine();
                
            }
            else
            {
                Console.WriteLine("\n\t\tTodos os contentores estão atribuídos!");
                Console.Write("\n\t\tPrima qualquer tecla para continuar...");
                Console.ReadLine();
            
            }

            return counter;
        }
    }
}