using System;
using System.Runtime.InteropServices.ComTypes;

namespace OOP_Project
{
    [Serializable]
    public abstract class Container
    {
        protected int ShipNumber;
        protected string Number;
        protected string Destination;
        protected int Weight;
        
        public Container(){}

        public Container( string destination, int weight)
        {
            ShipNumber = -1;
            Number = ContainerNumber();
            Destination = destination;
            Weight = weight;
        }

        public Container(int shipNumber, string destination, int weight)
        {
            ShipNumber = shipNumber;
            Number = ContainerNumber();
            Destination = destination;
            Weight = weight;
        }

        public int? GetShipNumber()
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

        public override string ToString()
        {
            string s;
            if (ShipNumber != null)
            {
                s = "\t\tNavio: " + ShipNumber;
                s += "\n\t\tNúmero de contentor: " + Number;
                s += "\n\t\tDestino: " + Destination + "\tPeso: " + Weight;
                s += "\n\t\t-------------------------------------------------\n";

                return s;
            }
            else
            {
                s = "\t\tNúmero de contentor: " + Number;
                s += "\n\t\tDestino: " + Destination + "\tPeso: " + Weight;
                s += "\n\t\t-------------------------------------------------\n";

                return s;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Container c)) return false;
            var result =
                Number == c.Number &&
                Destination.Equals(c.Destination) &&
                Weight == c.Weight;

            return result;
        }

        public string ContainerNumber()
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

            cNumb = a + b + c + d + " " + num1 + " " + num2;

            return cNumb;

        }
    }
}