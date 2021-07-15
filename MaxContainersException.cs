using System;

namespace OOP_Project
{
    public class MaxContainersException : Exception
    {
        public MaxContainersException() : base() { }

        public MaxContainersException(string message) : base(message) { }
    }
}