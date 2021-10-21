using UnityEngine.SceneManagement;

public class GoToMenuAction : IActionButton
{
    public override void Action()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
