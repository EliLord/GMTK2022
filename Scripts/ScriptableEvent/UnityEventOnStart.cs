using UnityEngine;
using UnityEngine.Events;

public class UnityEventOnStart : MonoBehaviour
{
	[SerializeField] private UnityEvent unityEvent;

	private void Start()
	{
		unityEvent.Invoke();
	}
}