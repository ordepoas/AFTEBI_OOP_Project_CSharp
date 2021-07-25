using System;
using System.Runtime.Serialization;

namespace OOP_Project
{
    [Serializable][DataContract (Name = "Quimico")]
    public class Chemical : Container //sub-classe de contentores 
    {
        [DataMember (Name = "Tipo Quimico")]
        private string _type;
        
        public Chemical(){}

        public Chemical(string type, string destination, int weight)
        {
            ShipNumber = -1;
            Number = ContainerNumber();
            Destination = destination;
            Weight = weight;
            _type = type;
        }
        
        public Chemical(string type, int shipNumber, string destination, int weight)
        {
            ShipNumber = shipNumber;
            Number = ContainerNumber();
            Destination = destination;
            Weight = weight;
            _type = type;
        }

        public string GetNewType()
        {
            return _type;
        }

        public void SetType(string type)
        {
            _type = type;
        }
        
        public override string ToString()
        {
            var s = base.ToString();
            s += "\n\t\t\tCarga Química";
            s += "\n\t\t\tDescrição do químico: " + _type;
            s += "\n\t\t---------------------------------------------------------------------------------------------";

            return s;
        }

    }
}