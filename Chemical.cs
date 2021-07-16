using System;

namespace OOP_Project
{
    [Serializable]
    public class Chemical : Container
    {
        private string _type;
        
        public Chemical(){}

        public Chemical(string type, string destination, int weight) : base(
            destination, weight)
        {
            _type = type;
        }
        
        public Chemical(string type, int shipNumber, string destination, int weight) : base(shipNumber,destination, weight)
        {
            _type = type;
        }
    }
}