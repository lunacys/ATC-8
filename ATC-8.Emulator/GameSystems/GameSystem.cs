using System;
using Microsoft.Xna.Framework;

namespace ATC8.Emulator.GameSystems
{
    public abstract class GameSystem
    {
        public event EventHandler OnReset;

        public IGameSystemManager GameSystemManager { get; internal set; }

        public bool IsActive { get; set; }

        protected GameSystem() { }

        public T FindSystem<T>() where T : GameSystem
        {
            return GameSystemManager?.FindSystem<T>();
        }

        public virtual void Initialize() { }
        public virtual void Update(GameTime gameTime) { }

        public virtual void Reset()
        {
            OnReset?.Invoke(this, EventArgs.Empty);
        }
    }
}