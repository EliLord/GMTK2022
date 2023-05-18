using UnityEngine;

//[CreateAssetMenu(fileName = "New WeaponInfo", menuName = "ScriptableObjects/WeaponInfo", order = 1)]
public class WeaponInfo : ScriptableObject
{
	public int cost;
	public int damage;
	public float range, cooldown, animationDuration,preAttackDelay;
	public Sprite image;
	public GameObject weaponPrefab;
}