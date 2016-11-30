using Newtonsoft.Json;
using SampleGame.Entity;
using SampleGame.World.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleGame.World
{
    public enum ObjectType { Foreground, Background, Normal, Entity };

    class Level
    {   
        public List<LevelObject> levelObjects = new List<LevelObject>();
        public List<AbstractEntity> entities  = new List<AbstractEntity>();
        public string name = "unnamed";
        
    
        public Level(string name)
        {
            foreach (AbstractEntity entity in entities)
                Console.WriteLine("Loaded entity: " + entity.GetName());

            ObjectBox box = new ObjectBox(128, 128);

            levelObjects.Add(box);
               
            foreach (LevelObject levelObject in levelObjects)
                Console.WriteLine("Loaded level object: {0} ({1})", levelObject.objectName, levelObject.type.ToString());

            this.name = name;
        }

        public EntityPlayer GetControllable()
        {

            foreach (AbstractEntity entity in entities)
               if (entity.ToPlayer() != null && entity.controlable)
                    return entity.ToPlayer();

            return null;
        }

        public List<LevelObject> GetObjectsAt(int x, int y, List<LevelObject> except)
        {
            List<LevelObject> objects = new List<LevelObject>();

            foreach (LevelObject lo in levelObjects)
                if (lo.CollidesPosition(x, y))
                {
                    Console.WriteLine("Object (X: {0} Y: {1}) Collides X: {2} Y: {3}", lo.x, lo.y, x, y);
                    objects.Add(lo);
                    
                }
                else
                    Console.WriteLine("Object (X: {0} Y: {1}) NOT Collides X: {2} Y: {3}", lo.x, lo.y, x, y);

            return objects;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
