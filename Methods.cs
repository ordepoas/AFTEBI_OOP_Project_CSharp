using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace OOP_Project
{
    public class Methods
    {
        public static void Backup(List<State> estado)
        {
            IFormatter formatador = new BinaryFormatter();
            Stream stream = new FileStream("Estado.txt", FileMode.Create, FileAccess.Write);

            formatador.Serialize(stream, estado);
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

        public static void addShip(List<Ship> ships)
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

            if (ships.Contains(s))
            {
                Console.WriteLine("\t\tEste navio já existe!");
            }

            try
            {
                ships.Add(s);
                Console.WriteLine("\t\tNavio adicionado com sucesso!!");
                Console.WriteLine("\n\t\tPrima um enter para continuar");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}