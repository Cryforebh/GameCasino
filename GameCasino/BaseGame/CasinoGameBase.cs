using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.BaseGame
{
    public class GameEventArgs : EventArgs
    {
        public int BetAmount { get; }

        public GameEventArgs(int betAmount)
        {
            BetAmount = betAmount;
        }
    }

    public abstract class CasinoGameBase
    {
        public event EventHandler<GameEventArgs> OnWin;
        public event EventHandler<GameEventArgs> OnLose;
        public event EventHandler<GameEventArgs> OnDraw;

        protected readonly int Bet;

        protected CasinoGameBase(int bet)
        {
            Bet = bet;
            FactoryMethod();
        }

        protected abstract void FactoryMethod();
        public abstract void PlayGame();

        protected virtual void OnWinInvoke() => OnWin?.Invoke(this, new GameEventArgs(Bet));
        protected virtual void OnLoseInvoke() => OnLose?.Invoke(this, new GameEventArgs(Bet));
        protected virtual void OnDrawInvoke() => OnDraw?.Invoke(this, new GameEventArgs(Bet));
    }
}
