using System.Collections;
using UnityEngine;

public class RegenerateHealthSkill : StatusEffect
{
    public int regenAmount = 5;

    public override void Initialize(CardInstance targetUnit, StatusEffect origin)
    {
        base.Initialize(targetUnit, origin);
        RegenerateHealthSkill originEffect = origin as RegenerateHealthSkill;

        regenAmount = originEffect.regenAmount;
        target = targetUnit;
        EffectsManager.instance.CreateFloatingText(target.transform.position, "Regeneration", Color.black);
    }
    public override IEnumerator OnTurnStartCoroutine()
    {
        if (target == null)
            yield break;

        target.Heal(regenAmount);

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
    public override void Reapply(StatusEffect newEffect)
    {
        duration = Mathf.Max(duration, newEffect.duration);

        RegenerateHealthSkill newPoison = newEffect as RegenerateHealthSkill;
        regenAmount += newPoison.regenAmount;
    }

    protected override void OnExpire()
    {
        base.OnExpire();
    }
}
