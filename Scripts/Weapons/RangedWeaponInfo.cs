using UnityEngine;

[CreateAssetMenu(fileName = "New RangedWeaponInfo", menuName = "ScriptableObjects/RangedWeaponInfo", order = 1)]
public class RangedWeaponInfo : WeaponInfo
{
	public float velocity;
	public GameObject projectile;
	public Transform projectileSpawn;
}