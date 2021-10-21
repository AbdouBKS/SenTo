using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitAction : IActionButton
{
    public override void Action()
    {
        Debug.Log("Quit Action button");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
