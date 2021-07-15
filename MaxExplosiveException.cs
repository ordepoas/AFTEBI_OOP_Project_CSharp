using System;

namespace OOP_Project
{
    public class MaxExplosiveException : Exception
    {
        public MaxExplosiveException() : base() { }

        public MaxExplosiveException(string message) : base(message) { }
    }
}