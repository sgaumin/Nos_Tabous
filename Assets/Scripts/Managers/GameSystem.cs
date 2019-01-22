using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    // Singleton
    public static GameSystem instance = null;

    // State of the Game
    public enum gameStates { Playing, Pause, End };
    public gameStates gameState = gameStates.Playing;

    // To Update according to main Menu name scene
    public const string MenuName = "0b- Menu";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

       // DontDestroyOnLoad(gameObject);
    }

    // Allow to load a scene by its name
    public void LoadSceneByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    // Allow to load a scene by its name
    public void LoadSceneByIndex(int buildIndexNumber)
    {
        SceneManager.LoadScene(buildIndexNumber);
    }

    // Relaod the actual scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //  Play the next scene present in the build
    public void PlayNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMenu()
    {
        LoadSceneByName(MenuName);
    }

    // Quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
