using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
	public static GameSystem Instance { get; private set; }

	public const string MenuName = "0b- Menu";

	protected void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != null)
		{
			Destroy(gameObject);
		}
	}

	public void LoadSceneByName(string scene) => SceneManager.LoadScene(scene);

	public void LoadSceneByIndex(int buildIndexNumber) => SceneManager.LoadScene(buildIndexNumber);

	public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	public void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	public void LoadMenu() => LoadSceneByName(MenuName);

	public void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
	}
}
