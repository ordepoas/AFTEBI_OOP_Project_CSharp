using System;
using System.Collections.Generic;

namespace OOP_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<Ship> ships = new List<Ship>();
            List<Container> containers = new List<Container>();
            State portState = new State();

            if (Menu.RestoreMenu() == 1 && (portState = Methods.Restore()) != null)
            {
                ships = portState.GetShips();
                containers = portState.GetContainers();

            }

            while (Menu.MainMenu(ships, containers) != 0);
            
        }
    }
}