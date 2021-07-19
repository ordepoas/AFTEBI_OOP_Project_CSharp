using System;
using System.Runtime.Serialization;

namespace OOP_Project
{
    [Serializable][DataContract (Name = "Explosivo")]
    public class Explosive : Container
    {
        [DataMember (Name = "Tipo Explosivo")]
        private string _type;
        [DataMember (Name = "É plastico")]
        private bool _isPlasticExplosive;
        
        public Explosive(){}

        public Explosive(string type, bool isPlasticExplosive, string destination,
            int weight) : base(destination, weight)
        {
            _type = type;
            _isPlasticExplosive = isPlasticExplosive;
        }

        public Explosive(string type, bool isPlasticExplosive, int shipNumber, string destination,
            int weight) : base(shipNumber, destination, weight)
        {
            _type = type;
            _isPlasticExplosive = isPlasticExplosive;
        }

        public string GetType()
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
            s += "\n\t\t\t------------------";
            s += "\n\t\t\tCarga Explosiva";
            s += "\n\t\t\tDescrição do explosivo: " + _type;
            s += "\n\t\t\tExplosivo Plástico: " + (_isPlasticExplosive? "Sim" : "Não");
            s += "\n\t\t---------------------------------------------------------------------------------------------";

            return s;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Explosive e)) return false;
            bool result =
                _type.Equals(e._type) &&
                _isPlasticExplosive == e._isPlasticExplosive;

            return result;
        }
    }
}