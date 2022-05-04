using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Camera c1;
    public GameObject player;
    public float min = 4f;
    public float max = 16f;
    void Start()
    {
        c1 = GetComponent<Camera>();
    }

    void Update()
    {
        var mousePlus = Input.GetAxis("Mouse ScrollWheel");
        c1.transform.position = new Vector3(player.transform.position.x,player.transform.position.y, -11);
        if(mousePlus == 0)
            return;
        if(mousePlus > 0 && c1.orthographicSize<=max)
            c1.orthographicSize = c1.orthographicSize + mousePlus*4;
        if(mousePlus < 0 && c1.orthographicSize >= min)
            c1.orthographicSize = c1.orthographicSize + mousePlus*4;
    }
}
