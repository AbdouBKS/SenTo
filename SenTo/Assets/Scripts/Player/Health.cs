using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image heart;
    public static int health;

    private void Awake()
    {
        health = 3;
    }

    public int newHealth()
    {
        if (health > 0)
            health -= 1;
        
        float newWidth = health * 100;

        heart.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);

        return health;
    }

}
