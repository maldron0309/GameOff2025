using UnityEngine;

public class EffectSpawnIn : MonoBehaviour
{
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.Play("Spawn");
    }
}
