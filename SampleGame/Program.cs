using System;
using System.Collections.Generic;
using System.Text;

namespace SampleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game window = new Game(800, 600))
            {
                window.Run();

            }
        }
    }
}
