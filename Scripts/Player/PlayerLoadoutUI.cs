using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PlayerLoadoutUI : MonoBehaviour
{
	[SerializeField] private PlayerWeaponUI playerWeaponUI;
	[SerializeField] private WeaponInfo fistWeaponInfo;
	[SerializeField] private ScriptableIntEvent onNewWeaponRolled;

	[SerializeField] private Transform[] inventoryParents;

	public static PlayerLoadoutUI Instance;

	public event Action LoadoutFinalized;

	private List<PlayerWeaponUI> loadout;
	public ReadOnlyCollection<PlayerWeaponUI> Loadout { get; private set; }

	public PlayerWeaponUI CurrentWeaponUI => loadout[currentWeaponIndex];
	private int currentWeaponIndex = -1;

	private const int MAX_LOADOUT_SIZE = 6;

	public bool LoadoutFull => loadout.Count == MAX_LOADOUT_SIZE;

	public void SetNewWeaponIndex(int newIndex)
	{
		currentWeaponIndex = newIndex;
	}

	public void FinalizeLoadout()
	{
		int fistsToAdd = MAX_LOADOUT_SIZE - loadout.Count;
		for (int i = 0; i < fistsToAdd; i++)
			CreateWeaponUI(fistWeaponInfo);

		Loadout = new ReadOnlyCollection<PlayerWeaponUI>(loadout);
		
		LoadoutFinalized?.Invoke();
	}

	public void AddWeaponToLoadout(WeaponInfo weaponInfo)
	{
		if (loadout.Count < MAX_LOADOUT_SIZE)
			CreateWeaponUI(weaponInfo);
	}

	private void CreateWeaponUI(WeaponInfo weaponInfo)
	{
		Transform parentTransform = inventoryParents[loadout.Count];
		DestroyChildren(parentTransform);
		PlayerWeaponUI newInventoryWeapon = Instantiate(playerWeaponUI, parentTransform);
		newInventoryWeapon.WeaponInfo = weaponInfo;
		loadout.Add(newInventoryWeapon);
	}

	private void Awake()
	{
		onNewWeaponRolled.Event += OnNewWeaponRolledOnEvent;

		foreach (Transform inventoryParent in inventoryParents)
		{
			PlayerWeaponUI newFist = Instantiate(playerWeaponUI, inventoryParent);
			newFist.WeaponInfo = fistWeaponInfo;
		}

		loadout = new List<PlayerWeaponUI>(MAX_LOADOUT_SIZE);

		if (Instance == null)
			Instance = this;
	}

	private void OnDestroy()
	{
		onNewWeaponRolled.Event -= OnNewWeaponRolledOnEvent;

		if (Instance == this)
			Instance = null;
	}

	private void OnNewWeaponRolledOnEvent(int newWeaponIndex)
	{
		for (int i = 0; i < loadout.Count; i++)
			loadout[i].SetInUse(newWeaponIndex == i);
	}

	private void DestroyChildren(Transform parent)
	{
		foreach (Transform child in parent)
			Destroy(child.gameObject);
	}
}