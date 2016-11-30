using System;
using System.Collections.Generic;
using System.Drawing;

using OpenTK.Graphics.OpenGL;
using Newtonsoft.Json;

namespace SampleGame.Entity
{
    class EntityPlayer : AbstractEntity
    {
        public EntityPlayer(int x, int y)
        {
            base.x = x;
            base.y = y;

            speed = 32;
            direction = Direction.DOWN;

            width = 32;
            height = 32;

            textureName = "player";
            
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
