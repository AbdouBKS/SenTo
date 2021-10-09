using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTiles : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            GameManager.instance.RestartGame(0f);
        }
    }
}
