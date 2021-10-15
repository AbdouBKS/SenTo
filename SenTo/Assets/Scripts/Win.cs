using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    private string spriteName;

    void Start()
    {
        spriteName = GetComponent<SpriteRenderer>().sprite.name;
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        spriteName = GetComponent<SpriteRenderer>().sprite.name;
        if (hit.CompareTag("Player") && string.Equals(spriteName, "Door_1"))
        {
            GameManager.instance.WinGame(0f);
        }
    }
}
