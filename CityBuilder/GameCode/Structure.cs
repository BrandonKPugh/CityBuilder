using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder
{
    public class Structure : Object
    {
        public int StructureTileWidth;
        public int StructureTileHeight;

        public Structure(Game1 game, CollisionBody collision) : base(game, collision)
        {
            
        }

        public void RotateRight()
        {
            throw new NotImplementedException();
        }

        public void RotateLeft()
        {
            throw new NotImplementedException();
        }
    }
}
