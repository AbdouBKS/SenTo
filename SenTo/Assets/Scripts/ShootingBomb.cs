using System.Collections;
using UnityEngine;

public class ShootingBomb : MonoBehaviour
{
    [SerializeField]
    private GameObject itemToShootPrefab;

    [SerializeField]
    private GameObject bombPosObj;

    [SerializeField]
    private float shootInterval = 2f;

    [SerializeField]
    private float destroyItemDelay = 3f;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(ShootObject(shootInterval));
    }

    private IEnumerator ShootObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 bomb_start_pos = bombPosObj.transform.position;

        var item = (GameObject) Instantiate(itemToShootPrefab, bomb_start_pos, transform.rotation);
        Destroy(item, destroyItemDelay);
        StartCoroutine(ShootObject(shootInterval));
    }

}
