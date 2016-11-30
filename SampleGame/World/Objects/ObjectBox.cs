using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleGame.World.Objects
{
    class ObjectBox : LevelObject
    {
        public ObjectBox(int x, int y) : base(ObjectType.Normal)
        {
            base.x = x;
            base.y = y;

            objectName  = "box";
            textureName = "box";
            width  = 32;
            height = 32;

            
        }
    }
}
