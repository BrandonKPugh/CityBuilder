using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder.Interface
{
    public class ScrollBox : UIGroup
    {
        private const int CARDS_PER_PAGE = 2;
        private ContentManager content;
        private Town town;
        private SpriteFont titleFont;
        private SpriteFont resourceFont;
        private Texture2D cardTexture;
        private UIBox scrollBox;
        private List<StructureCard> structureCards;
        private int page = 0;
        private Button buttonUp;
        private Button buttonDown;
        private UIBox slider;

        private int maxPages
        {
            get { return (int)((structureCards.Count-1) / CARDS_PER_PAGE); }
        }

        public ScrollBox(ContentManager content, ControlConstants.UIBOX_INFO info)
        {
            this.content = content;
            UIBox scrollBox = new UIBox(content, info);
            this.Add(scrollBox);
            this.scrollBox = scrollBox;
            structureCards = new List<StructureCard>();
        }

        public void Initialize(Town town)
        {
            titleFont = content.Load<SpriteFont>(ControlConstants.STRUCTURE_CARD_TITLE_FONT);
            resourceFont = content.Load<SpriteFont>(ControlConstants.STRUCTURE_CARD_RESOURCE_FONT);

            this.town = town;

            Button scrollBoxUpArrow = new Button(content, ControlConstants.BUILD_SCROLLBOX_UP);
            scrollBoxUpArrow.Click += ScrollBoxUpArrow_Click;
            this.Add(scrollBoxUpArrow);
            buttonUp = scrollBoxUpArrow;
            Button scrollBoxDownArrow = new Button(content, ControlConstants.BUILD_SCROLLBOX_DOWN);
            scrollBoxDownArrow.Click += ScrollBoxDownArrow_Click;
            this.Add(scrollBoxDownArrow);
            buttonDown = scrollBoxDownArrow;
            UIBox scrollBoxSlider = new UIBox(content, ControlConstants.BUILD_SCROLLBOX_SLIDER);
            this.Add(scrollBoxSlider);
            slider = scrollBoxSlider;

            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (page <= 0)
            {
                buttonUp.IsActive = false;
                buttonUp.BackColour = ControlConstants.BUILD_SCROLLBOX_UPDOWN_INACTIVE_COLOR;
            }
            else
            {
                buttonUp.IsActive = true;
                buttonUp.BackColour = ControlConstants.BUILD_SCROLLBOX_UPDOWN_ACTIVE_COLOR;
            }
            if (page >= maxPages)
            {
                buttonDown.IsActive = false;
                buttonDown.BackColour = ControlConstants.BUILD_SCROLLBOX_UPDOWN_INACTIVE_COLOR;
            }
            else
            {
                buttonDown.IsActive = true;
                buttonDown.BackColour = ControlConstants.BUILD_SCROLLBOX_UPDOWN_ACTIVE_COLOR;
            }
            SetSliderPosition();
            base.Draw(gameTime, spriteBatch);
            for(int i = page * CARDS_PER_PAGE; i < ((page + 1) * CARDS_PER_PAGE); i++)
            {
                if(structureCards.Count > i)
                    structureCards[i].Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            for (int i = page * CARDS_PER_PAGE; i < ((page + 1) * CARDS_PER_PAGE); i++)
            {
                if (structureCards.Count > i)
                    structureCards[i].Update(gameTime);
            }
        }

        private void ScrollBoxDownArrow_Click(object sender, EventArgs e)
        {
            AttemptPageDown();
        }

        private void ScrollBoxUpArrow_Click(object sender, EventArgs e)
        {
            AttemptPageUp();
        }

        private void AttemptPageUp()
        {
            if(page > 0)
            {
                page--;
            }

        }

        private void AttemptPageDown()
        {
            if(page < maxPages)
            {
                page++;
            }

        }

        public void AddCard(Structure.StructureType type)
        {
            if (cardTexture == null)
                cardTexture = content.Load<Texture2D>(ControlConstants.STRUCTURE_CARD_TEXTURE);
            StructureCard newCard = new StructureCard(cardTexture);
            newCard.Initialize(this.town, type, NextCardPosition(), titleFont, resourceFont);
            structureCards.Add(newCard);
            //this.Add(newCard);
        }

        private Vector2 NextCardPosition()
        {
            Vector2 pos = scrollBox.Position + ControlConstants.STRUCTURE_CARD_POSITION;
            pos.Y += ControlConstants.STRUCTURE_CARD_SIZE.Y * (structureCards.Count % CARDS_PER_PAGE);
            return pos;
        }
        public void SetSliderPosition()
        {
            int min = ControlConstants.BUILD_SCROLLBOX_SLIDER.Y;
            int max = ControlConstants.BUILD_SCROLLBOX_SLIDER.Y + ControlConstants.BUILD_SCROLLBOX_SLIDER_RANGE;
            int newY = min + (int)(((float)page / maxPages) * (max - min));
            slider.Position = new Vector2(slider.Position.X, newY);
        }
    }
}
