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
            this.Collision.Position = mouseState.Position.ToVector2() - (this.Collision.Region().Size.ToVector2() / 2);
        }
    }
}
