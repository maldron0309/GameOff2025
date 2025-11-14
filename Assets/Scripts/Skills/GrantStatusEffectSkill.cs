using System.Collections;
using UnityEngine;

public class GrantStatusEffectSkill : BaseSkill
{
    [Header("Skill Visuals")]
    public GameObject effectPrefab;
    public ElementIconLibrary elementsLib;

    [Header("Projectile Timing")]
    public float elementalRiseHeight = 2.0f;
    public float elementalLaunchDuration = 0.4f;
    public float mergeDelay = 0.3f;
    public float effectDuration = 0.6f;
    public StatusEffect statusEffect;

    public override void Execute()
    {
        GameManager.Instance.StartCoroutine(WaitForTargetAndHeal());
    }

    private IEnumerator WaitForTargetAndHeal()
    {
        GameManager.Instance.SetPlayerInput(false);

        Debug.Log("Select friendly target to heal...");
        GameManager.Instance.SelectedTarget = null;
        InfoPanel.instance.ShowMessage("Select ally to heal...");

        // Wait for player to select a friendly hero
        CardInstance target = null;
        while (target == null)
        {
            // Wait for any target to be selected
            yield return new WaitUntil(() => GameManager.Instance.SelectedTarget != null);

            CardInstance clickedTarget = GameManager.Instance.SelectedTarget;
            GameManager.Instance.SelectTarget(null);

            // Validate by tag
            if (clickedTarget != null && clickedTarget.CompareTag("Player"))
            {
                target = clickedTarget;
            }
            else
            {
                Debug.Log("Invalid target! You can only heal friendly units.");
                InfoPanel.instance.ShowMessage("Invalid target! Select a friendly unit to heal...");
                // Small delay to avoid instant re-trigger
                yield return new WaitForSeconds(0.1f);
            }
        }

        InfoPanel.instance.Hide();
        GameManager.Instance.SelectTarget(null);

        // Launch elemental projectiles above heroes
        yield return GameManager.Instance.StartCoroutine(
            PerformElementalLaunches(elementsLib, requiredElements, elementalRiseHeight, elementalLaunchDuration, mergeDelay)
        );

        // Spawn healing effect
        if (effectPrefab != null)
        {
            GameObject healFx = GameObject.Instantiate(effectPrefab, target.transform.position, Quaternion.identity);
            GameObject.Destroy(healFx, effectDuration);
        }

        // Apply healing
        if (target != null)
        {
            target.AddStatusEffect(statusEffect);
        }

        GameManager.Instance.SetPlayerInput(true);
        GameManager.Instance.RegisterActionUse();
    }
}
