using System;

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
    }
}