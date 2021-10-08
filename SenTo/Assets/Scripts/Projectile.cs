using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private bool collision_bool = false;
  
    // Use this for initialization
    void Start () {
        
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        collision_bool = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (collision_bool == false)
            transform.position -= transform.right * Time.deltaTime * speed;
    }
}
