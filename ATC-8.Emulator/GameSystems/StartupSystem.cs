using System;
using lunge.Library.GameSystems;
using lunge.Library.GameTimers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ATC8.Emulator.GameSystems
{
    public class StartupSystem : DrawableGameSystem
    {
        public StartupSystem(Game game) 
            : base(game)
        {
            Console.WriteLine("StartupSystem");
            GameTimerManager.Add(new GameTimer(2.0, true, (sender, args) =>
            {
                Console.WriteLine("Elapsed");
            }));
        }
    }
}