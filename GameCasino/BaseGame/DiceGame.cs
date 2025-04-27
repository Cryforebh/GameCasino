using GameCasino.GameObject.DiceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.BaseGame
{
    public class DiceGame : CasinoGameBase
    {
        private List<Dice> _dices;
        private readonly int _diceCount;
        private readonly int _min;
        private readonly int _max;

        public DiceGame(int diceCount, int min, int max, int bet) : base(bet)
        {
            if (diceCount <= 0) throw new ArgumentException("Dice count must be positive");
            if (min >= max) throw new ArgumentException("Min must be less than max");

            _diceCount = diceCount;
            _min = min;
            _max = max;
        }

        protected override void FactoryMethod()
        {
            _dices = new List<Dice>();
            for (int i = 0; i < _diceCount; i++)
            {
                _dices.Add(new Dice(_min, _max));
            }
        }

        public override void PlayGame()
        {
            int total = 0;
            var random = new Random();
            for (int i = 0; i < _diceCount; i++)
            {
                total += random.Next(_min, _max + 1);
            }

            if (total > 7)
            {
                OnWinInvoke();
            }
            else
            {
                OnLoseInvoke();
            }
        }

        private int CalculateSum()
        {
            return _dices.Sum(d => d.Number);
        }
    }
}
