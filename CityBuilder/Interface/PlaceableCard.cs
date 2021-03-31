using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder.Interface
{
    public class PlaceableCard : Button
    {
        public override Vector2 Position
        { 
            get => base.Position; 
            set
            {
                base.Position = value;
                uiData.Position = value;
            }
        }
        private UIGroup uiData;
        public PlaceableCard()
        {
            uiData = new UIGroup();
            uiData.Position = this.Position;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            uiData.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            uiData.Draw(gameTime, spriteBatch);
        }

        public void Add(UI_Component ui_component)
        {
            uiData.Add(ui_component);
        }

        public bool Remove(UI_Component ui_component)
        {
            return uiData.Remove(ui_component);
        }

        /*
         * From UIGroup.cs
        public bool InitializeButton(EventHandler clickEvent, string buttonText)
        {
            return uiData.InitializeButton(clickEvent, buttonText);
        }
        */
    }
}
