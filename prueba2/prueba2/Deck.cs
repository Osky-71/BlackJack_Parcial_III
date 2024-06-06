using System;
using System.Collections.Generic;

public class Deck
{
    private List<Card> cards;
    private Random random;

    public Deck()
    {
        cards = new List<Card>();
        random = new Random();
        string[] suits = { "Corazones", "Diamantes", "Tréboles", "Picas" };
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jota", "Reina", "Rey", "As" };
        int[] values = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };

        foreach (var suit in suits)
        {
            for (int i = 0; i < ranks.Length; i++)
            {
                cards.Add(new Card(suit, ranks[i], values[i]));
            }
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = random.Next(cards.Count);
            Card temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            return null;
        }

        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }

    public int CardsRemaining()
    {
        return cards.Count;
    }
}
