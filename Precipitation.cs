using System.Collections.Generic;

namespace OOP_Project
{
    public class Precipitation
    {
        public string _owner { get; set; }
        public string _country { get; set; }
        public List<PrecipitationDetails> _data { get; set; }
    }
}