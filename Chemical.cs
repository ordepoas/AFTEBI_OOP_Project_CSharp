using System;

namespace OOP_Project
{
    [Serializable]
    public class Chemical : Container
    {
        private string _type;
        
        public Chemical(){}

        public Chemical(string type, string number, string destination, int weight) : base(
            number, destination, weight)
        {
            _type = type;
        }
        
        public Chemical(string type, int shipNumber, string number, string destination, int weight) : base(shipNumber,
            number, destination, weight)
        {
            _type = type;
        }
    }
}