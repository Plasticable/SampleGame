using OpenTK;
using OpenTK.Input;
using SampleGame.Entity;
using SampleGame.World;
using System;
using System.Collections.Generic;
using System.Text;
using static SampleGame.World.DrawableObject;

namespace SampleGame
{
    class Controls
    {

        public static Key EXIT = Key.Escape;
        public static Key FULLSCREEN = Key.F11;
        public static Key DEBUG = Key.F9;

        public static Key LEVEL_ONE = Key.F5;
        public static Key LEVEL_TWO = Key.F8;

        public static Key UP = Key.W;
        public static Key DOWN = Key.S;
        public static Key LEFT = Key.A;
        public static Key RIGHT = Key.D;

        private static long time = 0;

        private static long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalMilliseconds;
        }

        public static bool NeedUpdate()
        {
            long current = UnixTimeNow();

            if (time < current)
            {
                time = current + 100;   
                return true;
            }

            return false;
        }

        public static void CheckKeys()
        {
            if (!NeedUpdate())
                return;

            KeyboardState state = Keyboard.GetState();

            Game game = Game.instance;
            EntityPlayer player = game.localPlayer;

            if (state.IsKeyDown(EXIT))
                game.Exit();

            if (state.IsKeyDown(FULLSCREEN))
                game.WindowState =
                    game.WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;

            if (state.IsKeyDown(LEVEL_ONE))
                game.ChangeLevel(ContentManager.GetLevel("main"));

            if (state.IsKeyDown(LEVEL_TWO))
                game.ChangeLevel(ContentManager.GetLevel("two"));

            int speed = game.localPlayer.speed;


            if (state.IsKeyDown(UP))
            {
                player.direction = Direction.UP;

                if (!player.CollidesSolid(player.x, player.y - speed))
                    player.y -= speed;
            }

            if (state.IsKeyDown(DOWN))
            {
                player.direction = Direction.DOWN;
                if (!player.CollidesSolid(player.x, player.y + speed))
                    player.y += speed;
            }

            if (state.IsKeyDown(LEFT))
            {
                player.direction = Direction.LEFT;

                if (!player.CollidesSolid(player.x - speed, player.y))
                    player.x -= speed;
            }

            if (state.IsKeyDown(RIGHT))
            {
                player.direction = Direction.RIGHT;
                if (!player.CollidesSolid(player.x + speed, player.y))
                    player.x += speed;
            }

            if (state.IsKeyDown(DEBUG))
            {
                Console.WriteLine("===Begin debug message===");
                foreach (LevelObject lo in Game.instance.level.GetObjectsAt(player.x, player.y, null))
                    Console.WriteLine(lo.objectName);
                Console.WriteLine("=== End  debug message===");
            }
        }
    }
}
