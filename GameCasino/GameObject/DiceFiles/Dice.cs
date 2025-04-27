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

    public struct Dice
    {
        private readonly int _min;
        private readonly int _max;

        public int Number
        {
            get
            {
                var rnd = new Random();
                return rnd.Next(_min, _max + 1);
            }
        }

        public Dice(int min, int max)
        {
            if (min < 1 || max > int.MaxValue || min >= max)
                throw new WrongDiceNumberException(min, max);

            _min = min;
            _max = max;
        }
    }
}
