using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder
{
    public class Town
    {
        private List<Structure> _structures;

        public Town()
        {
            _structures = new List<Structure>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Structure structure in _structures)
            {
                structure.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Structure structure in _structures)
            {
                structure.Update(gameTime);
            }
        }

        public void AddStructure(Structure structure)
        {
            _structures.Add(structure);
        }
    }
}
