using GameCasino.GameObject.CardFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.BaseGame
{
    public class BlackjackGame : CasinoGameBase
    {
        private Queue<Card> _deck;
        private readonly int _cardCount;

        public BlackjackGame(int cardCount, int bet) : base(bet)
        {
            if (cardCount < 4) throw new ArgumentException("Требуется минимум 4 карты");
            _cardCount = cardCount;
        }

        protected override void FactoryMethod()
        {
            var cards = new List<Card>();
            // Генерация карт
            Shuffle(cards);
        }

        private void Shuffle(List<Card> cards)
        {
            _deck = new Queue<Card>(cards.OrderBy(c => Guid.NewGuid()));
        }

        public override void PlayGame()
        {
            var random = new Random();
            int playerScore = random.Next(17, 25);
            int dealerScore = random.Next(17, 25);

            if (playerScore > 21 || (dealerScore <= 21 && dealerScore > playerScore))
            {
                OnLoseInvoke();
            }
            else if (playerScore == dealerScore)
            {
                OnDrawInvoke();
            }
            else
            {
                OnWinInvoke();
            }
        }
    }
}
