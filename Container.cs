using System;
using System.Runtime.InteropServices.ComTypes;

namespace OOP_Project
{
    [Serializable]
    public class Container
    {
        protected int? ShipNumber;
        protected int Number;
        protected string Destination;
        protected int Weight;
        
        public Container(){}

        public Container(int number, string destination, int weight)
        {
            ShipNumber = null;
            Number = number;
            Destination = destination;
            Weight = weight;
        }

        public Container(int shipNumber, int number, string destination, int weight)
        {
            ShipNumber = shipNumber;
            Number = number;
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

        public int GetNumber()
        {
            return Number;
        }

        public void SetNumber(int number)
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
                ShipNumber == c.ShipNumber &&
                Number == c.Number &&
                Destination.Equals(c.Destination) &&
                Weight == c.Weight;

            return result;
        }
    }
}