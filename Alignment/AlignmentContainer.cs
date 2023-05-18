using UnityEngine;

public class AlignmentContainer : MonoBehaviour
{
	[SerializeField] private EmptyScriptableObject alignmentContainer;

	public EmptyScriptableObject Alignment => alignmentContainer;
}