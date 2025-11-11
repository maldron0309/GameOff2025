using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCardUI : MonoBehaviour
{
    [Header("UI References")]
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Button useButton;

    private BaseSkill skill;

    public void Initialize(BaseSkill skill)
    {
        this.skill = skill;

        if (nameText != null)
            nameText.text = skill.skillName;

        if (descriptionText != null)
            descriptionText.text = skill.description;

        if (iconImage != null && skill.skillIcon != null)
            iconImage.sprite = skill.skillIcon;

        if (useButton != null)
            useButton.onClick.AddListener(OnUsePressed);
    }

    private void OnUsePressed()
    {
        // Hide all skill cards and execute the chosen one
        GameManager.Instance.OnSkillCardChosen(skill);
    }
}
