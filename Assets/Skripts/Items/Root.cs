using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerController.numRoots == playerController.maxRoots)
            return;
        else
        {
            playerController.numRoots += 1;
            this.gameObject.SetActive(false);
        }
    }
}
