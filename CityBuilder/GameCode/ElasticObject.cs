using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder
{
    public class ElasticObject : Object
    {
        public ElasticObject(CollisionBody collision) : base(collision)
        {
        }

        public void BounceOff(Object other)
        {
            Vector2 velocityToCheck = this.Velocity - other.Velocity;

            if (this.Collision.Shape == CollisionBody.ShapeType.Circle && other.Collision.Shape == CollisionBody.ShapeType.Circle)
            {
                // Circle on Circle
            }
            else if (this.Collision.Shape == CollisionBody.ShapeType.Circle && other.Collision.Shape == CollisionBody.ShapeType.Rectangle)
            {
                CircleBody c = (CircleBody)this.Collision;
                RectangleBody r = (RectangleBody)other.Collision;
                if (velocityToCheck.X >= 0)
                {
                    // Moving right
                    if (velocityToCheck.Y >= 0)
                    {
                        // Moving right-down
                        if (r.Position.X - c.Position.X <= r.Position.Y - c.Position.Y)
                        {
                            this.BounceOffTop(other);
                        }
                        else
                        {
                            this.BounceOffLeft(other);
                        }
                    }
                    else
                    {
                        // Moving right-up
                        if (-1 * (r.Position.X - c.Position.X) <= (r.Position.Y + r.Size.Y - c.Position.Y))
                        {
                            this.BounceOffLeft(other);
                        }
                        else
                        {
                            this.BounceOffBottom(other);
                        }
                    }
                }
                else
                {
                    // Moving left
                    if (velocityToCheck.Y >= 0)
                    {
                        // Moving left-down
                        if (r.Position.X + r.Size.X - c.Position.X <= -1 * (r.Position.Y - c.Position.Y))
                        {
                            this.BounceOffRight(other);
                        }
                        else
                        {
                            this.BounceOffTop(other);
                        }
                    }
                    else
                    {
                        // Moving left-up
                        if (r.Position.X + r.Size.X - c.Position.X <= r.Position.Y + r.Size.Y - c.Position.Y)
                        {
                            this.BounceOffRight(other);
                        }
                        else
                        {
                            this.BounceOffBottom(other);
                        }
                    }
                }
            }
        }

        private void BounceOffTop(Object other)
        {
            if (this.Collision.Shape == CollisionBody.ShapeType.Circle)
            {
                CircleBody c = (CircleBody)this.Collision;
                RectangleBody r = (RectangleBody)other.Collision;

                float dist = (c.Position.Y + c.Radius) - (r.Position.Y);
                this.Collision.Position.Y -= dist * 2;
                this.Velocity.Y *= -1;
            }
            else
            {
                RectangleBody r1 = (RectangleBody)this.Collision;
                RectangleBody r2 = (RectangleBody)other.Collision;

                float dist = (r1.Position.Y + r1.Size.Y) - (r2.Position.Y);
                this.Collision.Position.Y -= dist * 2;
                this.Velocity.Y *= -1;
            }
        }
        private void BounceOffLeft(Object other)
        {
            if (this.Collision.Shape == CollisionBody.ShapeType.Circle)
            {
                CircleBody c = (CircleBody)this.Collision;
                RectangleBody r = (RectangleBody)other.Collision;

                float dist = (c.Position.X + c.Radius) - (r.Position.X);
                this.Collision.Position.X -= dist * 2;
                this.Velocity.X *= -1;
            }
            else
            {
                RectangleBody r1 = (RectangleBody)this.Collision;
                RectangleBody r2 = (RectangleBody)other.Collision;

                float dist = (r1.Position.X + r1.Size.X) - (r2.Position.X);
                this.Collision.Position.X -= dist * 2;
                this.Velocity.X *= -1;
            }
        }
        private void BounceOffRight(Object other)
        {
            if (this.Collision.Shape == CollisionBody.ShapeType.Circle)
            {
                CircleBody c = (CircleBody)this.Collision;
                RectangleBody r = (RectangleBody)other.Collision;

                float dist = (r.Position.X + r.Size.X) - (c.Position.X - c.Radius);
                this.Collision.Position.X += dist * 2;
                this.Velocity.X *= -1;
            }
            else
            {
                RectangleBody r1 = (RectangleBody)this.Collision;
                RectangleBody r2 = (RectangleBody)other.Collision;

                float dist = (r2.Position.X + r2.Size.X) - (r1.Position.X);
                this.Collision.Position.X += dist * 2;
                this.Velocity.X *= -1;
            }
        }
        private void BounceOffBottom(Object other)
        {
            if (this.Collision.Shape == CollisionBody.ShapeType.Circle)
            {
                CircleBody c = (CircleBody)this.Collision;
                RectangleBody r = (RectangleBody)other.Collision;

                float dist = (r.Position.Y + r.Size.Y) - (c.Position.Y - c.Radius);
                this.Collision.Position.Y += dist * 2;
                this.Velocity.Y *= -1;
            }
            else
            {
                RectangleBody r1 = (RectangleBody)this.Collision;
                RectangleBody r2 = (RectangleBody)other.Collision;

                float dist = (r2.Position.Y + r2.Size.Y) - (r1.Position.Y);
                this.Collision.Position.Y += dist * 2;
                this.Velocity.Y *= -1;
            }
        }
    }
}
