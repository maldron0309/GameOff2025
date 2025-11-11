using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class BaseSkill : MonoBehaviour
{
    public string skillName;
    [TextArea(4,4)]
    public string description;
    public Sprite skillIcon;
    public List<ElementType> requiredElements = new List<ElementType>();

    public abstract void Execute();

    public bool Matches(List<ElementType> elements)
    {
        if (requiredElements.Count != elements.Count)
            return false;

        foreach (var elem in requiredElements)
            if (!elements.Contains(elem))
                return false;

        return true;
    }
}
