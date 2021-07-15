using System;
using System.Collections.Generic;

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
        private List<Container> _containers;
        
        public Ship(){}

        public Ship(string name, int number, int maxContainers, int maxExplosive, int maxChemical)
        {
            _name = name;
            _number = number;
            _maxContainers = maxContainers;
            _maxExplosive = maxExplosive;
            _maxChemical = maxChemical;
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
            if (_containers != null && _containers.Count == _maxContainers)
            {
                throw new MaxContainersException(
                    "\t\tEste navio já tem o máximo de contentores! Contentor não adicionado!");
            }
            else
            {

                if (c is Explosive || _containers.Count == _maxExplosive)
                {
                    throw new MaxExplosiveException(
                        "\t\tEste navio já tem o máximo de contentores do tipo Transporte de Explosivo! Contentor não adicionado!");
                }
                else
                {
                    if (c is Chemical || _containers.Count == _maxChemical)
                    {
                        throw new MaxChemicalException(
                            "\t\tEste navio já tem o máximo de contentores do tipo Transporte de Químicos!" +
                            " Contentor não adicionado!"
                        );
                    }
                    else
                    {
                        _containers.Add(c);
                    }
                }
            }
        }

        public override string ToString()
        {
            string s = "\t\tNome: " + _name + "Número: " + _number;
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
                _containers == s._containers;

            return result;
        }
    }
}