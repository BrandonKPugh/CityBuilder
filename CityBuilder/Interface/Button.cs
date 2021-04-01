using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Interface
{
    public class Button : UI_Component
    {
        #region Fields

        protected MouseState _currentMouse;

        protected SpriteFont _font;

        protected bool _isHovering;

        protected Color _hoverColor;

        protected MouseState _previousMouse;

        protected Texture2D _texture;

        #endregion

        #region Properties

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }
        public Color BackColour { get; set; }
        public Color HoverColour { get { return _hoverColor; } set { _hoverColor = value; } }
        public bool IsActive = true;

        public ControlConstants.BUTTON_INFO ButtonInfo { set { Position = new Vector2(value.X, value.Y); Size = new Vector2(value.Width, value.Height); Text = value.Text; } }

        public Rectangle Location
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            }
        }

        public string Text { get; set; }

        #endregion

        #region Methods

        public Button()
        {

            _hoverColor = new Color((int)(BackColour.R * .50f), (int)(BackColour.G * .50f), (int)(BackColour.B * .50f), (int)BackColour.A);
        }
        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            PenColour = ControlConstants.BUTTON_PENCOLOR;

            BackColour = ControlConstants.BUTTON_BACKCOLOR;
            _hoverColor = new Color((int)(BackColour.R * .50f), (int)(BackColour.G * .50f), (int)(BackColour.B * .50f), (int)BackColour.A);
        }
        public Button(ContentManager content, ControlConstants.BUTTON_INFO buttonInfo)
        {
            PenColour = ControlConstants.BUTTON_PENCOLOR;
            BackColour = ControlConstants.BUTTON_BACKCOLOR;
            this.ButtonInfo = buttonInfo;
            this._texture = content.Load<Texture2D>(buttonInfo.Texture_Name);
            if(buttonInfo.Font_Name != null)
            {
                this._font = content.Load<SpriteFont>(buttonInfo.Font_Name);
            }

            _hoverColor = new Color((int)(BackColour.R * .50f), (int)(BackColour.G * .50f), (int)(BackColour.B * .50f), (int)BackColour.A);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = BackColour;
            if (_isHovering && IsActive)
                color = _hoverColor;

            spriteBatch.Draw(_texture, Location, color);

            if (!string.IsNullOrEmpty(Text))
            {
                CenterString(Text, _font, Position, Size, spriteBatch, PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                _previousMouse = _currentMouse;
                _currentMouse = Mouse.GetState();

                var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

                _isHovering = false;

                if (mouseRectangle.Intersects(Location))
                {
                    _isHovering = true;

                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        Click?.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        public virtual void Selected()
        {
            BackColour = ControlConstants.BUTTON_SELECTED;
        }

        #endregion
    }
}
