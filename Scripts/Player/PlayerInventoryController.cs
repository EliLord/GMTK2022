using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
	[SerializeField] private ScriptableIntEvent onNewWeaponRolled;
	[SerializeField] private Transform[] inventoryParents;
	private GameObject[] weaponObjects;

	public AttackHandler attackHandler;

	private void Start()
	{
		PlayerLoadoutUI.Instance.LoadoutFinalized += OnLoadoutFinalized;
	}

	private void OnDestroy()
	{
		PlayerLoadoutUI.Instance.LoadoutFinalized -= OnLoadoutFinalized;
	}

	private void OnLoadoutFinalized()
	{
		weaponObjects = new GameObject[inventoryParents.Length];
		for (int index = 0; index < inventoryParents.Length; index++)
		{
			WeaponInfo weaponInfo = PlayerLoadoutUI.Instance.Loadout[index].WeaponInfo;
			weaponObjects[index] = Instantiate(weaponInfo.weaponPrefab, inventoryParents[index]);
			inventoryParents[index].gameObject.SetActive(false);
		}

		RollNewWeapon();
	}

	public void RollNewWeapon()
	{
		int newWeaponIndex = Random.Range(0, inventoryParents.Length);

		foreach (Transform inventoryParent in inventoryParents)
			inventoryParent.gameObject.SetActive(false);

		inventoryParents[newWeaponIndex].gameObject.SetActive(true);
		PlayerLoadoutUI.Instance.SetNewWeaponIndex(newWeaponIndex);
		attackHandler.weaponInstance = weaponObjects[newWeaponIndex];

		onNewWeaponRolled.Invoke(newWeaponIndex);
		// TODO: Swap to weapon in other ways.
	}
}