using UnityEngine;
using UnityEngine.Events;

public class ScriptableEventOnUnityEvent : MonoBehaviour
{
	[SerializeField] private UnityEvent unityEvent;

	[SerializeField] private ScriptableEvent[] scriptableEvents;


	private void Awake()
	{
		foreach (ScriptableEvent scriptableEvent in scriptableEvents)
			scriptableEvent.Event += OnEvent;
	}

	private void OnDestroy()
	{
		foreach (ScriptableEvent scriptableEvent in scriptableEvents)
			scriptableEvent.Event -= OnEvent;
	}

	private void OnEvent()
	{
		unityEvent.Invoke();
	}
}