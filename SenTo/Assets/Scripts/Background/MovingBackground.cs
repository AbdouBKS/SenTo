using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject back1;

    [SerializeField]
    private GameObject back2;

    [SerializeField]
    private GameObject back3;

    [SerializeField]
    private GameObject back4;
    

    [SerializeField]
    private GameObject player;

    void Update()
    {
        Vector3 back1_pos = back1.transform.position;
        Vector3 back2_pos = back2.transform.position;
        Vector3 back3_pos = back3.transform.position;
        Vector3 back4_pos = back4.transform.position;

        if (player != null)
        {
            back1_pos.x = player.transform.position.x * 0.1f;
            back1.transform.position = back1_pos;

            back2_pos.x = player.transform.position.x * 0.08f;
            back2.transform.position = back2_pos;

            back3_pos.x = player.transform.position.x * 0.05f;
            back3.transform.position = back3_pos;

            back4_pos.x = player.transform.position.x * 0.03f;
            back4.transform.position = back4_pos;
        }
    }
}
