﻿using System;
using System.Runtime.Serialization;

namespace OOP_Project
{
    [Serializable][DataContract (Name = "Regular")]
    public class Regular : Container
    {
        [DataMember (Name = "Descrição")]
        private string _description;
        [DataMember (Name = "ERefrigerado")]
        private bool _isRefrigerated;
        
        public Regular(){}

        public Regular(string description, bool isRefrigerated, string destination, int weight ) : base(destination, weight)
        {
            _description = description;
            _isRefrigerated = isRefrigerated;
        }
        
        public Regular(string description, bool isRefrigerated, int shipNumber, string destination, int weight ) : base(shipNumber, destination, weight)
        {
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
            s += "\n\t\t\t------------------";
            s += "\n\t\t\tCarga Regular";
            s += "\n\t\t\tDescrição: " + _description;
            s += "\n\t\t\tRefrigerado: " + (_isRefrigerated? "Sim" : "Não");
            s += "\n\t\t---------------------------------------------------------------------------------------------";
            
            return s;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Regular r)) return false;
            var result =
                _description.Equals(r._description) &&
                _isRefrigerated == r._isRefrigerated;

            return result;
        }
    }
}