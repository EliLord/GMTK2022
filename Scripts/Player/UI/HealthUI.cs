using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	[SerializeField] private ScriptableIntEvent healthChangedEvent;

	[SerializeField] private Image image;
	[SerializeField] private Sprite[] healthPointSprites;
	[SerializeField] private Image backgroundBorder;

	private WaitForSeconds flashDuration;

	private void Start()
	{
		healthChangedEvent.Event += HealthOnHealthChanged;

		flashDuration = new WaitForSeconds(0.25f);
	}

	private void OnDestroy()
	{
		healthChangedEvent.Event -= HealthOnHealthChanged;
	}

	private void HealthOnHealthChanged(int newHealth)
	{
		if (newHealth <= 0)
			return;
		
		image.sprite = healthPointSprites[newHealth - 1];

		StartCoroutine(BorderFlash());
	}

	private IEnumerator BorderFlash()
	{
		backgroundBorder.enabled = true;
		yield return flashDuration;

		backgroundBorder.enabled = false;
	}
}