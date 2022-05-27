using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    PlayerController player;
    public Canvas playerUI;
    public Text hp;
    public Text roots;
    public Text arrows;
    public Text lvl;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        hp = GameObject.FindGameObjectWithTag("HPBar").GetComponent<Text>();
        roots = GameObject.FindGameObjectWithTag("Roots").GetComponent<Text>();
        arrows = GameObject.FindGameObjectWithTag("ArrowsUI").GetComponent<Text>();
        lvl = GameObject.FindGameObjectWithTag("LvlUI").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        hp.text = "HP " + player.entity.currentHP + "/" + player.entity.maxHP;
        roots.text = "Roots " + player.numRoots + "/" + player.maxRoots;
        arrows.text = "Arrows " + player.playerShoot.numArrows + "/" + player.playerShoot.maxArrows;
        lvl.text = "LVL " + player.playerLevel.ToString();
    }
}
