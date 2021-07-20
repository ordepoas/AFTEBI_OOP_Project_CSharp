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

        public string GetType()
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
            s += "\n\t\t\t------------------";
            s += "\n\t\t\tCarga Química";
            s += "\n\t\t\tDescrição do químico: " + _type;
            s += "\n\t\t---------------------------------------------------------------------------------------------";

            return s;
        }
        /*
        public override bool Equals(object obj)
        {
            if (!(obj is Chemical e)) return false;
            bool result = _type.Equals(e._type);

            return result;
        }
        */
    }
}