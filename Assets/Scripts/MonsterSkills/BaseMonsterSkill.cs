using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseMonsterSkill : MonoBehaviour
{
    public string skillName;
    public string skillDescription;
    public Sprite idleSprite;
    public Sprite actingSprite;

    public abstract void Execute();
}
