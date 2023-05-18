using UnityEngine;

public class Health : MonoBehaviour
{
	public int currentHealth;

	[SerializeField] private ScriptableEvent deathEvent;
	[SerializeField] private ScriptableIntEvent healthChangedEvent;


	public void takeDamage(int damage)
	{
		if (currentHealth <= 0)
			return;
		
		currentHealth -= damage;

		healthChangedEvent.Invoke(currentHealth);

		if (currentHealth <= 0)
		{
			if (deathEvent != null)
				deathEvent.Invoke();
			Destroy(gameObject);
		}
	}
}