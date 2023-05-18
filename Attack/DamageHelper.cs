using UnityEngine;

public static class DamageHelper
{
	public static void TryDamage(GameObject othersObject, EmptyScriptableObject alignment, int damage)
	{
		Health targetHealth = othersObject.GetComponentInChildren<Health>();

		bool isAllied = Allied(othersObject, alignment);

		if (targetHealth != null && !isAllied)
			targetHealth.takeDamage(damage);
	}

	public static bool Allied(GameObject othersObject, EmptyScriptableObject alignment)
	{
		AlignmentContainer othersAlignment = othersObject.GetComponent<AlignmentContainer>();
		return othersAlignment != null && othersAlignment.Alignment == alignment;
	}
}