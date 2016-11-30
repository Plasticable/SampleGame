using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using Newtonsoft.Json;
using SampleGame.World;
using System.Diagnostics;

namespace SampleGame
{
    class ContentManager
    {
        
        private static Dictionary<string, int> textures = new Dictionary<string, int>();
        public static List<Level> levels = new List<Level>();

        public static Level GetLevel(string levelName)
        {
            foreach (Level level in levels)
                if (level.name.Equals(levelName))
                    return level;

            throw new ArgumentException("Level with that name doesn't exists");
        }

        public static Level LoadLevel(string levelName)
        {
            string path = levelName.ToLower().StartsWith("resources/levels/") 
                ? levelName : "resources/levels/" + levelName;

            path += ".json";

            if (!File.Exists(path))
                throw new FileNotFoundException("Level " + levelName + " not found at path: " + path);


            Console.WriteLine("Loading level " + levelName);
            Stopwatch timer = Stopwatch.StartNew();

            string json = File.ReadAllText(path);

            Level level = JsonConvert.DeserializeObject<Level>(json);

            levels.Add(level);

            LevelObject lo = level.levelObjects[0];

            Console.WriteLine("Is {0} collides: {1}", lo.objectName, lo.CollidesPosition(130, 130));
            

            timer.Stop();
            Console.WriteLine("Loaded in {0} ms", timer.ElapsedMilliseconds);
            timer.Reset();

            return level;
        }


        public static int GetTextureId(String textureName)
        {
            if (!textures.ContainsKey(textureName))
                throw new ArgumentException("Texture with that name doesn't exists");

            return textures[textureName];
        }

        public static int LoadTexture(String path, String name)
        {
            if (!path.ToLower().StartsWith("resources/sprites/"))
                path = "resources/sprites/" + path;

            if (!File.Exists(path))
                throw new FileNotFoundException("Texture "+name+" not found at path: " + path);

            int id = GL.GenTexture();
            
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap(path);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter , (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            textures.Add(name, id);

            return id;
        }
    }
}
