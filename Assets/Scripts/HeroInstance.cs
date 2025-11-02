using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroInstance : CardInstance
{
    public HeroCard heroData;
    public bool HasActedThisTurn { get; set; }

    private List<EnchantmentCard> activeEnchantments = new();

    public override void UpdateVisuals()
    {
        base.UpdateVisuals();
    }
    //protected override void HandleLeftClick()
    //{
    //    base.HandleLeftClick();
    //    if (state == CardState.OnField)
    //    {
    //        if (troopsField.CompareTag("PlayerField"))
    //        {
    //            if (HasAttackedThisTurn)
    //            {
    //                return;
    //            }

    //            SelectAsAttacker();
    //        }
    //        else // Enemy card clicked
    //        {
    //            if (selectedAttacker != null)
    //            {
    //                StartCoroutine(selectedAttacker.PerformAttack(this));
    //            }
    //        }
    //    }
    //}
    public override void SelectAsAttacker()
    {
        base.SelectAsAttacker();
        GameManager.Instance.SelectHero(this);
        Debug.Log("Hero Selected");
    }

    public void ApplyEnchantment(EnchantmentCard enchantment)
    {
        activeEnchantments.Add(enchantment);
        Debug.Log($"{heroData.cardName} received enchantment: {enchantment.cardName}");
        ApplyEffect(enchantment);
    }

    private void ApplyEffect(EnchantmentCard enchantment)
    {
        UpdateVisuals();
    }

    public void PerformAction(HeroActionType actionType)
    {
        // To be implemented next
        Debug.Log($"{heroData.cardName} performs {actionType}");
        HasActedThisTurn = true;
    }
}

public enum HeroActionType
{
    Attack,
    Cast,
    Defend
}
