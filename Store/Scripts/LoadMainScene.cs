using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
	private AsyncOperation loadingMainScene;

	public void Load()
	{
		SceneManager.UnloadSceneAsync("Store");
		SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive).completed += Oncompleted;
	}

	private void Oncompleted(AsyncOperation obj)
	{
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));
	}
}