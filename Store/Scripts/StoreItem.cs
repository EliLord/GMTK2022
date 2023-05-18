using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
	[SerializeField] private Sprite purchasedImage;
	[SerializeField] private Image image;
	[SerializeField] private TMP_Text costText;
	[SerializeField] private TMP_Text damageText;
	[SerializeField] private List<GameObject> deactivateWhenBought;

	private WeaponInfo _weaponInfo;

	private bool purchased;

	public event Action<StoreItem> TryPurchased;

	public WeaponInfo WeaponInfo
	{
		get => _weaponInfo;

		set
		{
			_weaponInfo = value;
			UpdateWeaponInfo();
		}
	}

	public void TryPurchase()
	{
		if (!purchased)
			TryPurchased?.Invoke(this);
	}

	public void Purchase()
	{
		purchased = true;
		ClearWeaponGraphic();
	}

	private void UpdateWeaponInfo()
	{
		image.sprite = WeaponInfo.image;
		costText.text = WeaponInfo.cost.ToString();
		damageText.text = WeaponInfo.damage.ToString();
	}

	private void ClearWeaponGraphic()
	{
		image.sprite = purchasedImage;
		foreach (GameObject objectToDeactivate in deactivateWhenBought)
			objectToDeactivate.SetActive(false);
	}
}