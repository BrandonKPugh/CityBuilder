using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder
{
    public class Town
    {
        private SpriteSheet _spriteSheet;
        private List<Structure> _structures;

        private bool _contentLoaded = false;

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

        public void LoadContent(SpriteSheet spriteSheet)
        {
            _spriteSheet = spriteSheet;
            foreach(Structure structure in _structures)
            {
                LoadStructureContent(structure);
            }
            _contentLoaded = true;
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
            if(_contentLoaded)
            {
                LoadStructureContent(structure);
            }
        }

        public void LoadStructureContent(Structure structure)
        {
            structure.LoadContent(_spriteSheet.GetSprite("structure-" + structure.Data.width + 'x' + structure.Data.height));
        }
    }
}
