namespace OOP_Project
{
    public class PrecipitationDetails
    {
            public string descClassPrecIntEN { get; set; }
            public string descClassPrecIntPT { get; set; }
            public string classPrecInt { get; set; }

            public override string ToString()
            {
                string s = "Classe de Precipitação em EN: " + descClassPrecIntEN;
                s += "\nClass de Precipitação em PT: " + descClassPrecIntPT;
                s += "\nIntensidade: " + classPrecInt;
                s += "\n___________________________________________________________________\n";

                return s;
            }
    }
}