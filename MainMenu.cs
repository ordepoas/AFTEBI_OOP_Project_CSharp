using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Xml;
using Newtonsoft.Json;

namespace OOP_Project
{
    public class MainMenu
    {
        //---- Método para apresentação do menu de restauro do estado do porto devolve um int com a opção escolhida
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
            Console.WriteLine();
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
        public static int Menu(Seaport s)
        {
            bool success;
            int option;
            
            Console.Clear();
            Console.WriteLine();
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
                        option = MenuShip.Menu(s);
                        success = (option == 0) || (option == 9);

                    } while (!success);

                    break;
                //Acesso ao Menu GERIR CONTENTORES
                case 2 :
                    do
                    {
                        option = MenuContainer.Menu(s);
                        success = (option == 0) || (option == 9);

                    } while (!success);
                    break;
                case 9 :
                    Console.Clear();
                    GetWeather();
                    GetNews();
                    break;
            }

            return option;
        }

        //---- Métodos para backup em TXT, JSON e XML
        public static void Backup(Seaport s)
        {
            //------- BACKUP PARA TXT
            IFormatter formatador = new BinaryFormatter();
            Stream streamTxt = new FileStream("backup.txt", FileMode.Create, FileAccess.Write);

            try
            {
                formatador.Serialize(streamTxt, s);

                Console.WriteLine("\t\tBackup efetuado com sucesso!");

            }
            catch (IOException e)
            {
                Console.Write("\t\t");
                Console.WriteLine("Error: " + e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\tPrima qualquer tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();

            }
            finally
            {
                streamTxt.Close();
            }

            //-------- BACKUP PARA JSON
            DataContractJsonSerializer djs;
            FileStream stream = File.Create("backup.json");

            try
            {

                djs = new DataContractJsonSerializer(typeof(Seaport));
                djs.WriteObject(stream, s);

                Console.WriteLine("\t\tBackup efetuado com sucesso!");

            }

            catch (IOException e)
            {
                Console.Write("\t\t");
                Console.WriteLine("Error: " + e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\tPrima qualquer tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();

            }
            finally
            {
                stream.Close();
            }

            //-------- BACKUP PARA XML
            DataContractSerializer ds;
            FileStream streamXml = File.Create("backup.xml");
            try
            {
                ds = new DataContractSerializer(typeof(Seaport));
                ds.WriteObject(streamXml, s);

                Console.WriteLine("\t\tBackup efetuado com sucesso!");


            }
            catch (IOException e)
            {
                Console.Write("\t\t");
                Console.WriteLine("Error: " + e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\tPrima qualquer tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();
            }
            finally
            {
                streamXml.Close();

            }

        }

        //---- Métodos para restores de ficheiros em TXT, JSON e XML
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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\t\tPrima qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.ResetColor();
                }

                Console.WriteLine("\t\tRestore efetuado com sucesso!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();
                return s;
            }

            Console.WriteLine("O ficheiro JSON para 'restore' não existe!");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
            Console.ResetColor();

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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\t\tPrima qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.ResetColor();
                }
            }

            Console.WriteLine("O ficheiro TXT para 'restore' não existe!");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
            Console.ResetColor();

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
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();

            }

            Console.WriteLine("O ficheiro para 'restore' não existe!");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
            Console.ResetColor();

            return s;
        }
        
        //---- Método para listar todos os navios com contentores e reutilizado noutros métodos
        public static int ListAllShipsWithContainers(Seaport s)
        {
            int counter = 0, shipsWithContainers = 0;

            if (s.Ships.Count == 0)
            {
                Console.WriteLine("\t\tNão existem navios associados ao porto!!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();

                return shipsWithContainers;
            }
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(
                "\t\t===================================== LISTA DE NAVIOS =====================================\t\t");
            Console.ResetColor();
            
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
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();
            }

            foreach (var ship in s.Ships)
            {
                if (ship.GetContainers().Count > 0)
                {
                    Console.WriteLine(ship);
                    counter++;

                    if (shipsWithContainers != counter)
                        RecordsPerPage(counter, 4);
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
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(
                    "\t\t===================================== LISTA DE NAVIOS =====================================\t\t");
                Console.ResetColor();
                
                foreach (var ship in s.Ships.Where(ship => ship.GetIsAtPort()))
                {
                    Console.WriteLine(ship);
                    counter++;
                    if (shipsAtPort != counter)
                        RecordsPerPage(counter, 4);
                }
            }
            else
            {
                Console.WriteLine("\t\tNão existem navios no porto!!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();
            }

            return counter;
        }
        
        //---- Método auxiliar para fazer break na apresentação de listas de registos
        //de acordo com número de registos indicado
        public static void RecordsPerPage(int numberOfRecords, int recordsPerPage)
        {
            if (numberOfRecords % recordsPerPage == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para mais registos... ");
                Console.ReadKey();
                Console.ResetColor();
                Console.WriteLine(
                    "\n\t\t_____________________________________________________________________________________________");
            }
        }

        //Função adicional consulta o IPMA para informar o tempo de 5 dias (só apresenta o primeiro registo)
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

                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\t\t---- Distrito de Castelo Branco ----\t\t");
                Console.ResetColor();
                
                Console.WriteLine(weather.data[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();

            }
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
                response.Close();

                WeatherType weatherType = JsonConvert.DeserializeObject<WeatherType>(strJson);
                return weatherType;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();
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
                response.Close();

                List<News> latestestNews = JsonConvert.DeserializeObject<List<News>>(strJson);

                int counter = 0;
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\t\t--------------------------------- Últimas Notícias ---------------------------------\t\t");
                Console.ResetColor();
                foreach (var news in latestestNews)
                {
                    Console.WriteLine(news);
                    counter++;
                    if(latestestNews.Count != counter)
                        RecordsPerPage(counter, 3);
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();
                Console.ResetColor();

            }
        }
    }
}