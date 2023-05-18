using UnityEngine;

public class WeaponController : MonoBehaviour
{
	[SerializeField] private MeleeWeaponInfo weaponInfo;
	
	private void OnTriggerEnter(Collider col)
	{
		Health targetHealth = col.gameObject.GetComponentInChildren<Health>();
		if (targetHealth != null) targetHealth.takeDamage(weaponInfo.damage);
	}
}