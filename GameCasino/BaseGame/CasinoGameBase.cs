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
        public string GameDetails { get; }

        public GameEventArgs(int betAmount, string gameDetails)
        {
            BetAmount = betAmount;
            GameDetails = gameDetails;
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

        protected virtual void OnWinInvoke(string details) =>
        OnWin?.Invoke(this, new GameEventArgs(Bet, details));

        protected virtual void OnLoseInvoke(string details) =>
            OnLose?.Invoke(this, new GameEventArgs(Bet, details));

        protected virtual void OnDrawInvoke(string details) =>
            OnDraw?.Invoke(this, new GameEventArgs(Bet, details));
    }
}
