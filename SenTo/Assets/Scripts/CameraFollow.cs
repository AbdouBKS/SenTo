using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camera_pos = transform.position;

        camera_pos.x = Player.transform.position.x;

        if (camera_pos.x < (-4.6f))
            camera_pos.x = -4.6f;

        if (camera_pos.x > 19f)
            camera_pos.x = 19f;

        if (Player.transform.position.y > 1)
        {
            if (camera_pos.y < Player.transform.position.y + 0.78f)
                camera_pos.y += 0.02f;
            if (camera_pos.y > 9.7f)
                camera_pos.y = 9.7f;
        }
        else if (Player.transform.position.y < 3.5f)
        {
            if (camera_pos.y > Player.transform.position.y + 0.78f)
                camera_pos.y -= 0.02f;
            if (camera_pos.y < -2.82f)
                camera_pos.y = -2.82f;
        }



           transform.position = camera_pos;
    }
}
