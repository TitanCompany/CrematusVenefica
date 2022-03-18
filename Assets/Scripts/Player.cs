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

    void FixedUpdate()
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

    private IEnumerator WaitAnim(float time)
	{
        Animator animator = GetComponent<Animator>();
        animator.Ta
        yield return new WaitForSeconds(time);
	}
}
