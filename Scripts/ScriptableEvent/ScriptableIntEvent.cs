using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScriptableIntEvent", menuName = "ScriptableObjects/ScriptableIntEvent", order = 1)]
public class ScriptableIntEvent : ScriptableObject
{
	public event Action<int> Event;

	public void Invoke(int value)
	{
		Event?.Invoke(value);
	}
}