using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
	[SerializeField] private ScriptableEvent playerDeathEvent;

	private void Start()
	{
		playerDeathEvent.Event += OnPlayerDeath;
	}

	private void OnDestroy()
	{
		playerDeathEvent.Event -= OnPlayerDeath;
	}

	private void OnPlayerDeath()
	{
		StartCoroutine(DelayBeforeEnd());
	}

	private IEnumerator DelayBeforeEnd()
	{
		PlayerController.canMove = false;
		
		// TODO: Death animation.
		yield return new WaitForSeconds(2);

		SceneManager.LoadScene("Store");
	}
}