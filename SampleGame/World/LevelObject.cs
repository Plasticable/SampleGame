using System;
using System.Collections.Generic;

namespace SampleGame.World
{
    internal class LevelObject : DrawableObject
    {
        public ObjectType type;
        public string objectName = "unknownObject";
        public bool solid = true;
        
        public LevelObject(ObjectType type)
        {
            this.type = type;
            objectName = type.ToString();
        }

        public bool CollidesSolid(int x, int y)
        {
            return Game.instance.level.GetObjectsAt(x, y, new List<LevelObject> { this }).Count > 0;
        }

        /*
        public bool Collides(LevelObject anotherObject)
        {
            return CollidesPosition(anotherObject.x,  anotherObject.y)
                || CollidesPosition(anotherObject.x + anotherObject.width, anotherObject.y)
                || CollidesPosition(anotherObject.x + anotherObject.width, anotherObject.y + anotherObject.height)
                || CollidesPosition(anotherObject.x,  anotherObject.y +    anotherObject.height);
        }
        */

        public bool CollidesPosition(int x, int y)
        {
            return (x >= this.x && x <= this.x + width) && (y >= this.y && y <= this.y + height);
        }
    }
}