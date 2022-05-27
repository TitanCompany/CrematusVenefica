using UnityEngine;

public class Arrows : MonoBehaviour
{
    private PlayerController playerController;
    public int arrowsInBunch;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" || playerController.playerShoot.numArrows > playerController.playerShoot.maxArrows - arrowsInBunch)
            return;
        else
        {
            playerController.playerShoot.numArrows += arrowsInBunch;
            this.gameObject.SetActive(false);
        }
    }
}
