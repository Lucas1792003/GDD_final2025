using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene Names (exact file names without .unity)")]
    [SerializeField] string flyingScene;
    [SerializeField] string ballScene;
    [SerializeField] string foodScene;

    public void PlayFlying() => Load(flyingScene);
    public void PlayBall() => Load(ballScene);
    public void PlayFood() => Load(foodScene);

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    static void Load(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
