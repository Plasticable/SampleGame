using System;
using System.Collections.Generic;



namespace SampleGame.Entity
{
    class AbstractEntity : World.LevelObject
    {
        public int speed { get; set; } = 0;

        #pragma warning disable 414
            public bool controlable = false;
        #pragma warning restore 414

        public AbstractEntity() : base(World.ObjectType.Entity)
        { }

        public string GetName()
        {
            return GetType().Name;
        }

        /*
        public double GetMouseAngle(int mouseX, int mouseY)
        {
            const double RAD_2_DEG = 180 / Math.PI;
            double degAngle = Math.Atan2(mouseY - y, mouseX - x) * RAD_2_DEG;
            return degAngle < 0 ? degAngle += 360 : degAngle;
        }
        */

        public EntityPlayer ToPlayer()
        {
            if (!textureName.ToLower().Equals("player"))
                return null;

            EntityPlayer result = new EntityPlayer(x, y);
            
            result.direction = direction;
            result.controlable = controlable;

            return result;
        }
    }
}
