using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trader_Hub : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Hub");
        var rbody = GameObject.Find("Player");
        rbody.transform.position = new Vector2(-1.4f,-5.9f);
    }
}
