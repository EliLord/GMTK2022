using UnityEngine;

public class ProjectileController : MonoBehaviour
{
	[SerializeField] private EmptyScriptableObject alignment;
	public RangedWeaponInfo WeaponInfo { private get; set; }

	private float distanceTraveled;

	private Health targetHealth;

	private Transform myTransform;

	private void Start()
	{
		myTransform = transform;
	}

	private void Update()
	{
		if (distanceTraveled < WeaponInfo.range)
		{
			myTransform.position += WeaponInfo.velocity * Time.deltaTime * myTransform.forward;
			distanceTraveled += WeaponInfo.velocity * Time.deltaTime;
		}
		else
			Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider col)
	{
		DamageHelper.TryDamage(col.gameObject, alignment, WeaponInfo.damage);

		bool isAllied = DamageHelper.Allied(col.gameObject, alignment);

		if (!isAllied)
			Destroy(gameObject);
	}
}