using UnityEngine;

public class LockWorldRotation : MonoBehaviour
{
	private Quaternion initialWorldRotation;
	private Transform myTransform;

	private void Start()
	{
		myTransform = transform;
		initialWorldRotation = myTransform.rotation;
	}

	private void LateUpdate()
	{
		myTransform.rotation = initialWorldRotation;
	}
}