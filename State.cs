﻿using System;
using System.Collections.Generic;

namespace OOP_Project
{
    [Serializable]
    public class State
    {
        private List<Ship> _ships;
        private List<Container> _containers;
        
        public State(){}

        public State(List<Ship> ships, List<Container> containers)
        {
            _ships = ships;
            _containers = containers;
        }

        public List<Ship> GetShips()
        {
            return _ships;
        }

        public void SetShips(List<Ship> ships)
        {
            _ships = ships;
        }
        
        public List<Container> GetContainers()
        {
            return _containers;
        }

        public void SetContainers(List<Container> containers)
        {
            _containers = containers;
        }

        public override string ToString()
        {
            string s = _ships.ToString();
            string c = _containers.ToString();

            return s + c;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is State e)) return false;

            bool result =
                _ships.Equals(e._ships) &&
                _containers.Equals(e._containers);

            return result;
        }
    }
}