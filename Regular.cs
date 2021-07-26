using System;
using System.Runtime.Serialization;

namespace OOP_Project
{
    [Serializable][DataContract (Name = "Regular")]
    public class Regular : Container //---- subclasse do contentor
    {
        [DataMember (Name = "Descrição")]
        private string _description;
        [DataMember (Name = "ERefrigerado")]
        private bool _isRefrigerated;
        
        public Regular(){}

        public Regular(string description, bool isRefrigerated, string destination, int weight )
        {
            ShipNumber = -1;
            Number = ContainerNumber();
            Destination = destination;
            Weight = weight;
            _description = description;
            _isRefrigerated = isRefrigerated;
        }
        
        public string GetDescription()
        {
            return _description;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }

        public bool GetIsRefrigerated()
        {
            return _isRefrigerated;
        }

        public void SetIsRefrigerated(bool isRefrigerated)
        {
            _isRefrigerated = isRefrigerated;
        }

        public override string ToString()
        {
            var s = base.ToString();
            s += "\n\t\t\tCarga Regular";
            s += "\n\t\t\tDescrição: " + _description;
            s += "\n\t\t\tRefrigerado: " + (_isRefrigerated? "Sim" : "Não");
            s += "\n\t\t---------------------------------------------------------------------------------------------";
            
            return s;
        }
        
    }
}