using CityBuilder.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder.GameCode
{
    public class Road : Structure
    {
        private SpriteSheet _spriteSheet;
        public Road(Grid buildGrid, StructureData data, SpriteSheet spriteSheet) : base(buildGrid, data)
        {
            this.Data.IsRoad = true;
            _spriteSheet = spriteSheet;
        }

        public void RecalculateTexture(Town town)
        {
            bool top = false;
            bool right = false;
            bool bottom = false;
            bool left = false;
            int x = Data.X1;
            int y = Data.Y1;
            // Top side
            if (y != 0)
            {
                if (town.IsStructureUnderTile(x, y - 1))
                {
                    Structure found = town.GetStructureUnderTile(x, y - 1);
                    if (found.Data.IsRoad)
                        top = true;
                }
            }

            // Right side
            if (x < _grid.Info.TilesWide - 1)
            {
                if (town.IsStructureUnderTile(x + 1, y))
                {
                    Structure found = town.GetStructureUnderTile(x + 1, y);
                    if (found.Data.IsRoad)
                        right = true;
                }
            }

            // Bottom side
            if (y < _grid.Info.TilesHigh - 1)
            {
                if (town.IsStructureUnderTile(x, y + 1))
                {
                    Structure found = town.GetStructureUnderTile(x, y + 1);
                    if (found.Data.IsRoad)
                        bottom = true;
                }
            }

            // Left side
            if (x != 0)
            {
                if (town.IsStructureUnderTile(x - 1, y))
                {
                    Structure found = town.GetStructureUnderTile(x - 1, y);
                    if (found.Data.IsRoad)
                        left = true;
                }
            }

            this.Sprite = _spriteSheet.GetSprite(GetSpriteName(top, right, bottom, left, out Rotation rotation));
            this.Data.Rotation = rotation;

        }

        private string GetSpriteName(bool top, bool right, bool bottom, bool left, out Rotation rotation)
        {
            int count = 0;
            if (top)
                count++;
            if (right)
                count++;
            if (bottom)
                count++;
            if (left)
                count++;

            rotation = Rotation.Normal;

            switch(count)
            {
                case 0:
                    {
                        return ControlConstants.ROAD_TEXTURE_ZERO_CONNECTIONS;
                    }
                case 1:
                    {
                        if (top)
                            rotation = Rotation.Normal;
                        else if (right)
                            rotation = Rotation.Right;
                        else if (bottom)
                            rotation = Rotation.Half;
                        else if (left)
                            rotation = Rotation.Left;
                        return ControlConstants.ROAD_TEXTURE_ONE_CONNECTION;
                    }
                case 2:
                    {
                        if (top && bottom)
                        {
                            rotation = Rotation.Normal;
                            return ControlConstants.ROAD_TEXTURE_TWO_CONNECTIONS_OPPOSITE;
                        }
                        else if(right && left)
                        {
                            rotation = Rotation.Right;
                            return ControlConstants.ROAD_TEXTURE_TWO_CONNECTIONS_OPPOSITE;
                        }
                        else if(top && right)
                        {
                            rotation = Rotation.Normal;
                            return ControlConstants.ROAD_TEXTURE_TWO_CONNECTIONS_ADJACENT;
                        }
                        else if (right && bottom)
                        {
                            rotation = Rotation.Right;
                            return ControlConstants.ROAD_TEXTURE_TWO_CONNECTIONS_ADJACENT;
                        }
                        else if (bottom && left)
                        {
                            rotation = Rotation.Half;
                            return ControlConstants.ROAD_TEXTURE_TWO_CONNECTIONS_ADJACENT;
                        }
                        else //if (left && top)
                        {
                            rotation = Rotation.Left;
                            return ControlConstants.ROAD_TEXTURE_TWO_CONNECTIONS_ADJACENT;
                        }
                    }
                case 3:
                    {
                        if(!left)
                        {
                            rotation = Rotation.Normal;
                        }
                        else if (!top)
                        {
                            rotation = Rotation.Right;
                        }
                        else if (!right)
                        {
                            rotation = Rotation.Half;
                        }
                        else if (!bottom)
                        {
                            rotation = Rotation.Left;
                        }
                        return ControlConstants.ROAD_TEXTURE_THREE_CONNECTIONS;
                    }
                case 4:
                    {
                        return ControlConstants.ROAD_TEXTURE_FOUR_CONNECTIONS;
                    }
                default:
                    {
                        throw new Exception("Road cannot have have that many connections");
                    }
            }
        }
    }
}
