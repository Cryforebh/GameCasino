using GameCasino.BaseGame;
using GameCasino.ProfileSystem;
using GameCasino.SaveLoad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.BaseGame
{
    public interface IGame
    {
        void StartGame();
        void SaveProfile();
    }

}
