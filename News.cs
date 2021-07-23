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
            string p1, p2, p3;

            string s = "\t\tTítulo: " + title;
            if (lead.Length > 93)
            {

                if (lead.Length > 186)
                {
                    p3 = lead.Substring(186);
                    p2 = lead.Substring(93);
                    p2 = p2.Remove(93);
                    p1 = lead.Remove(93);
                    s += "\n\t\t" + p1;
                    s += "\n\t\t" + p2;
                    s += "\n\t\t" + p3;

                }
                else
                {
                    p2 = lead.Substring(93);
                    p1 = lead.Remove(93);
                    s += "\n\t\t" + p1;
                    s += "\n\t\t" + p2;
                }

            }
            else
            {
                s += "\n\t\t" + lead;
            }
            s += "\n\t\tPublicado em: " + publish_date;
            s += "\n\t\t------------";

            return s;
        }
    }
}