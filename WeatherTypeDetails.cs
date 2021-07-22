using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace OOP_Project
{
    public class WeatherTypeDetails //---- class para criar um objecto que recebe dados JSON da internet 
    {
        public string descIdWeatherTypeEN { get; set; }
        public string descIdWeatherTypePT { get; set; }
        public int idWeatherType { get; set; }
        
    }
}