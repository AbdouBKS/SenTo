using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Health health;
    public GameObject bloodSprayPrefab;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Killing" && this.gameObject != null)
        {
            PlayerVariables.health -= 1;
            Debug.Log(PlayerVariables.health);
            GameManager.instance.RestartGame(1f);
            StartCoroutine(SprayBlood(1f, coll.contacts[0].point, this.gameObject));

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
