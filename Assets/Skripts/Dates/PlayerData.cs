using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float maxHP;
    public float currentHP;
    public int maxRoots;
    public int numRoots;
    public int level;
    public float damage;
    public float[] position;

    public PlayerData(PlayerController playerController)
    {
        maxHP = playerController.entity.maxHP;
        currentHP = playerController.entity.currentHP;
        maxRoots = playerController.maxRoots;
        numRoots = playerController.numRoots;
        level = 0;
        damage = playerController.playerAttack.damage;
        position = new float[3];
        position[0] = playerController.transform.position.x;
        position[1] = playerController.transform.position.y;
        position[2] = playerController.transform.position.z;
    }
}
