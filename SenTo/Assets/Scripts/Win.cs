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

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player" && coll.gameObject != null)
        {
            Debug.Log("lol");
            GameManager.instance.WinGame(1f);
        }
    }
}
