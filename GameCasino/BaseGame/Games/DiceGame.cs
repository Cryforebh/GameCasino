using GameCasino.GameObject.DiceFiles;
using GameCasino.ProfileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.BaseGame.Games
{
    public class DiceGame : CasinoGameBase
    {
        private List<Dice> _dices;
        private readonly int _diceCount;
        private readonly int _min;
        private readonly int _max;

        public DiceGame(int diceCount, int min, int max, int bet) : base(bet)
        {
            if (diceCount <= 0) throw new ArgumentException("Количество кубиков должно быть положительным");
            if (min >= max) throw new ArgumentException("Мин. должен быть меньше макс.");

            _diceCount = diceCount;
            _min = min;
            _max = max;
            FactoryMethod();
        }

        protected override void FactoryMethod()
        {
            _dices = new List<Dice>();
            for (int i = 0; i < _diceCount; i++)
            {
                _dices.Add(new Dice(_min, _max));
            }
        }

        private int RollDice()
        {
            _dices.ForEach(d => d.Roll());
            return _dices.Sum(d => d.Value);
        }

        public override void PlayGame()
        {
            int playerSum = RollDice();
            string playerLog = $"Ваш результат: {string.Join(" + ", _dices.Select(d => d.Value))} = {playerSum}";

            int computerSum = RollDice();
            string computerLog = $"Дилер: {string.Join(" + ", _dices.Select(d => d.Value))} = {computerSum}";

            string result = $"{playerLog}\n{computerLog}\nИтог: ";

            if (playerSum > computerSum)
            {
                OnWinInvoke(result + "Победа!");
            }
            else if (computerSum > playerSum)
            {
                OnLoseInvoke(result + "Поражение");
            }
            else
            {
                OnDrawInvoke(result + "Ничья");
            }
        }
    }
}
