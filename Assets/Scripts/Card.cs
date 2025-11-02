using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card")]
public class Card : ScriptableObject
{
    [Header("Card Info")]
    public string cardName;
    public Sprite artwork;
    public int cost;
    public int attack;
    public int health;

    [TextArea]
    public string description;
}
