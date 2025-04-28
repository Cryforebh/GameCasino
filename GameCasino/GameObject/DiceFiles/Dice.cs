using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.GameObject.DiceFiles
{
    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(int min, int max)
            : base($"Неверный диапазон кубиков. Разрешено: {min}-{max}") { }
    }

    public class Dice
    {
        private readonly int _min;
        private readonly int _max;
        private static Random _random = new Random();

        public int Value { get; private set; }

        public Dice(int min, int max)
        {
            _min = min;
            _max = max;
            Roll();
        }

        public void Roll()
        {
            Value = _random.Next(_min, _max + 1);
        }
    }
}
