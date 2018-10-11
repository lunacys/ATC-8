using System.Collections.Generic;

namespace ATC8.Emulator.GameSystems
{
    public interface IGameSystemManager
    {
        T FindSystem<T>() where T : GameSystem;

        IList<GameSystem> GetAllGameSystems();
    }
}