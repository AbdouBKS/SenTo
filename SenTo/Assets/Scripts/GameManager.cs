using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public void resetVariables()
    {
        PlayerVariables.health = 3;
    }

    public void startScene(string levelName, int levelIndex, float delay)
    {
        StartCoroutine(GameCoroutine(levelName, levelIndex, delay));
    }

    private IEnumerator GameCoroutine(string levelName, int levelIndex, float delay)
    {
        MenuManager.sceneNumber = levelIndex;
        yield return new WaitForSeconds(delay);

        if (string.Equals(levelName, StrRepo.winScene)
            || string.Equals(levelName, StrRepo.menuScene)
            || string.Equals(levelName, StrRepo.gameOverScene))
        {
            resetVariables();
        }

        SceneManager.LoadScene(levelName);
    }
}