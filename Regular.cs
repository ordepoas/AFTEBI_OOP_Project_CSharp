﻿using System;

namespace OOP_Project
{
    [Serializable]
    public class Regular : Container
    {
        private string _description;
        private bool _isRefrigerated;
        
        public Regular(){}

        public Regular(string description, bool isRefrigerated, int number, string destination, int weight ) : base(number, destination, weight)
        {
            _description = description;
            _isRefrigerated = isRefrigerated;
        }
        
        public Regular(string description, bool isRefrigerated, int shipNumber, int number, string destination, int weight ) : base(shipNumber, number, destination, weight)
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
            s += "\n\t\tDescrição: " + _description;
            s += "\n\t\tRefrigerado: " + (_isRefrigerated? "Sim" : "Não");

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