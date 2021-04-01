using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder.Interface
{
    class GhostStructure : Structure
    {

        public GhostStructure(Grid buildGrid, StructureData data) : base(buildGrid, data)
        {

        }

        public new void Update(GameTime gameTim)
        {
            MouseState mouseState = Mouse.GetState();
            RectangleBody mouseCollision = new RectangleBody(mouseState.Position.ToVector2(), new Vector2(0, 0));
            Vector2 coordOffset = new Vector2((this._grid.Info.TileWidth * this.Data.Size.Width - this._grid.Info.TileWidth) / 2, (this._grid.Info.TileHeight * this.Data.Size.Height - this._grid.Info.TileHeight) / 2);

            if(_grid == null)
            {
                throw new Exception("Grid not initialized in GhostStructure");
            }
            if(mouseCollision.CollidesWith(new RectangleBody(_grid.Info.GridRectangle)))
            {
                _grid.PixelToTile((int)(mouseCollision.Position.X - coordOffset.X), (int)(mouseCollision.Position.Y - coordOffset.Y), out int tileX, out int tileY);
                if (tileX < 0 || tileY < 0)
                {
                    this.Collision.Position = mouseState.Position.ToVector2() - (this.Collision.Region().Size.ToVector2() / 2);
                }
                else
                {
                    Rectangle loc = _grid.TileToPixelRect(tileX, tileY);
                    loc.Size = new Vector2(loc.Width * Data.Width, loc.Height * Data.Height).ToPoint();
                    this.Collision = new RectangleBody(loc);
                }
            }
            else
            {
                this.Collision.Position = mouseState.Position.ToVector2() - (this.Collision.Region().Size.ToVector2() / 2);
            }
        }
    }
}
