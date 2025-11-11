using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsField : MonoBehaviour
{
    [Header("Field Settings")]
    public List<Transform> fieldPositions = new List<Transform>();
    public Transform spawnPoint;
    public float moveDuration = 0.5f;

    private List<CardInstance> cardsOnField = new List<CardInstance>();
    private Dictionary<CardInstance, Transform> cardToPosition = new Dictionary<CardInstance, Transform>();
    private HashSet<Transform> occupiedPositions = new HashSet<Transform>();

    public void AddCard(CardInstance card)
    {
        if (card == null)
            return;

        Transform freeSlot = FindFreePosition();
        if (freeSlot == null)
        {
            Debug.LogWarning("No free position available on TroopsField!");
            return;
        }

        cardsOnField.Add(card);
        cardToPosition[card] = freeSlot;
        occupiedPositions.Add(freeSlot);

        // Place card at spawn point and animate move to target
        card.transform.position = spawnPoint.position;
        StartCoroutine(MoveCardToPosition(card, freeSlot.position));
    }

    public void RemoveCard(CardInstance card)
    {
        if (card == null)
            return;

        if (cardToPosition.ContainsKey(card))
        {
            occupiedPositions.Remove(cardToPosition[card]);
            cardToPosition.Remove(card);
        }

        cardsOnField.Remove(card);
    }
    private Transform FindFreePosition()
    {
        foreach (var pos in fieldPositions)
        {
            if (!occupiedPositions.Contains(pos))
                return pos;
        }
        return null;
    }

    private IEnumerator MoveCardToPosition(CardInstance card, Vector3 targetPosition)
    {
        Vector3 startPosition = card.transform.position;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            card.transform.position = Vector3.Lerp(startPosition, targetPosition, smoothT);
            yield return null;
        }

        card.transform.position = targetPosition;
    }

    public List<CardInstance> GetCards()
    {
        return cardsOnField;
    }
    //private void UpdateCardPositions()
    //{
    //    if (cardsOnField.Count == 0)
    //        return;

    //    float totalWidth = (cardsOnField.Count - 1) * cardSpacing;
    //    Vector3 startPos = transform.position - new Vector3(totalWidth / 2f, 0, 0);

    //    for (int i = 0; i < cardsOnField.Count; i++)
    //    {
    //        Vector3 targetPos = startPos + new Vector3(i * cardSpacing, 0, 0);
    //        StartCoroutine(MoveCard(cardsOnField[i], targetPos, moveDuration));
    //    }
    //}

    private IEnumerator MoveCard(CardInstance card, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = card.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            card.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        card.transform.position = targetPosition;
    }
}
