﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace CityBuilder
{
    public class PhysicsGridTile
    {
        public Point Position;
        public static int Size;
        public Dictionary<uint, CollisionBody> Data;

        public PhysicsGridTile(Point position, int size)
        {
            Position = position;
            Size = size;
            Data = new Dictionary<uint, CollisionBody>();
        }

        public void Add(CollisionBody body)
        {
            Data.Add(body.ID, body);
        }

        public void Remove(CollisionBody body)
        {
            Data.Remove(body.ID);
        }

        public void Remove(uint collisionBodyId)
        {
            Data.Remove(collisionBodyId);
        }
    }
}
