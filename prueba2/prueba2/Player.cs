using System.Collections.Generic;

public class Player
{
    public string Name { get; private set; }
    public List<Card> Hand { get; private set; }
    public int Score { get; private set; }

    public Player(string name)
    {
        Name = name;
        Hand = new List<Card>();
        Score = 0;
    }

    public void AddCardToHand(Card card)
    {
        if (card != null)
        {
            Hand.Add(card);
            Score += card.Value;
        }
    }

    public void ResetHand()
    {
        Hand.Clear();
        Score = 0;
    }

    public override string ToString()
    {
        return $"{Name} - Puntuación: {Score}";
    }
}
