using GameCasino.Enum;
using GameCasino.Enum.TranslatedEnumName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.GameObject.CardFiles
{
    public class Card
    {
        public CardRank Rank { get; }
        public CardSuit Suit { get; }

        public Card(CardSuit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
        }
        public override string ToString() => $"{CardName.GetNameRank(Rank)}-{CardName.GetNameSuit(Suit)};";
    }
}
