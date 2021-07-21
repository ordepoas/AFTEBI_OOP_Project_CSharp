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


        public static void GetNews()
        {
            try
            {
                WebRequest request = HttpWebRequest.Create("https://observador.pt/wp-json/obs_api/v4/news/widget");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string strJson = reader.ReadToEnd();

                List<News> latestestNews = JsonConvert.DeserializeObject<List<News>>(strJson);
                Console.WriteLine();
                Console.WriteLine("\t\t--------------------------------- Últimas Notícias ---------------------------------");
                foreach (var news in latestestNews)
                {
                    Console.WriteLine(news);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadKey();

            }


            Console.Write("\n\t\tPrima uma tecla para continuar...");
            Console.ReadKey();
        }

        public override string ToString()
        {
            string s = "\t\tTítulo: " + title;
            s += "\n\t\tPublicado em: " + publish_date;
            s += "\n\t\t------------";

            return s;
        }
    }
}