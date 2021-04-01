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
        private ContentManager content;
        private Town town;
        private SpriteFont titleFont;
        private SpriteFont resourceFont;
        private Texture2D cardTexture;
        private UIBox scrollBox;
        private List<StructureCard> structureCards;

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
            Button scrollBoxDownArrow = new Button(content, ControlConstants.BUILD_SCROLLBOX_DOWN);
            scrollBoxDownArrow.Click += ScrollBoxDownArrow_Click;
            this.Add(scrollBoxDownArrow);
            UIBox scrollBoxSlider = new UIBox(content, ControlConstants.BUILD_SCROLLBOX_SLIDER);
            this.Add(scrollBoxSlider);

            
        }

        private void ScrollBoxDownArrow_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ScrollBoxUpArrow_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void AddCard(Structure.StructureType type)
        {
            if (cardTexture == null)
                cardTexture = content.Load<Texture2D>(ControlConstants.STRUCTURE_CARD_TEXTURE);
            StructureCard newCard = new StructureCard(cardTexture);
            newCard.Initialize(this.town, type, NextCardPosition(), titleFont, resourceFont);
            structureCards.Add(newCard);
            this.Add(newCard);
        }

        private Vector2 NextCardPosition()
        {
            Vector2 pos = scrollBox.Position + ControlConstants.STRUCTURE_CARD_POSITION;
            pos.Y += ControlConstants.STRUCTURE_CARD_SIZE.Y * (structureCards.Count);
            return pos;
        }
    }
}
