using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject bloodSprayPrefab;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player" && coll.gameObject != null)
        {
            GameManager.instance.RestartGame(1f);
            StartCoroutine(SprayBlood(1f, coll.contacts[0].point, coll.gameObject));
        }
    }

    private IEnumerator SprayBlood(float delay, Vector2 position, GameObject player)
    {
        var bloodSpray = (GameObject)Instantiate(bloodSprayPrefab, position, Quaternion.identity);
        Destroy(bloodSpray, 3f);
        Destroy(player);
        yield return new WaitForSeconds(delay);
    }
}
