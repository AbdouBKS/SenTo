using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject bloodSprayPrefab;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Killing" && this.gameObject != null)
        {
            PlayerVariables.health -= 1;

            StartCoroutine(SprayBlood(1f, coll.contacts[0].point, this.gameObject));

            if (PlayerVariables.health <= 0)
            {
                GameManager.instance.startScene(StrRepo.gameOverScene, 3, 1.5f);
            }
            else
            {
                GameManager.instance.startScene(StrRepo.gameScene, 0, 1.5f);
            }

            
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
