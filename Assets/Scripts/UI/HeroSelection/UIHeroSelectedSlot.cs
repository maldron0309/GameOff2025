using GameOff2025.Assets.Scripts.UI.HeroSelection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroSelectedSlot : UIHeroSelectionSlot
{
    public TextMeshProUGUI unselectSlot;
    protected override void Awake()
    {
        base.Awake();
    }
    public override void ClickHeroSelection()
    {
        if (isSelected)
        {
            base.ClickHeroSelection();
        }
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public void MarkAsSelected(HeroInfoModel hero)
    {
        unselectSlot.enabled = false;
        isSelected = true;
        iconImage.sprite = hero.heroIcon.sprite;
        iconImage.enabled = true;
        heroName = hero.heroName;
        descriptionText = hero.heroDescription;
        heroPrefab = hero.heroPrefab;
    }
    public void UnmarkAsSelected()
    {
        unselectSlot.enabled = true;
        isSelected = false;
        iconImage.enabled = false;
        heroName = string.Empty;
        descriptionText = string.Empty;
        heroPrefab = null;
    }
    public bool IsSelected()
    {
        return isSelected;
    }
    public string GetHeroName()
    {
        return heroName;
    }
    public GameObject GetHeroPrefab()
    {
        return heroPrefab;
    }
}
