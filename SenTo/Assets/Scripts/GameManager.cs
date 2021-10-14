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

    public void RestartGame(float delay)
    {
        StartCoroutine(RestartGameCoroutine(delay));
    }

    public void WinGame(float delay)
    {
        StartCoroutine(WinGameCoroutine(delay));
    }

    private IEnumerator RestartGameCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameScene");
    }

    private IEnumerator WinGameCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("WinScene");
    }
}