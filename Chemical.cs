using System;
using System.Runtime.Serialization;

namespace OOP_Project
{
    [Serializable][DataContract (Name = "Quimico")]
    public class Chemical : Container
    {
        [DataMember (Name = "Tipo Quimico")]
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