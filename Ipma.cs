using System.Buffers;
using System.Collections.Generic;
using System.Net.Mime;

namespace OOP_Project
{
    public class Ipma
    {
        private string _owner;
        private string _country;
        private List<Precipitation> _data;
        
        public Ipma(){}

        public Ipma(string owner, string country, List<Precipitation> data)
        {
            _owner = owner;
            _country = country;
            _data = data;
        }

        public string GetOwner()
        {
            return _owner;
        }

        public void SetOwner(string owner)
        {
            _owner = owner;
        }

        public string GetCountry()
        {
            return _country;
        }

        public void SetCountry(string country)
        {
            _country = country;
        }
        

    }
}