using GameCasino.Enum;
using GameCasino.GameObject.CardFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.BaseGame.Games
{
    public class BlackjackGame : CasinoGameBase
    {
        private Queue<Card> _deck;
        private readonly int _cardCount;
        private List<Card> _playerHand;
        private List<Card> _dealerHand;

        public BlackjackGame(int cardCount, int bet) : base(bet)
        {
            if (cardCount < 4) throw new ArgumentException("Требуется минимум 4 карты");
            _cardCount = cardCount;
            FactoryMethod();
        }

        protected override void FactoryMethod()
        {
            var cards = new List<Card>();

            var suits = (CardSuit[])System.Enum.GetValues(typeof(CardSuit));
            var ranks = (CardRank[])System.Enum.GetValues(typeof(CardRank));

            // Генерация карт
            foreach (CardSuit suit in suits)
            {
                foreach (CardRank rank in ranks)
                {
                    cards.Add(new Card(suit, rank));
                }
            }
            Shuffle(cards);
        }

        private void Shuffle(List<Card> cards)
        {
            _deck = new Queue<Card>(cards.OrderBy(c => Guid.NewGuid()));
        }

        private int GetHandValue(List<Card> hand)
        {
            int value = 0;
            int aces = 0;
            foreach (var card in hand)
            {
                if (card.Rank == CardRank.Ace) aces++;
                value += (int)card.Rank > 10 ? 10 : (int)card.Rank;
            }
            while (value > 21 && aces > 0)
            {
                value -= 10;
                aces--;
            }
            return value;
        }

        public override void PlayGame()
        {
            _playerHand = new List<Card> { _deck.Dequeue(), _deck.Dequeue() };
            _dealerHand = new List<Card> { _deck.Dequeue(), _deck.Dequeue() };

            int playerValue = GetHandValue(_playerHand);
            int dealerValue = GetHandValue(_dealerHand);

            string details = $"Карты игрока: {string.Join(" ", _playerHand)} ({playerValue}) | Карты дилера: {_dealerHand[0]} ??";

            if (playerValue > 21 || (dealerValue <= 21 && dealerValue > playerValue))
            {
                OnLoseInvoke(details);
            }
            else if (playerValue == dealerValue)
            {
                OnDrawInvoke(details);
            }
            else
            {
                OnWinInvoke(details);
            }
        }
    }
}
