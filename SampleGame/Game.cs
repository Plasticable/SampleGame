using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using OpenTK.Input;
using SampleGame.Entity;
using System.Collections.Generic;
using SampleGame.World;

namespace SampleGame
{
    class Game : GameWindow
    {

        public EntityPlayer localPlayer;
        // public List<AbstractEntity> entities;
        public Level level;

        public static Game instance = null;

        public Game(int width, int height)
                : base (width,     height)
        {
            

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            


            instance = this;   
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ContentManager.LoadTexture("green/up1.png", "player");
            ContentManager.LoadTexture("box.jpg", "box");
            
            
            ContentManager.LoadLevel("two");

            ChangeLevel(ContentManager.LoadLevel("main"));
            

            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, 1, -1);

            GL.MatrixMode(MatrixMode.Modelview);
            

        }

        public void ChangeLevel(Level newLevel)
        {
            // TOFIX


            level = newLevel;
            localPlayer = newLevel.GetControllable();
        }
        

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            
            if(Focused)
                Controls.CheckKeys();
            
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.Purple);

            foreach (LevelObject lo in level.levelObjects)
                if (lo.type.Equals(ObjectType.Background))
                    lo.Draw();

            localPlayer.Draw();

            foreach (LevelObject lo in level.levelObjects)
                if (lo.type.Equals(ObjectType.Normal))
                    lo.Draw();

            foreach (AbstractEntity entity in level.entities)
                if(!entity.controlable)
                    entity.Draw();

            foreach (LevelObject lo in level.levelObjects)
                if (lo.type.Equals(ObjectType.Foreground))
                    lo.Draw();


            SwapBuffers();
        }
    }
}
