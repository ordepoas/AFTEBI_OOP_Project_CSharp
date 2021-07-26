using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OOP_Project
{
    [Serializable][DataContract (Name = "Estado")]
    public class Seaport //---- classe para instanciar um objeto que recolha a informação do porto em duas listas  
                        // uma de navios e uma de contentores
    {
        [DataMember (Name = "Navios")]
        public List<Ship> Ships;
        [DataMember (Name = "Contentores")]
        public List<Container> Containers;

        public Seaport()
        {
            Ships = new List<Ship>();
            Containers = new List<Container>();
        }

        public Seaport(List<Ship> ships, List<Container> containers)
        {
            Ships = ships;
            Containers = containers;
        }

        public List<Ship> GetShips()
        {
            return Ships;
        }

        public void SetShips(List<Ship> ships)
        {
            Ships = ships;
        }
        
        public List<Container> GetContainers()
        {
            return Containers;
        }

        public void SetContainers(List<Container> containers)
        {
            Containers = containers;
        }

        public override string ToString()
        {
            string s = Ships.ToString();
            string c = Containers.ToString();

            return s + c;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Seaport e)) return false;

            bool result =
                Ships.Equals(e.Ships) &&
                Containers.Equals(e.Containers);

            return result;
        }
    }
}