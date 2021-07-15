using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_Project
{
    [Serializable]
    public class Ship
    {
        private string _name;
        private int _number;
        private int _maxContainers;
        private int _maxExplosive;
        private int _maxChemical;
        private string _flag;
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
            else
            {
                switch (c)
                {
                    case Explosive _ when _containers.OfType<Explosive>().Count() == _maxExplosive:
                        throw new MaxExplosiveException(
                            "\t\tEste navio já tem o máximo de contentores do tipo Transporte de Explosivo! Contentor não adicionado!");
                    case Chemical _ when _containers.OfType<Chemical>().Count() == _maxChemical:
                        throw new MaxChemicalException(
                            "\t\tEste navio já tem o máximo de contentores do tipo Transporte de Químicos!" +
                            " Contentor não adicionado!"
                        );
                    default:
                        _containers.Add(c);
                        break;
                }
            }
        }

        public override string ToString()
        {
            string s = "\t\tNome: " + _name + "\tNúmero: " + _number + "\tBandeira: " + _flag;
            s += "\n\t\tQuant. máxima de contentores: " + _maxContainers;
            s += "\n\t\tQuant. contentores para transporte de Explosivos: " + _maxExplosive;
            s += "\n\t\tQuant. contentores para transporte de Químicos: " + _maxChemical;
            s += "\n\t\t================================================\n";
            s += _containers.ToString();

            return s;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Ship s)) return false;
            bool result =
                _name.Equals(s._name) &&
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
    }
}