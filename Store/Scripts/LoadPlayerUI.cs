using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPlayerUI : MonoBehaviour
{
	public void Start()
	{
		SceneManager.LoadSceneAsync("PlayerUI", LoadSceneMode.Additive);
	}
}