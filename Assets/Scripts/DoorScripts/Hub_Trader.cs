using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hub_Trader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
            SceneManager.LoadScene("Trader");
    }
}
