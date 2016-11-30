using System;

using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace SampleGame.World
{
    class DrawableObject
    {
        public enum Direction { UP, RIGHT, DOWN, LEFT};

        public int x { get; set; }
        public int y { get; set; }
        
        // public double rotation { get; set; }
        public Direction direction { get; set; }

        public int width { get; set; }
        public int height { get; set; }


        public string textureName;


        public virtual void Draw()
        {
            GL.Color3(Color.White);
            GL.BindTexture(TextureTarget.Texture2D, ContentManager.GetTextureId(textureName));

            GL.PushMatrix();

            GL.MatrixMode(MatrixMode.Texture);
            GL.LoadIdentity();
            GL.MatrixMode(MatrixMode.Modelview);
            

            int m = 50; // M is for MAGIC

            GL.Translate((x + width / m), (y + height / m), 0);
            GL.Rotate(((int)direction) * 90, 0, 0, 1);
            GL.Translate((-x - width / m), (-y - height / m), 0);
            
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex2(x - width / 2, y - height / 2);

            GL.TexCoord2(1, 0);
            GL.Vertex2(x + width - width / 2, y - height / 2);

            GL.TexCoord2(1, 1);
            GL.Vertex2(x + width - width / 2, y + height - height / 2);

            GL.TexCoord2(0, 1);
            GL.Vertex2(x - width / 2, y + height - height / 2);

            GL.End();

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PopMatrix();


        }
    }
}
