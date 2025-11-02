using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [Header("Hand Layout Settings")]
    public float cardSpacing = 2.0f;       // distance between cards
    public float handHeightOffset = 0f;    // vertical offset
    public float moveDuration = 0.5f;      // how long the motion lasts
    public TroopsField troopsField;

    private List<CardInstance> cardsInHand = new List<CardInstance>();
    private Dictionary<CardInstance, Vector3> targetPositions = new Dictionary<CardInstance, Vector3>();

    public void AddCard(CardInstance card)
    {
        cardsInHand.Add(card);
        card.troopsField = troopsField;
        RecalculateTargetPositions();
        StopAllCoroutines();
        StartCoroutine(SmoothMoveAllCards());
    }

    public void RemoveCard(CardInstance card)
    {
        cardsInHand.Remove(card);
        RecalculateTargetPositions();
        StopAllCoroutines();
        StartCoroutine(SmoothMoveAllCards());
    }

    private void RecalculateTargetPositions()
    {
        targetPositions.Clear();

        if (cardsInHand.Count == 0)
            return;

        float totalWidth = (cardsInHand.Count - 1) * cardSpacing;
        float startX = transform.position.x - totalWidth / 2;

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            Vector3 targetPos = new Vector3(
                startX + i * cardSpacing,
                transform.position.y + handHeightOffset,
                0f
            );

            targetPositions[cardsInHand[i]] = targetPos;
        }
    }

    private System.Collections.IEnumerator SmoothMoveAllCards()
    {
        float elapsed = 0f;
        Dictionary<CardInstance, Vector3> startPositions = new Dictionary<CardInstance, Vector3>();

        // Cache starting positions
        foreach (var card in cardsInHand)
        {
            startPositions[card] = card.transform.position;
        }

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);

            foreach (var card in cardsInHand)
            {
                if (card == null) continue;
                if (!targetPositions.ContainsKey(card)) continue;

                Vector3 start = startPositions[card];
                Vector3 end = targetPositions[card];
                card.transform.position = Vector3.Lerp(start, end, t);
            }

            yield return null;
        }

        // Snap to final positions
        foreach (var card in cardsInHand)
        {
            if (card == null) continue;
            if (targetPositions.ContainsKey(card))
                card.transform.position = targetPositions[card];
        }
    }

    public List<CardInstance> GetCards()
    {
        return cardsInHand;
    }
}
