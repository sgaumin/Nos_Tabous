using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
	public static GameSystem Instance { get; private set; }

	public const string MenuName = "0b- Menu";
	public const string CreditsName = "0c- Credits";

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

	public void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	public void LoadGame()
	{
		Destroy(MusicPlayer.Instance.gameObject);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadMenu()
	{
		DOTween.KillAll();
		if (AudioManagerClic.Instance != null)
		{
			Destroy(AudioManagerClic.Instance.gameObject);
		}
		LoadSceneByName(MenuName);
	}

	public void LoadCredits() => LoadSceneByName(CreditsName);

	public void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
		Application.Quit();
#endif
	}
}
