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

        // Метод рассчитывает значение карт в руке игрока с учетом особой роли тузов.
        private int GetHandValue(List<Card> hand)
        {
            int value = 0;                  // Общая сумма очков 
            int aces = 0;                   // Счётчик тузов
            foreach (var card in hand)
            {
                if (card.Rank == CardRank.Ace) aces++;
                value += (int)card.Rank > 10 
                    ? 10                    // Для карт Валет, Дама, Король (значения 11-13)
                    : (int)card.Rank;       // Для карт 2-10
            }
            while (value > 21 && aces > 0)
            {
                value -= 10;                // Изменение значение туза с 11 на 1
                aces--;                     // Уменьшаем счетчик неучтенных тузов
            }
            return value;
        }

        public override void PlayGame()
        {
            _playerHand = new List<Card> { _deck.Dequeue(), _deck.Dequeue(), };
            _dealerHand = new List<Card> { _deck.Dequeue(), _deck.Dequeue(), };

            bool playerTurn = true;
            bool dealerTurn = true;

            while (playerTurn || dealerTurn)
            {
                // Ход игрока
                if (playerTurn)
                {
                    Console.WriteLine(GetGameStatus());
                    Console.WriteLine("F - Взять карту | Любая клавиша - Пас");

                    if (Console.ReadKey(true).Key == ConsoleKey.F)
                    {
                        _playerHand.Add(_deck.Dequeue());
                    }
                    else
                    {
                        playerTurn = false;
                    }
                }

                // Проверка переборов
                if (GetHandValue(_playerHand) >= 21) break;

                // Ход дилера
                if (dealerTurn && GetHandValue(_dealerHand) <= 17)
                {
                    _dealerHand.Add(_deck.Dequeue());
                }
                else
                {
                    dealerTurn = false;
                }

                // Проверка переборов
                if (GetHandValue(_dealerHand) >= 21) break;
            }

            // Финализация игры
            int playerValue = GetHandValue(_playerHand);
            int dealerValue = GetHandValue(_dealerHand);
            string result = GetGameStatus() + "\nИтог: ";

            if (playerValue > 21 || (dealerValue <= 21 && dealerValue > playerValue))
            {
                OnLoseInvoke(result + "Поражение");
            }
            else if (playerValue == dealerValue)
            {
                OnDrawInvoke(result + "Ничья");
            }
            else
            {
                OnWinInvoke(result + "Победа!");
            }
        }

        private string GetGameStatus()
        {
            return $"Ваши карты: {FormatHand(_playerHand)} ({GetHandValue(_playerHand)})\n" +
                   $"Карты дилера: {FormatHand(_dealerHand)} ({GetHandValue(_dealerHand)})";
        }

        private static string FormatHand(IEnumerable<Card> hand)
        {
            return string.Join(" ", hand.Select(c => c.ToString()));
        }
    }
}
