using System;
using System.Collections.Generic;

namespace OOP_Project
{
    public class Weather
    {
        public string owner { get; set; }
        public string country { get; set; }
        public List<WeatherDetails> data { get; set; }
        public int globalIdLocal { get; set; }
        public DateTime dataUpdate { get; set; }
    }
}