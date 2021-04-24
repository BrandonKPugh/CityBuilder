using CityBuilder.GameCode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder
{
    public class Structure : Object
    {
        public StructureData Data;
        protected Grid _grid;

        public enum StructureType
        {
            House,
            Warehouse,
            Lumbermill,
            Capitol,
            Forge,
            Mine,
            Road,
            Other
        }

        public enum Rotation
        {
            Normal, 
            Right,
            Half,
            Left
        }

        public struct StructureSize
        {
            public StructureSize(int width, int height)
            {
                this.Width = width;
                this.Height = height;
            }
            public int Width;
            public int Height;
        }

        public struct StructureData
        {
            public StructureData(int width, int height, int x1, int y1, StructureType type = StructureType.Other)
            {
                this.Size = new StructureSize(width, height);
                this.X1 = x1;
                this.Y1 = y1;
                this.Cost = new Dictionary<Resource.ResourceType, int>();
                this.Type = type;
                this.Rotation = Rotation.Normal;
                IsRoad = false;
            }
            public StructureData(StructureSize size, int x1, int y1, StructureType type = StructureType.Other)
            {
                this.Size = size;
                this.X1 = x1;
                this.Y1 = y1;
                this.Cost = new Dictionary<Resource.ResourceType, int>();
                this.Type = type;
                this.Rotation = Rotation.Normal;
                IsRoad = false;
            }
            public StructureSize Size;
            public StructureType Type;
            public int X1;
            public int Y1;
            public Rotation Rotation;
            public int X2 { get { return X1 + Size.Width - 1; } }
            public int Y2 { get { return Y1 + Size.Height - 1; } }
            public int Height { get { return Size.Height; } }
            public int Width { get { return Size.Width; } }
            public Dictionary<Resource.ResourceType, int> Cost;
            public bool IsRoad;
        }

        public Structure(CollisionBody collision, StructureData data) : base(collision)
        {
            this.Data = data;
            if(collision.Shape == CollisionBody.ShapeType.Rectangle)
            {
                RectangleBody rect = ((RectangleBody)this.Collision);
                rect.Size = new Vector2(collision.Region().Width * data.Width, collision.Region().Height * data.Height);
            }
        }

        public Structure(Grid buildGrid, StructureData data)
        {
            this._grid = buildGrid;
            RectangleBody rect;
            if (data.X1 < 0 || data.Y1 < 0)
            {
                rect = new RectangleBody(buildGrid.TileToPixelRect(0, 0));
                rect.Position.X = -10000;
                rect.Position.Y = -10000;
            }
            else
                rect = new RectangleBody(buildGrid.TileToPixelRect(data.X1, data.Y1));
            this.Collision = rect;
            rect.Size = new Vector2(rect.Region().Width * data.Width, rect.Region().Height * data.Height);
            this.Data = data;

            Collision = rect;
            Collision.Parent = this;

            Velocity = new Vector2(0f);
            Acceleration = new Vector2(0f);
        }

        public void RotateRight()
        {
            throw new NotImplementedException("Function not complete");
            /*
            this.Data.Rotation++;
            if (this.Data.Rotation > Rotation.Left)
                this.Data.Rotation = Rotation.Normal;
            //*/
        }

        public void RotateLeft()
        {
            throw new NotImplementedException("Function not complete");
            /*
            this.Data.Rotation--;

            if (this.Data.Rotation < Rotation.Normal)
                this.Data.Rotation = Rotation.Right;
            //*/
        }

        public static Dictionary<Resource.ResourceType, int> GetStructureCostByType(Structure.StructureType structureType)
        {
            Dictionary<Resource.ResourceType, int> cost = new Dictionary<Resource.ResourceType, int>();
            switch (structureType)
            {
                case StructureType.House:
                    {
                        cost.Add(Resource.ResourceType.Wood, 50);
                        cost.Add(Resource.ResourceType.Stone, 20);
                        cost.Add(Resource.ResourceType.Metal, 5);
                        break;
                    }
                case StructureType.Warehouse:
                    {
                        cost.Add(Resource.ResourceType.Wood, 200);
                        cost.Add(Resource.ResourceType.Stone, 100);
                        break;
                    }
                case StructureType.Lumbermill:
                    {
                        cost.Add(Resource.ResourceType.Wood, 150);
                        cost.Add(Resource.ResourceType.Stone, 50);
                        cost.Add(Resource.ResourceType.Metal, 10);
                        break;
                    }
                case StructureType.Capitol:
                    {
                        cost.Add(Resource.ResourceType.Wood, 500);
                        cost.Add(Resource.ResourceType.Stone, 500);
                        cost.Add(Resource.ResourceType.Metal, 250);
                        break;
                    }
                case StructureType.Forge:
                    {
                        cost.Add(Resource.ResourceType.Stone, 300);
                        break;
                    }
                case StructureType.Mine:
                    {
                        cost.Add(Resource.ResourceType.Wood, 500);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return cost;
        }

        public static Dictionary<Resource.ResourceType, int> GetStructureCost(Structure structure)
        {
            if (structure.Data.Cost != null)
                return structure.Data.Cost;
            else
            {
                return GetStructureCostByType(structure.Data.Type);
            }
        }

        public static StructureSize GetStructureSize(Structure structure)
        {
            return structure.Data.Size;
        }

        public static StructureSize GetStructureDefaultSize(StructureType structureType)
        {
            switch (structureType)
            {
                case StructureType.House:
                    {
                        return new StructureSize(1, 1);
                    }
                case StructureType.Warehouse:
                    {
                        return new StructureSize(4, 2);
                    }
                case StructureType.Lumbermill:
                    {
                        return new StructureSize(2, 2);
                    }
                case StructureType.Capitol:
                    {
                        return new StructureSize(4, 3);
                    }
                case StructureType.Forge:
                    {
                        return new StructureSize(2, 2);
                    }
                case StructureType.Mine:
                    {
                        return new StructureSize(4, 4);
                    }
                case StructureType.Road:
                    {
                        return new StructureSize(1, 1);
                    }
                default:
                    {
                        throw new NotImplementedException("That structure type does not have a size!");
                    }
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            float rotation = ((int)Data.Rotation) * ((float)Math.PI / 2f);

            Rectangle dest = Collision.Region();
            if (Data.Rotation != Rotation.Normal)
            {
                switch(Data.Rotation)
                {
                    case Rotation.Right:
                        {
                            dest = new Rectangle(dest.X, dest.Y, dest.Height, dest.Width);
                            break;
                        }
                    case Rotation.Half:
                        {
                            dest = new Rectangle(dest.X, dest.Y, dest.Width, dest.Height);
                            break;
                        }
                    case Rotation.Left:
                        {
                            dest = new Rectangle(dest.X, dest.Y, dest.Height, dest.Width);
                            break;
                        }
                    default:
                        {
                            throw new Exception("Rotation does not exist!");
                        }
                }
            }
            this.Sprite.Draw(spriteBatch, Collision.Region(), rotation);
        }
    }
}
