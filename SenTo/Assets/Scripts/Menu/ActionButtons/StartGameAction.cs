using UnityEngine.SceneManagement;

public class StartGameAction : IActionButton
{
    public override void Action()
    {
        PlayerVariables.health = 3;
        SceneManager.LoadScene("GameScene");
    }
}
