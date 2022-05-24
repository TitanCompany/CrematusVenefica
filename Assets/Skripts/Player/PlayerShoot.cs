using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Camera cam;
    public Rigidbody2D rbody;

    public Transform firePoint;
    public GameObject arrowPrefab;
    private float angle;
    private Transform player;

    public float arrowForce = 20f;
    private float shootRate = 2f;
    private float nextShootTime = 0f;

    Vector2 mousePos;

    private void Start()
    {
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rbody.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Debug.Log(angle);
        if (angle > 0)
            angle = 0;
        if (angle < -180)
            angle = -180;
        if (Time.time >= nextShootTime)
            if (Input.GetButtonDown("Fire1"))
            {
                nextShootTime = Time.time + 1f / shootRate;
                Shoot();
            }
    }

    private void Shoot()
    {
        firePoint.Rotate(0f, 0f, angle);
        GameObject bullet = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
        rbody.AddForce(firePoint.up * arrowForce, ForceMode2D.Impulse);
        firePoint.Rotate(0f, 0f, -angle);
    }
}
