using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder
{
    public class GameData
    {
        public Town Town;
        public Grid Grid;

        public GameData()
        {
        }

        public void Initialize(Town town, Grid grid)
        {
            Town = town;
            Grid = grid;
        } 
    }
}
