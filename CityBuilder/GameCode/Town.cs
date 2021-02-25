using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder.GameCode
{
    public class Town
    {
        private List<Structure> _structures;

        public Town()
        {

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
    }
}
