using System.Collections;
using UnityEngine;

public class ElementalProtectionEffect : StatusEffect
{
    public ElementType element;

    public override void Initialize(CardInstance targetUnit, StatusEffect origin, int power)
    {
        base.Initialize(targetUnit, origin, power);
        ElementalProtectionEffect originEffect = origin as ElementalProtectionEffect;

        target = targetUnit;
        EffectsManager.instance.CreateFloatingText(target.transform.position, "protect", Color.black);
        element = originEffect.element;
    }
    public override IEnumerator OnTurnStartCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        duration--;
        if (duration <= 0)
        {
            OnExpire();
            Destroy(this);
        }
    }
    public override void Reapply(StatusEffect newEffect, int power)
    {

    }

    protected override void OnExpire()
    {
        base.OnExpire();
    }
}
