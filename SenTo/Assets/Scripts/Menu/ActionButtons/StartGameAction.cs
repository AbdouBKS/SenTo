using UnityEngine.SceneManagement;

public class StartGameAction : IActionButton
{
    public override void Action()
    {
        SceneManager.LoadScene("GameScene");
    }
}
