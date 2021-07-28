using System;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;

namespace OOP_Project
{
    [Serializable][DataContract (Name = "Contentor")]
    [KnownType(typeof(Regular))]
    [KnownType(typeof(Explosive))]
    [KnownType(typeof(Chemical))]
    
    public abstract class Container //---- super-classe de contentores
    {
        [DataMember (Name = "Numero Navio")]
        protected int ShipNumber;
        [DataMember (Name = "Numero")]
        protected string Number;
        [DataMember (Name = "Destino")]
        protected string Destination;
        [DataMember (Name = "Peso")]
        protected int Weight;
        
        public int GetShipNumber()
        {
            return ShipNumber;
        }

        public void SetShipNumber(int shipNumber)
        {
            ShipNumber = shipNumber;
        }

        public string GetNumber()
        {
            return Number;
        }

        public void SetNumber(string number)
        {
            Number = number;
        }

        public string GetDestination()
        {
            return Destination;
        }

        public void SetDestination(string destination)
        {
            Destination = destination;
        }

        public int GetWeight()
        {
            return Weight;
        }

        public void SetWeight(int weight)
        {
            Weight = weight;
        }

        protected string ContainerNumber()
        {
            char a, b, c, d;
            int num1, num2;
            string cNumb;

            var random = new Random();
            a = (char)random.Next(65, 90);
            b = (char)random.Next(65, 90);
            c = (char)random.Next(65, 90);
            d = (char)random.Next(65, 90);
            num1 = random.Next(100000, 999999);
            num2 = random.Next(1, 9);

            cNumb = a.ToString() + b.ToString() + c.ToString() + d.ToString() + " " + num1 + " " + num2;

            return cNumb;

        }

        public override string ToString()
        {
            string s;
            s = "\t\tNúmero de contentor: " + Number + "\t Destino: " + Destination + "\t Peso: " + Weight;
            if (ShipNumber != -1)
            {
                s += "\n\t\tNavio: " + ShipNumber;
            }
            s += "\n\t\t\t------------------";
            return s;
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Container c)) return false;

            return Number == c.Number;
        }

    }
}