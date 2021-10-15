using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Health health;
    public GameObject bloodSprayPrefab;

    void Start()
    {
        health = GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        int currentHealth;
        if (coll.transform.tag == "Player" && coll.gameObject != null)
        {
            currentHealth = health.newHealth();
            GameManager.instance.RestartGame(1f);
            StartCoroutine(SprayBlood(1f, coll.contacts[0].point, coll.gameObject));

            if (currentHealth <= 0)
                GameManager.instance.WinGame(0f);
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
