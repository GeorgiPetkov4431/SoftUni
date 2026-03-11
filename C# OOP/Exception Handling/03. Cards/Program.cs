using System;
using System.Collections.Generic;
using System.Linq;

namespace Cards
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] cardsInfo = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);
            List<Card> cards = new List<Card>();

            foreach (var card in cardsInfo)
            {
                string[] cardInfo = card.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string face = cardInfo[0];
                string suit = cardInfo[1];

                try
                {
                    Card currentCard = new Card(face, suit);
                    cards.Add(currentCard);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine(string.Join(' ', cards));
        }

        public class Card
        {
            private static string[] validFaces = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            private static string[] validSuits = { "S", "H", "D", "C" };

            public Card(string face, string suit)
            {
                this.ValidateFace(face);
                this.ValidateSuit(suit);

                this.Face = face;
                this.Suit = suit;
            }

            public string Face { get; private set; }
            public string Suit { get; private set; }

            private void ValidateFace(string face)
            {
                if (!validFaces.Contains(face))
                {
                    throw new Exception("Invalid card!");
                }
            }

            public void ValidateSuit(string suit)
            {
                if (!validSuits.Contains(suit))
                {
                    throw new Exception("Invalid card!");
                }
            }

            public override string ToString()
            {
                var suitSymbol = string.Empty;

                switch (this.Suit)
                {
                    case "S":
                        suitSymbol = "\u2660";
                        break;
                    case "H":
                        suitSymbol = "\u2665";
                        break;
                    case "D":
                        suitSymbol = "\u2666";
                        break;
                    case "C":
                        suitSymbol = "\u2663";
                        break;
                }

                return $"[{this.Face}{suitSymbol}]";
            }
        }
    }
}
