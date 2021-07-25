using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Intrinsics.X86;
using Newtonsoft.Json;

namespace OOP_Project
{
    public class WeatherDetails //---- class para criar um objecto que recebe dados JSON da internet 
    {
        public string precipitaProb { get; set; }
        public string tMin { get; set; }
        public string tMax { get; set; }
        public string predWindDir { get; set; }
        public int idWeatherType { get; set; }
        public int classWindSpeed { get; set; }
        public string longitude { get; set; }
        public string forecastDate { get; set; }
        public string latitude { get; set; }
        
        public override string ToString()
        {
            string weatherDiscription;
            WeatherType wt = MainMenu.GetWeatherType();
            WeatherTypeDetails wd = new WeatherTypeDetails();

            wd = wt.data.Find(x => x.idWeatherType == this.idWeatherType);

            weatherDiscription = wd.descIdWeatherTypePT;

            string s = "\t\t" + forecastDate;
            s +="\n\t\t" + weatherDiscription;
            s += "\n\t\tTemperatura Miníma: " + tMin;
            s += "\n\t\tTemperatura máxima: " + tMax + " ----------";

            return s;
        }
    }
}