using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.GameObject.CardFiles
{
    public enum CardSuit { Diamonds, Hearts, Clubs, Spades }
    public enum CardRank { Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    public struct Card
    {
        public CardSuit Suit { get; }
        public CardRank Rank { get; }

        public Card(CardSuit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }
}
