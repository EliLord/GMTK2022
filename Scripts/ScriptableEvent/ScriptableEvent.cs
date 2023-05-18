using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScriptableEvent", menuName = "ScriptableObjects/ScriptableEvent", order = 1)]
public class ScriptableEvent : ScriptableObject
{
	public event Action Event;

	public void Invoke()
	{
		Event?.Invoke();
	}
}