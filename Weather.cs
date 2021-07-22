using System;
using System.Collections.Generic;

namespace OOP_Project
{
    public class Weather //---- class para criar um objecto que recebe dados JSON da internet 
    {
        public string owner { get; set; }
        public string country { get; set; }
        public List<WeatherDetails> data { get; set; }
        public int globalIdLocal { get; set; }
        public DateTime dataUpdate { get; set; }
    }
}