using System.Collections;
using UnityEngine;

public class BurnEffect : StatusEffect
{
    public int damagePerTurn = 4;

    public override void Initialize(CardInstance targetUnit, StatusEffect origin, int power)
    {
        base.Initialize(targetUnit, origin, power);
        BurnEffect originEffect = (BurnEffect)origin;

        damagePerTurn = Mathf.RoundToInt(originEffect.damagePerTurn * power * 0.01f);
        target = targetUnit;
        EffectsManager.instance.CreateFloatingText(target.transform.position, "Burning", Color.black);
    }
    public override IEnumerator OnTurnStartCoroutine()
    {
        if (target == null)
            yield break;

        target.TakeDamage(damagePerTurn, ElementType.Fire);

        // add visual effect for posin here and wait a bit
        yield return new WaitForSeconds(0.3f);

        // Handle duration countdown and expiration
        duration--;
        if (duration <= 0)
        {
            OnExpire();
            Destroy(this);
        }
    }
    public override void Reapply(StatusEffect newEffect, int power)
    {
        duration++;

        BurnEffect newPoison = newEffect as BurnEffect;
        damagePerTurn += Mathf.RoundToInt(newPoison.damagePerTurn * power * 0.01f);
    }

    protected override void OnExpire()
    {
        base.OnExpire();
    }
}
