using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace OOP_Project
{
    public class News //---- class para criar um objecto que recebe dados JSON da internet 
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime publish_date { get; set; }
        public string tag { get; set; }
        public string lead { get; set; }
        public string image { get; set; }
        public string image_16x9 { get; set; }
        public string url { get; set; }
        public object authors { get; set; }
        
        public override string ToString()
        {
            string s = "Título: " + title;
            s += "\n" + lead;
            s += "\nPublicado em: " + publish_date;
            s += "\n------------";

            return s;
        }
    }
}