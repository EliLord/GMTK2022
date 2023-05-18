using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponUI : MonoBehaviour
{
	[SerializeField] private Image image;
	[SerializeField] private TMP_Text damageText;
	[SerializeField] private Image inUseBackground;

	private WeaponInfo _weaponInfo;

	public WeaponInfo WeaponInfo
	{
		get => _weaponInfo;

		set
		{
			_weaponInfo = value;
			UpdateWeaponInfo();
		}
	}

	public void SetInUse(bool inUse)
	{
		inUseBackground.enabled = inUse;
	}

	private void UpdateWeaponInfo()
	{
		image.sprite = WeaponInfo.image;
		damageText.text = WeaponInfo.damage.ToString();
	}
}