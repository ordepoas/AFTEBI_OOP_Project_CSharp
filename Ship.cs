using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace OOP_Project
{
    [Serializable][DataContract (Name = "Navio")]
    public class Ship //---- Classe para objetos tipo Navio
    {
        [DataMember (Name = "Nome")]
        private string _name;
        [DataMember (Name = "Número")]
        private int _number;
        [DataMember (Name = "MaxContentores")]
        private int _maxContainers;
        [DataMember (Name = "MaxExplosivos")]
        private int _maxExplosive;
        [DataMember (Name = "MaxQuimicos")]
        private int _maxChemical;
        [DataMember (Name = "Bandeira")]
        private string _flag;
        [DataMember (Name = "NoPorto")]
        private bool _isAtPort;
        [DataMember (Name = "Contentores")]
        private List<Container> _containers;
        
        public Ship(){}

        public Ship(string name, int maxContainers, int maxExplosive, int maxChemical, string flag)
        {
            _name = name;
            _number = ShipNumber();
            _maxContainers = maxContainers;
            _maxExplosive = maxExplosive;
            _maxChemical = maxChemical;
            _flag = flag;
            _isAtPort = true;
            _containers = new List<Container>();
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public int GetNumber()
        {
            return _number;
        }

        public void SetNumber(int number)
        {
            _number = number;
        }

        public int GetMaxContainers()
        {
            return _maxContainers;
        }

        public void SetMaxContainers(int maxContainers)
        {
            _maxContainers = maxContainers;
        }

        public int GetMaxExplosive()
        {
            return _maxExplosive;
        }

        public void SetMaxExplosive(int maxExplosive)
        {
            _maxExplosive = maxExplosive;
        }

        public int GetMaxChemical()
        {
            return _maxChemical;
        }

        public void SetMaxChemical(int maxChemical)
        {
            _maxChemical = maxChemical;
        }

        public string GetFlag()
        {
            return _flag;
        }

        public void SetFlag(string flag)
        {
            _flag = flag;
        }

        public bool GetIsAtPort()
        {
            return _isAtPort;
        }

        public void SetIsAtPort(bool isAtPort)
        {
            _isAtPort = isAtPort;
        }

        public List<Container> GetContainers()
        {
            return _containers;
        }

        public void SetContainers(List<Container> containers)
        {
            _containers = containers;
        }

        public void AddContainer(Container c)
        {
            if (_containers.Count == _maxContainers)
            {
                throw new MaxContainersException(
                    "\t\tEste navio já tem o máximo de contentores! Contentor não adicionado!");
            }
            switch (c)
            {
                case Explosive _ when _containers.OfType<Explosive>().Count() == _maxExplosive:
                    throw new MaxExplosiveException(
                        "\t\tEste navio já tem o máximo de contentores do tipo Explosivo! Contentor não adicionado!");
                case Chemical _ when _containers.OfType<Chemical>().Count() == _maxChemical:
                    throw new MaxChemicalException(
                        "\t\tEste navio já tem o máximo de contentores do tipo Químico!" +
                        " Contentor não adicionado!"
                    );
                default:
                    _containers.Add(c);
                    break;
            }
        }

        public override string ToString()
        {
            string s = "\t\tNome: " + _name + "\tNúmero: " + _number + "\tBandeira: " + _flag;
            s += "\n\t\tMax. contentores: " + _maxContainers + "\t Max Explosivo: " + _maxExplosive + "\t Max Químicos: " + _maxChemical;
            if (_isAtPort == true)
            {
                s += "\n\t\tAtracado no porto: Sim";
            }
            else
            {
                s += "\n\t\tAtracado no porto: Não";
            }
            s += "\n\t\t---------------------------------------------------------------------------------------------";

            return s;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Ship s)) return false;
            bool result =
                _number == s._number &&
                _maxContainers == s._maxContainers &&
                _maxExplosive == s._maxExplosive &&
                _maxChemical == s._maxChemical &&
                _flag == s._flag;

            return result;
        }

        public int ShipNumber()
        {
            var random = new Random();
            var rand = random.Next(1000000, 9999999);

            return rand;
        }

        public int ListContainers()
        {
            if (_containers.Count == 0)
            {
                Console.WriteLine("\n\t\tEste navio não tem contentores atribuídos!");
                Console.Write("\n\t\tPrima uma tecla para continuar...");
                Console.ReadLine();
                
            }
            else
            {
                _containers.ForEach(Console.WriteLine);
            }

            return _containers.Count;
        }
    }
}