using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackSkill : BaseSkill
{
    [Header("Skill Projectile Prefabs")]
    public GameObject formedProjectilePrefab; // The final projectile prefab

    [Header("Projectile Timing")]
    public float elementalRiseHeight = 2.0f;
    public float elementalLaunchDuration = 0.4f;
    public float mergeDelay = 0.3f;
    public float travelDuration = 0.4f;
    public float impactDelay = 0.1f;
    public int baseDamage = 5;
    public ElementType damageType;
    public ElementIconLibrary elementsLib;
    public GameObject onHitEffect;
    [Header("Status Effect")]
    public StatusEffect statusEffect;   // The effect to apply
    [Range(0, 100)]
    public int chanceToProc = 0;        // % chance to apply
    public override void Execute()
    {
        GameManager.Instance.StartCoroutine(WaitForTargetAndAttack());
    }

    private IEnumerator WaitForTargetAndAttack()
    {
        GameManager.Instance.SetPlayerInput(false);

        Debug.Log("Select enemy target...");
        GameManager.Instance.SelectedTarget = null;
        InfoPanel.instance.ShowMessage("Select enemy as target...");

        yield return new WaitUntil(() => GameManager.Instance.SelectedTarget != null);

        InfoPanel.instance.Hide();
        var target = GameManager.Instance.SelectedTarget;
        GameManager.Instance.SelectTarget(null);

        // Example: perform attack animation
        Debug.Log($"Attacking {target.name}!");

        yield return GameManager.Instance.StartCoroutine(PerformAttackVisuals(target));

        target.TakeDamage(baseDamage, damageType);
        if (statusEffect != null)
        {
            int roll = Random.Range(0, 100);
            if (roll < chanceToProc)
            {
                Debug.Log($"Applying {statusEffect.effectName} to {target.name} ({roll}% < {chanceToProc}%)");
                target.AddStatusEffect(statusEffect);
            }
            else
            {
                Debug.Log($"Effect {statusEffect.effectName} did not proc ({roll}% >= {chanceToProc}%)");
            }
        }

        if (onHitEffect)
            Instantiate(onHitEffect, target.transform.position, Quaternion.identity);

        GameManager.Instance.SetPlayerInput(true);
        GameManager.Instance.RegisterActionUse();
    }
    private IEnumerator PerformAttackVisuals(CardInstance target)
    {
        yield return GameManager.Instance.StartCoroutine(
            PerformElementalLaunches(elementsLib, requiredElements, elementalRiseHeight, elementalLaunchDuration, mergeDelay)
        );

        if (formedProjectilePrefab != null)
        {
            GameObject formedProjectile = Instantiate(formedProjectilePrefab, mergePoint, Quaternion.identity);

            yield return MoveProjectile(formedProjectile, target.transform.position, travelDuration);

            // Impact delay before damage application
            yield return new WaitForSeconds(impactDelay);

            Destroy(formedProjectile);
        }
        else
        {
            Debug.LogError("Formed projectile prefab missing in AttackSkill!");
        }
    }
}
