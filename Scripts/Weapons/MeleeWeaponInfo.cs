using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MeleeWeaponInfo", menuName = "ScriptableObjects/MeleeWeaponInfo", order = 1)]
public class MeleeWeaponInfo : WeaponInfo
{
    public float hitboxDuration, hitboxDelay;
    public string animationStyle;
}
