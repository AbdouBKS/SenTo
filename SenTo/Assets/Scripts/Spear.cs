using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        spear_up();
    }

    private IEnumerator spear_up()
    {
        yield return new WaitForSeconds(3f);
        anim.SetBool("Spear", true);
        spear_up();
    }
}
