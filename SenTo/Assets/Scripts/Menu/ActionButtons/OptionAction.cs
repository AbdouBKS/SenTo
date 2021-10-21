using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionAction : IActionButton
{
    public override void Action()
    {
        Debug.Log("Option Action");
    }
}
