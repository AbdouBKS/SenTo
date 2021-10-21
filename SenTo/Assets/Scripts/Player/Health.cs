using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image heart;

    void Update()
    {
        int health = PlayerVariables.health;

        float newWidth = health * 100;
        heart.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
    }
}
