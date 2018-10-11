using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace ATC8.Emulator.GameSystems
{
    public class GameSystemComponent : DrawableGameComponent, IGameSystemManager
    {
        private readonly List<GameSystem> _gameSystems;

        public GameSystemComponent(Game game, IEnumerable<GameSystem> systems)
            : this(game)
        {
            foreach (var gameSystem in systems)
                Register(gameSystem);
        }

        public GameSystemComponent(Game game)
            : base(game)
        {
            _gameSystems = new List<GameSystem>();
        }

        public T FindSystem<T>() where T : GameSystem
        {
            var system = _gameSystems.OfType<T>().FirstOrDefault();

            if (system == null)
                throw new InvalidOperationException($"{typeof(T).Name} not registered");

            return system;
        }

        public IList<GameSystem> GetAllGameSystems()
        {
            return _gameSystems;
        }

        public T Register<T>(T system) where T : GameSystem
        {
            system.GameSystemManager = this;
            system.IsActive = true;
            _gameSystems.Add(system);

            return system;
        }

        public void Reset()
        {
            foreach (var gameSystem in _gameSystems)
                gameSystem.Reset();
        }

        public override void Initialize()
        {
            foreach (var gameSystem in _gameSystems)
                gameSystem.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var gameSystem in _gameSystems.Where(s => s.IsActive))
                gameSystem.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var gameSystem in _gameSystems.Where(s => s.IsActive))
                (gameSystem as DrawableGameSystem)?.Draw(gameTime);
        }
    }
}