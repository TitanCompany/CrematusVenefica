using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsScript : MonoBehaviour
{
    private bool fight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Animator a = GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.E))
            a.Play("Player_hit");
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            a.Play("Player_run");
            if (Input.GetAxis("Horizontal") < 0)
                sr.flipX = true;
            if (Input.GetAxis("Horizontal") > 0)
                sr.flipX = false;
        }
        else
            a.Play("Player_static");
    }
}
