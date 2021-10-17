using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image heart;
    static int health = 2;

    private void Start()
    {
        float newWidth = health * 100;
        heart.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
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
