using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private HeroInstance heroPrefab;
    [SerializeField] private PlayerDeck playerDeck;
    public PlayerHand playerHand;
    public TroopsField playerField;
    [SerializeField] private TroopsField enemyField;
    [SerializeField] private WaveSpawner enemySpawner;
    [SerializeField] private PlayerInputController playerInput;
    [SerializeField] private List<HeroCard> startingHeroes;

    [Header("Turn Settings")]
    [SerializeField] private float enemyTurnDuration = 2f;

    public bool PlayerInputEnabled => playerInput.InputEnabled;

    private enum GamePhase { None, GameStart, PlayerTurn, EnemyTurn }
    private GamePhase currentPhase = GamePhase.None;
    private int currentTurn = 0;
    private HeroInstance selectedHero;
    public List<HeroInstance> PlayerHeroes { get; private set; } = new();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(GameStartRoutine());
        //StartCoroutine(GameLoop());
    }
    private IEnumerator GameStartRoutine()
    {
        // Spawn heroes
        for (int i = 0; i < startingHeroes.Count; i++)
        {
            HeroInstance hero = Instantiate(heroPrefab, playerField.transform);
            hero.SetCardData(startingHeroes[i]);
            hero.troopsField = playerField;
            PlayerHeroes.Add(hero);
            playerField.AddCardRepresentation(hero.gameObject);
            yield return new WaitForSeconds(0.1f);
        }

        // Spawn enemy wave
        yield return enemySpawner.SpawnWaveCoroutine();

        // Draw starting hand
        for (int i = 0; i < 5; i++)
        {
            playerDeck.Draw();
            yield return new WaitForSeconds(0.2f);
        }

        StartCoroutine(PlayerTurnRoutine());
    }
    public void SelectHero(HeroInstance hero)
    {
        selectedHero = hero;
    }
    public HeroInstance GetSelectedHero()
    {
        return selectedHero;
    }

    private IEnumerator GameLoop()
    {
        playerInput.SetInputEnabled(false);

        // Start enemy wave
        yield return StartCoroutine(enemySpawner.SpawnWaveCoroutine());

        // Player draws 5 cards
        for (int i = 0; i < 5; i++)
        {
            playerDeck.Draw();
            yield return new WaitForSeconds(0.2f);
        }

        StartCoroutine(PlayerTurnRoutine());
    }

    private IEnumerator PlayerTurnRoutine()
    {
        currentTurn++;
        Debug.Log($"--- Player Turn {currentTurn} ---");

        ResetAllAttacks();
        if (currentTurn > 1)
        {
            playerDeck.Draw();
        }

        SetPlayerInput(true);
        yield return new WaitUntil(() => playerInput.EndTurnPressed);
        playerInput.EndTurnPressed = false;

        SetPlayerInput(false);
        StartCoroutine(EnemyTurnRoutine());
    }

    private IEnumerator EnemyTurnRoutine()
    {
        Debug.Log("--- Enemy Turn ---");

        List<CardInstance> enemyCards = enemyField.GetCards();
        List<CardInstance> playerCards = playerField.GetCards();

        foreach (var enemyCard in enemyCards)
        {
            if (enemyCard == null) continue;
            if (playerCards.Count == 0) break;

            CardInstance target = playerCards[Random.Range(0, playerCards.Count)];
            yield return enemyCard.StartCoroutine(enemyCard.PerformAttack(target));
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(enemyTurnDuration);
        StartCoroutine(PlayerTurnRoutine());
    }

    private void ResetAllAttacks()
    {
        foreach (var c in playerField.GetCards())
        {
            if (c != null) c.HasAttackedThisTurn = false;
        }
    }

    public void SetPlayerInput(bool enabled)
    {
        playerInput.SetInputEnabled(enabled);
    }
}
