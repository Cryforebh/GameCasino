namespace GameCasino.Enum.TranslatedEnumName
{
    public static class CardName
    {
        public static string GetNameSuit(CardSuit suit)
        {
            switch (suit)
            {
                case CardSuit.Clubs:
                    return "Крести";
                case CardSuit.Diamonds:
                    return "Буби";
                case CardSuit.Hearts:
                    return "Черви";
                case CardSuit.Spades:
                    return "Пики";
                default:
                    return string.Empty;
            }
        }

        public static string GetNameRank(CardRank rank)
        {
            switch (rank)
            {
                case CardRank.Ace:
                    return "Туз";
                case CardRank.Two:
                    return "2";
                case CardRank.Three:
                    return "3";
                case CardRank.Four:
                    return "4";
                case CardRank.Five:
                    return "5";
                case CardRank.Six:
                    return "6";
                case CardRank.Seven:
                    return "7";
                case CardRank.Eight:
                    return "8";
                case CardRank.Nine:
                    return "9";
                case CardRank.Ten:
                    return "10";
                case CardRank.Jack:
                    return "Валет";
                case CardRank.Queen:
                    return "Королева";
                case CardRank.King:
                    return "Король";
                default:
                    return string.Empty;
            }
        }
    }
}
