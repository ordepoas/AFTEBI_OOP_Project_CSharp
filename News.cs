using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace OOP_Project
{
    public class News
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
            string s = "\t\tTítulo: " + title;
            s += "\n\t\tPublicado em: " + publish_date;
            s += "\n\t\t------------";

            return s;
        }
    }
}