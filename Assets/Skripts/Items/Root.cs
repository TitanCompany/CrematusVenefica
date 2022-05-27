using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    private PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerController.numRoots == playerController.maxRoots || collision.tag!="Player")
            return;
        else
        {
            playerController.numRoots += 1;
            this.gameObject.SetActive(false);
        }
    }
}
