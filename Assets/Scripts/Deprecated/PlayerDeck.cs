using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    // old iteratrion! Deprecated!
    [Header("Deck Setup")]
    [SerializeField] private List<Card> cards = new List<Card>();
    [SerializeField] private GameObject cardPrefab; // Prefab containing CardInstance
    [SerializeField] private PlayerHand playerHand; // Reference to player's hand

    private System.Random rng = new System.Random();

    private void Start()
    {
        Shuffle();
    }

    public void Shuffle()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }
    }

    public void Initialize(List<Card> deckList, PlayerHand ownerHand)
    {
        cards = new List<Card>(deckList);
        playerHand = ownerHand;
        Shuffle();
    }
}
