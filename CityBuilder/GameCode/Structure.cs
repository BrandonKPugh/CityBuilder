using CityBuilder.GameCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder
{
    public class Structure : Object
    {
        public StructureData Data;

        public enum StructureType
        {
            House,
            Warehouse
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
            public StructureData(int width, int height, int x1, int y1)
            {
                this.Size = new StructureSize(width, height);
                this.X1 = x1;
                this.Y1 = y1;
                Cost = new Dictionary<Resource.ResourceType, int>();
            }
            public StructureData(StructureSize size, int x1, int y1)
            {
                this.Size = size;
                this.X1 = x1;
                this.Y1 = y1;
                Cost = new Dictionary<Resource.ResourceType, int>();
            }
            public StructureSize Size;
            public int X1;
            public int Y1;
            public int X2 { get { return X1 + Size.Width - 1; } }
            public int Y2 { get { return Y1 = Size.Height - 1; } }
            public int Height { get { return Size.Height; } }
            public int Width { get { return Size.Width; } }
            public Dictionary<Resource.ResourceType, int> Cost;
        }

        public Structure(Game1 game, CollisionBody collision, StructureData data) : base(game, collision)
        {
            this.Data = data;
            if(collision.Shape == CollisionBody.ShapeType.Rectangle)
            {
                RectangleBody rect = ((RectangleBody)this.Collision);
                rect.Size = new Vector2(collision.Region().Width * data.Width, collision.Region().Height * data.Height);
            }
        }

        public Structure(Game1 game, Grid buildGrid, StructureData data)
        {
            RectangleBody rect = new RectangleBody(buildGrid.TileToPixelRect(data.X1, data.Y1));
            this.Collision = rect;
            rect.Size = new Vector2(rect.Region().Width * data.Width, rect.Region().Height * data.Height);
            this.Data = data;

            Game = game;
            Collision = rect;
            Collision.Parent = this;

            Velocity = new Vector2(0f);
            Acceleration = new Vector2(0f);
        }

        public void RotateRight()
        {
            throw new NotImplementedException();
        }

        public void RotateLeft()
        {
            throw new NotImplementedException();
        }

        public static Dictionary<Resource.ResourceType, int> GetStructureCost(StructureType structureType)
        {
            Dictionary<Resource.ResourceType, int> cost = new Dictionary<Resource.ResourceType, int>();
            switch(structureType)
            {
                case StructureType.House:
                    {
                        cost.Add(Resource.ResourceType.Wood, 500000);
                        cost.Add(Resource.ResourceType.Stone, 25);
                        cost.Add(Resource.ResourceType.Ore, 10);
                        cost.Add(Resource.ResourceType.Metal, 5);
                        break;
                    }
                case StructureType.Warehouse:
                    {
                        cost.Add(Resource.ResourceType.Wood, 400);
                        cost.Add(Resource.ResourceType.Stone, 150);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return cost;
        }
    }
}
