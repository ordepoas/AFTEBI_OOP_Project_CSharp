using System;

namespace OOP_Project
{
    public class MaxChemicalException : Exception
    {
        public MaxChemicalException(){}
        
        public MaxChemicalException(string message) : base(message){}
    }
}