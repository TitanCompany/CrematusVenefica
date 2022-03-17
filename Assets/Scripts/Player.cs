using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
		if (Input.GetAxis("Horizontal") < 0)
		{
            sr.flipX = true;
		}
        else if(Input.GetAxis("Horizontal") > 0)
		{
            sr.flipX = false;
		}
    }

    
}
