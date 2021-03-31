using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Interface
{
    public class UIGroup : UI_Component
    {
        public List<UI_Component> UI_Components;

        private Vector2 position;
        public override Vector2 Position
        { 
            set
            {
                // If you change the Position of a UIGroup, it will change the position of every
                // UI_Component by the same difference.
                Vector2 translate;
                if (this.position == null)
                {
                    translate = value;
                }
                else
                {
                    translate = this.position - value;
                }
                foreach(UI_Component component in UI_Components)
                {
                    component.Position -= translate;
                }
                this.position = value;
            }
            get 
            { 
                return this.position; 
            } 
        }
        public override Vector2 Size { get { return new Vector2(0); } }

        public UIGroup()
        {
            UI_Components = new List<UI_Component>();
        }

        public void Add(UI_Component ui_component)
        {
            UI_Components.Add(ui_component);
        }

        public bool Remove(UI_Component ui_component)
        {
            if(UI_Components.Contains(ui_component))
            {
                UI_Components.Remove(ui_component);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach(UI_Component ui_component in UI_Components)
            {
                ui_component.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(UI_Component ui_component in UI_Components)
            {
                ui_component.Draw(gameTime, spriteBatch);
            }
        }

        public bool InitializeButton(EventHandler clickEvent, string buttonText)
        {
            foreach(UI_Component component in UI_Components)
            {
                if(component.GetType() == typeof(Button) || component.GetType() == typeof(ProgressBarButton))
                {
                    Button button = (Button)component;
                    if(button.Text == buttonText)
                    {
                        button.Click += clickEvent;
                        button.Click += SetSelectedButton;
                        return true;
                    }
                }
            }
            return false;
        }

        private void SetSelectedButton(object sender, EventArgs e)
        {
            foreach (UI_Component component in UI_Components)
            {
                if (component.GetType() == typeof(Button))
                {
                    ((Button)component).BackColour = ControlConstants.BUTTON_BACKCOLOR;
                }
                if (component.GetType() == typeof(ProgressBarButton))
                {
                    ((ProgressBarButton)component).BackColour = ControlConstants.PROGRESSBUTTON_BACKCOLOR;
                    ((ProgressBarButton)component).FrontColour = ControlConstants.PROGRESSBUTTON_FRONTCOLOR;
                }
            }
            if (sender.GetType() == typeof(ProgressBarButton))
            {
                ((ProgressBarButton)sender).Selected();
            }
            else if (sender.GetType() == typeof(Button))
            {
                ((Button)sender).Selected();
            }
        }
    }
}
