using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MovementSpeed;
    private Rigidbody2D r1;
    void Start()
    {
        r1 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        var l1 = Input.GetAxis("Horizontal");
        var l2 = Input.GetAxis("Vertical");
        var move = new Vector2(MovementSpeed * l1, MovementSpeed * l2);
        r1.velocity = move;
    }
}
