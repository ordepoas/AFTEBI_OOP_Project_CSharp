using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace OOP_Project
{
    public class Methods
    {
        //---- Métodos para backup em txt, JSON e XML
        public static void Backup(Seaport s)
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
                djs = new DataContractJsonSerializer(typeof(Seaport));
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
                ds = new DataContractSerializer(typeof(Seaport));
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

        //---- Métodos para restores de ficheiros em txt, JSON e XML
        //só faz o restore do ficheiro do tipo seguinte se o anterior não funcionar
        public static Seaport Restore()
        {
            // ----- Restore from JSON
            Seaport s = new Seaport();

            if (File.Exists("backup.json"))
            {
                var stream = File.Open("backup.json", FileMode.Open);

                try
                {
                    var ds = new DataContractJsonSerializer(typeof(Seaport));
                    s = (Seaport) ds.ReadObject(stream);

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
                    s = (Seaport) formatador.Deserialize(stream);
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
                    var ds = new DataContractSerializer(typeof(Seaport));

                    s = (Seaport) ds.ReadObject(reader, true);

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

        //---- Método para adicionar novos navios, o numero de navio é atribuido
        //automaticamente e o navio fica adicionado ao porto
        public static void AddShip(Seaport s)
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

            Console.Write("\t\tBandeira: ");
            flag = Console.ReadLine();

            Ship auxShip = new Ship(name, maxContainers, maxExplosive, maxChemical, flag);

            if (s.Ships.Find(x => x.GetNumber() == auxShip.GetNumber()) != null)
            {
                Console.WriteLine("\t\tEste navio já existe!");
                Console.WriteLine("\n\t\tNavio não adicionado!");
                Console.WriteLine("\n\t\tPrima um enter para continuar");
                Console.Write("\t\t");
                Console.ReadLine();
            }
            else
            {
                try
                {
                    s.Ships.Add(auxShip);
                    Console.WriteLine("\t\tNavio adicionado com sucesso!!");
                    Console.WriteLine("\n\t\tPrima um enter para continuar");
                    Console.Write("\t\t");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("\n\t\tPrima um enter para continuar");
                    Console.Write("\t\t");
                    Console.ReadLine();
                }
            }
        }

        //---- Método para listar todos os navios e reutilizado noutros métodos
        public static int ListShipAll(Seaport s)
        {
            int counter = 0;

            if (s.Ships.Count == 0)
            {
                Console.WriteLine("\t\tNão existem navios associados ao porto!!");
                Console.Write("\n\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(
                    "\t\t===================================== LISTA DE NAVIOS =====================================");
                foreach (var ship in s.Ships)
                {
                    Console.WriteLine(ship);
                    counter++;
                    if (s.Ships.Count != counter)
                    {
                        RecordsPerPage(counter, 4);
                    }
                }
            }

            return s.Ships.Count;
        }

        //---- Método para listar todos os navios com contentores e reutilizado noutros métodos
        public static int ListAllShipsWithContainers(Seaport s)
        {
            int counter = 0, shipsWithContainers = 0;

            if (s.Ships.Count == 0)
            {
                Console.WriteLine("\t\tNão existem navios associados ao porto!!");
                Console.Write("\n\n\t\tPrima enter para continuar...");
                Console.ReadLine();

                return shipsWithContainers;
            }
            else
                Console.WriteLine(
                    "\t\t===================================== LISTA DE NAVIOS =====================================");

            {
                foreach (var ship in s.Ships)
                {
                    if (ship.GetContainers().Count > 0)
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

                foreach (var ship in s.Ships)
                {
                    if (ship.GetContainers().Count > 0)
                    {
                        Console.WriteLine(s);
                        counter++;

                        if (shipsWithContainers != counter)
                            RecordsPerPage(counter, 4);
                    }
                }
            }

            return shipsWithContainers;
        }

        //Método para listar os navios que estão no porto
        public static int ListShipAtSeaport(Seaport s)
        {
            int counter = 0, shipsAtPort;

            foreach (var ship in s.Ships)
            {
                if (ship.GetIsAtPort())
                {
                    counter++;
                }
            }

            shipsAtPort = counter;

            if (shipsAtPort > 0)
            {
                counter = 0;
                Console.WriteLine(
                    "\t\t===================================== LISTA DE NAVIOS =====================================");
                foreach (var ship in s.Ships)
                {
                    if (ship.GetIsAtPort())
                    {
                        Console.WriteLine(s);
                        counter++;
                        if (shipsAtPort != counter)
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
                Console.WriteLine(
                    "\t\t===================================== LISTA DE NAVIOS =====================================");
                foreach (var ship in s.Ships)
                {
                    if (!ship.GetIsAtPort())
                    {
                        Console.WriteLine(s);
                        counter++;
                        if (shipsAtLarge != counter)
                            RecordsPerPage(counter, 4);
                    }
                }
            }
            else
            {
                Console.WriteLine("\t\tNão existem navios ao largo!");
                Console.Write("\n\n\t\tPrima enter para continuar...");
                Console.ReadLine();
            }

            return counter;
        }

        //---- Método para remover navios
        public static void RemoveShip(Seaport s)
        {
            bool success;
            int option;
            Ship aux;

            option = ListShipAtSeaport(s);
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
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadLine();
            }

            Console.WriteLine("\n\t\tNavio registado como fora do porto!!");
            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadLine();
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
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\n\t\tNavio registado como estando no porto!!");
            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadLine();

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
                        Console.Write("\n\t\tPrima uma tecla para continuar");
                        Console.ReadLine();
                    }
                    else
                    {
                        try
                        {
                            s.Containers.Add(r);
                            Console.WriteLine("\n\t\tContentor adicionado com sucesso!!");
                            Console.Write("\n\t\tPrima uma tecla para continuar");
                            Console.ReadLine();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.Write("\n\t\tPrima uma tecla para continuar");
                            Console.ReadLine();
                        }
                    }

                    break;

                case ConsoleKey.E:
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
                        Console.Write("\n\t\tPrima uma tecla para continuar...");
                        Console.ReadLine();
                    }
                    else
                    {
                        try
                        {
                            s.Containers.Add(explosive);
                            Console.WriteLine("\n\t\tContentor adicionado com sucesso!!");
                            Console.Write("\n\t\tPrima uma tecla para continuar...");
                            Console.ReadLine();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.Write("\n\t\tPrima uma tecla para continuar");
                            Console.ReadLine();
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
                    if (s.Containers.Contains(chemical))
                    {
                        Console.WriteLine("\t\tEste contentor já existe!!");
                        Console.WriteLine("\n\t\tContentor não adicionado");
                        Console.Write("\n\t\tPrima uma tecla para continuar...");
                        Console.ReadLine();
                    }
                    else
                    {
                        try
                        {
                            s.Containers.Add(chemical);
                            Console.WriteLine("\n\t\tContentor adicionado com sucesso!!");
                            Console.Write("\n\t\tPrima uma tecla para continuar...");
                            Console.ReadLine();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.Write("\n\t\tPrima uma tecla para continuar");
                            Console.ReadLine();

                        }
                    }

                    break;
            }
        }

        //--- Método para listar contentores e reutilizado noutros métodos
        public static int ListContainers(Seaport s)
        {
            int counter = 0;
            if (s.Containers.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem contentores no porto!!");
                Console.Write("\n\n\t\tPrima uma tecla para continuar");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine(
                    "\t\t===================================== LISTA DE CONTENTORES =====================================");
                foreach (var c in s.Containers)
                {

                    Console.WriteLine(c);
                    counter++;
                    if (s.Containers.Count != counter)
                        RecordsPerPage(counter, 3);
                }

                Console.Write("\n\n\t\tPrima uma tecla para continuar");
                Console.ReadLine();

            }

            return s.Containers.Count;
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
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadLine();
            }

            Console.WriteLine("\n\t\tContentor removido com sucesso!!");
            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadLine();
        }

        //---- Método para contar os navios no porto
        public static void CountShipsAtSeaport(Seaport s)
        {
            int counter = 0;

            if (s.Ships.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem navios no porto!");
                Console.Write("\n\t\tPrima uma tecla para continuar");
                Console.ReadLine();
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
                Console.WriteLine("\n\t\tPrima qualquer  tecla para continuar...");
                Console.Write("\t\t");
                Console.ReadLine();
            }
        }

        //---- Método para contar os navios ao largo
        public static void CountShipsAtLarge(Seaport s)
        {
            int counter = 0;

            if (s.Ships.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem navios ao largo!");
                Console.Write("\n\t\tPrima uma tecla para continuar");
                Console.ReadLine();
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
                Console.Write("\n\t\tPrima qualquer  tecla para continuar...");
                Console.ReadLine();
            }
        }

        //---- Método que lista os números dos contentores existentes num navio
        public static void ListContainerNumberAtShip(Seaport s)
        {
            bool success;
            int option;
            Ship aux;

            option = ListAllShipsWithContainers(s);
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

            Console.Write("\n\t\tPrima qualquer tecla para continuar...");
            Console.ReadLine();

        }

        //---- Método que lista com detalhe os contentores existentes num navio
        public static void ListContainersAtShip(Seaport s)
        {
            bool success;
            int option, counter = 0;
            Ship aux;

            option = ListAllShipsWithContainers(s);
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

            Console.WriteLine(
                "\t\t===================================== LISTA DE CONTENTORES =====================================");

            foreach (var c in aux.GetContainers())
            {
                Console.WriteLine(c);
                counter++;
                if (aux.GetContainers().Count != counter)
                    RecordsPerPage(counter, 3);
            }

            Console.Write("\t\tPrima qualquer tecla para continuar...");
            Console.ReadLine();

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

            option = ListShipAtSeaport(s);
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

        //---- Método para remover contentor de um navio
        public static void RemoveContainerFromShip(Seaport s)
        {
            bool success;
            int option;
            string numContainer;
            Container auxContainer;
            Ship auxShip;

            option = ListAllShipsWithContainers(s);
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
                Console.Write("\n\t\tPrima uma tecla para continuar");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\t\tPrima uma tecla para continuar...");
                Console.ReadLine();

            }

        }

        //---- Método para listar os contentoes sem navio atribuido
        public static int UnassignedContainers(Seaport s)
        {
            int counter = 0, unassignedContainersCount;

            if (s.Containers.Count == 0)
            {
                Console.WriteLine("\n\t\tNão existem contentores associados ao porto!");
                Console.Write("\n\t\tPrima qualquer tecla para continuar...");
                Console.ReadLine();

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
                Console.WriteLine(
                    "\t\t===================================== LISTA DE CONTENTORES =====================================");
                counter = 0;
                foreach (var c in s.Containers.Where(c => c.GetShipNumber() == -1))
                {
                    Console.WriteLine(c);
                    counter++;
                    if (unassignedContainersCount != counter)
                        RecordsPerPage(counter, 3);
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

        //---- Método auxiliar para fazer break na apresentação de listas de registos
        //de acordo com número de registos indicado
        public static void RecordsPerPage(int numberOfRecords, int recordsPerPage)
        {
            if (numberOfRecords % recordsPerPage == 0)
            {
                Console.Write("\n\t\tPrima uma Enter para mais registos... ");
                Console.ReadLine();
                Console.WriteLine(
                    "\n\t\t*********************************************************************************************");
            }
        }

        //Função adicional consulta o IPMA para informar o tempo nos próximos 5 dias
        public static void GetWeather()
        {
            try
            {
                WebRequest request =
                    HttpWebRequest.Create(
                        "http://api.ipma.pt/open-data/forecast/meteorology/cities/daily/1050200.json");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string strJson = reader.ReadToEnd();

                Weather weather = JsonConvert.DeserializeObject<Weather>(strJson);


                //int counter = 0;
                DateTime today = DateTime.Today;
                Console.WriteLine();
                Console.WriteLine("\t\t---- Distrito de Castelo Branco ----");
                foreach (var day in weather.data)
                {
                    if (day.forecastDate.Equals(today.ToString("yyyy-MM-dd")))
                    {
                        Console.WriteLine(day);
                        /*
                        counter++;
                        if (weather.data.Count != counter)
                            Methods.RecordsPerPage(counter, 5);
                        */
                    }   
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();

            }
            /*
            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
            */
        }
        
        // ---- Método de apoio ao GetWeater();
        public static WeatherType GetWeatherType()
        {
            try
            {
                WebRequest request = HttpWebRequest.Create("https://api.ipma.pt/open-data/weather-type-classe.json");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string strJson = reader.ReadToEnd();

                WeatherType weatherType = JsonConvert.DeserializeObject<WeatherType>(strJson);
                return weatherType;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();

            }

            return null;
        }

        //Função adicional ao programa, apresenta os principais titulos de noticias do dia.
        public static void GetNews()
        {
            try
            {
                WebRequest request = HttpWebRequest.Create("https://observador.pt/wp-json/obs_api/v4/news/widget");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string strJson = reader.ReadToEnd();

                List<News> latestestNews = JsonConvert.DeserializeObject<List<News>>(strJson);

                int counter = 0;
                Console.WriteLine();
                Console.WriteLine("\t\t--------------------------------- Últimas Notícias ---------------------------------");
                foreach (var news in latestestNews)
                {
                    Console.WriteLine(news);
                    counter++;
                    if(latestestNews.Count != counter)
                        Methods.RecordsPerPage(counter, 4);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();

            }


            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
        }

    }
}