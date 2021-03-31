using CityBuilder.GameCode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder.Interface
{
    public class StructureCard : Card
    {
        public Structure.StructureType StructureType;

        private Town town;


        public StructureCard()
        {
            
        }

        public StructureCard(Texture2D texture) : base(texture)
        {

        }

        public void Initialize(EventHandler Click, Town town, Structure.StructureType type, Vector2 position, SpriteFont titleFont, SpriteFont resourceFont)
        {
            this.StructureType = type;
            this.Position = position;
            this.Size = ControlConstants.STRUCTURE_CARD_SIZE;
            this.town = town;
            TextBox title = new TextBox(titleFont);
            title.Text = type.ToString();
            title.Position = this.Position + ControlConstants.STRUCTURE_CARD_TITLE_POSITION;
            title.Size = ControlConstants.STRUCTURE_CARD_TITLE_SIZE;
            title.Color = ControlConstants.STRUCTURE_CARD_TITLE_COLOR;
            this.Add(title);
            this.Click += Click;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (town.CanCreateStructure(StructureType))
                this.BackColour = ControlConstants.STRUCTURE_CARD_PLACEABLE;
            else
                this.BackColour = ControlConstants.STRUCTURE_CARD_NOT_PLACEABLE;
            base.Draw(gameTime, spriteBatch);
        }
    }
}
