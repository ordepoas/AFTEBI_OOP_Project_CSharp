using System;
using System.Runtime.Serialization;

namespace OOP_Project
{
    [Serializable][DataContract (Name = "Explosivo")]
    public class Explosive : Container //---- subclasse de contentores
    {
        [DataMember (Name = "Tipo Explosivo")]
        private string _type;
        [DataMember (Name = "É plastico")]
        private bool _isPlasticExplosive;
        
        public Explosive(){}

        public Explosive(string type, bool isPlasticExplosive, string destination,
            int weight)
        {            
            ShipNumber = -1;
            Number = ContainerNumber();
            Destination = destination;
            Weight = weight;
            _type = type;
            _isPlasticExplosive = isPlasticExplosive;
        }

        public Explosive(string type, bool isPlasticExplosive, int shipNumber, string destination,
            int weight)
        {
            ShipNumber = shipNumber;
            Number = ContainerNumber();
            Destination = destination;
            Weight = weight;
            _type = type;
            _isPlasticExplosive = isPlasticExplosive;
        }

        public string GetNewType()
        {
            return _type;
        }

        public void SetType(string type)
        {
            _type = type;
        }

        public bool GetIsPlasticExplosive()
        {
            return _isPlasticExplosive;
        }

        public void SetIsPlasticExplosive(bool isPlasticExplosive)
        {
            _isPlasticExplosive = isPlasticExplosive;
        }
        
        public override string ToString()
        {
            var s = base.ToString();
            s += "\n\t\t\tCarga Explosiva";
            s += "\n\t\t\tDescrição do explosivo: " + _type;
            s += "\n\t\t\tExplosivo Plástico: " + (_isPlasticExplosive? "Sim" : "Não");
            s += "\n\t\t---------------------------------------------------------------------------------------------";

            return s;
        }

    }
}