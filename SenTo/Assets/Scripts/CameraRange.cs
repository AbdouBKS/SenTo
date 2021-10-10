using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRange : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camera_pos = transform.position;

        if (camera_pos.x < (-4.6f))
            camera_pos.x = -4.6f;
        if (camera_pos.x > 19f)
            camera_pos.x = 19f;

         transform.position = camera_pos;
    }
}
