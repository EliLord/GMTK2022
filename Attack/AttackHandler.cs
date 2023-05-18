using System.Collections;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
	[SerializeField] private Animator wepAnim;

	public GameObject weaponInstance;

	private void Start()
	{
		if (wepAnim == null)
			wepAnim = GetComponent<Animator>();
	}

	public void Attack(WeaponInfo weapon, GameObject launcher)
	{
		if (weapon is MeleeWeaponInfo meleeWeapon)
			MeleeAttack(meleeWeapon, launcher);
		else if (weapon is RangedWeaponInfo rangedWeapon)
			RangedAttack(rangedWeapon, launcher);
	}

	private void MeleeAttack(MeleeWeaponInfo weapon, GameObject launcher)
	{
		StartCoroutine(MeleeAttackSequence(weapon, launcher));
	}

	private void RangedAttack(RangedWeaponInfo weapon, GameObject launcher)
	{
		StartCoroutine(RangedAttackSequence(weapon, launcher));
	}

	private IEnumerator MeleeAttackSequence(MeleeWeaponInfo weapon, GameObject launcher)
	{
		//weapon.weaponPrefab.GetComponent<WeaponController>().weaponInfo = weapon;
		wepAnim.SetTrigger(weapon.animationStyle);
		yield return new WaitForSeconds(weapon.hitboxDelay);
		weaponInstance.GetComponent<Collider>().enabled = true;
		yield return new WaitForSeconds(weapon.hitboxDuration);
		weaponInstance.GetComponent<Collider>().enabled = false;
	}

	private IEnumerator RangedAttackSequence(RangedWeaponInfo weapon, GameObject launcher)
	{
		yield return new WaitForSeconds(0f);
		GameObject proj = Instantiate(weapon.projectile,
			weaponInstance.GetComponent<ProjectileSpawn>().transform.position, launcher.transform.rotation);
		proj.GetComponent<ProjectileController>().WeaponInfo = weapon;
	}
}