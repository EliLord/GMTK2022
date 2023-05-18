using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Store : MonoBehaviour
{
	[SerializeField] private WeaponInfo[] weaponInfoPool;
	[SerializeField] private RectTransform[] storeItemParents;
	[SerializeField] private StoreItem storeItemPrefab;
	[SerializeField] private TMP_Text fundsText;
	

	private int funds = 1000;

	private void Start()
	{
		List<WeaponInfo> weaponInfoPoolCopy = new List<WeaponInfo>(weaponInfoPool);
		for (int i = 0; i < storeItemParents.Length; i++)
		{
			StoreItem newStoreItem = Instantiate(storeItemPrefab, storeItemParents[i]);
			newStoreItem.TryPurchased += TryPurchase;
			
			int randomWeaponIndex = Random.Range(0, weaponInfoPoolCopy.Count);
			newStoreItem.WeaponInfo = weaponInfoPoolCopy[randomWeaponIndex];
			weaponInfoPoolCopy.RemoveAt(randomWeaponIndex);
		}

		fundsText.text = funds.ToString();
	}

	private void TryPurchase(StoreItem storeItem)
	{
		if (funds >= storeItem.WeaponInfo.cost && !PlayerLoadoutUI.Instance.LoadoutFull)
		{
			funds -= storeItem.WeaponInfo.cost;
			fundsText.text = funds.ToString();
			
			storeItem.Purchase();
			PlayerLoadoutUI.Instance.AddWeaponToLoadout(storeItem.WeaponInfo);
		}
	}
}