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

        public void Initialize(Town town, Structure.StructureType type, Vector2 position, SpriteFont titleFont, SpriteFont resourceFont)
        {
            this.StructureType = type;
            this.Position = position;
            this.Size = ControlConstants.STRUCTURE_CARD_SIZE;
            this.town = town;

            _hoverColor = ControlConstants.STRUCTURE_CARD_HOVER_COLOR;

            TextBox title = new TextBox(titleFont);
            title.Text = type.ToString();
            title.Position = this.Position + ControlConstants.STRUCTURE_CARD_TITLE_POSITION;
            title.Size = ControlConstants.STRUCTURE_CARD_TITLE_SIZE;
            title.Color = ControlConstants.STRUCTURE_CARD_TITLE_COLOR;
            title.TextAlignment = TextBox.TextAlign.Left;

            Dictionary<Resource.ResourceType, int> structureCost = Structure.GetStructureCostByType(type);
            Vector2 nextCountPos = this.Position + ControlConstants.STRUCTURE_CARD_RESOURCE_COUNT_POSITION;
            Vector2 nextLabelPos = this.Position + ControlConstants.STRUCTURE_CARD_RESOURCE_LABEL_POSITION;
            foreach (Resource.ResourceType resource in structureCost.Keys)
            {
                TextBox resourceCost = new TextBox(resourceFont);
                TextBox resourceLabel = new TextBox(resourceFont);

                resourceCost.Text = structureCost[resource].ToString();
                resourceLabel.Text = resource.ToString();

                resourceCost.Position = nextCountPos;
                resourceLabel.Position = nextLabelPos;

                nextCountPos += ControlConstants.STRUCTURE_CARD_RESOURCE_LABEL_OFFSET;
                nextLabelPos += ControlConstants.STRUCTURE_CARD_RESOURCE_LABEL_OFFSET;

                resourceCost.Size = ControlConstants.STRUCTURE_CARD_RESOURCE_COUNT_SIZE;
                resourceLabel.Size = ControlConstants.STRUCTURE_CARD_RESOURCE_LABEL_SIZE;

                resourceCost.Color = ControlConstants.STRUCTURE_CARD_RESOURCE_COUNT_COLOR;
                resourceLabel.Color = ControlConstants.STRUCTURE_CARD_RESOURCE_LABEL_COLOR;

                resourceCost.TextAlignment = TextBox.TextAlign.Right;
                resourceLabel.TextAlignment = TextBox.TextAlign.Left;

                this.Add(resourceCost);
                this.Add(resourceLabel);
            }

            this.Add(title);
            this.Click += StructureCard_Click;
        }

        private void StructureCard_Click(object sender, EventArgs e)
        {
            town.BeginStructurePlacement(this.StructureType);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (town.CanCreateStructureByType(StructureType))
            {
                this.BackColour = ControlConstants.STRUCTURE_CARD_PLACEABLE;
                this.IsActive = true;
            }
            else
            {
                this.BackColour = ControlConstants.STRUCTURE_CARD_NOT_PLACEABLE;
                this.IsActive = false;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
