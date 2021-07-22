using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace OOP_Project
{
    public class WeatherType //---- class para criar um objecto que recebe dados JSON da internet 
    {
        public string owner { get; set; }
        public string country { get; set; }
        public List<WeatherTypeDetails> data { get; set; }
        
    }
}