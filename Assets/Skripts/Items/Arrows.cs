using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    private PlayerController playerController;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerController.playerShoot.numArrows == playerController.playerShoot.maxArrows || collision.tag != "Player")
            return;
        else
        {
            playerController.playerShoot.numArrows += 5;
            if (playerController.playerShoot.numArrows > playerController.playerShoot.maxArrows)
                playerController.playerShoot.numArrows = playerController.playerShoot.maxArrows;
            this.gameObject.SetActive(false);
        }
    }
}
