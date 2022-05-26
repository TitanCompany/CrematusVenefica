using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
	[SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    Transform enemyParant;
    [SerializeField]
    int minEnemiesCount;
    [SerializeField]
    int needEnemiesCount;

    void Start()
    {
    }

    public void RefreshEnemiesCount(string EntityTag)
 	{
        int count = 0;
        foreach (var en in GameObject.FindGameObjectsWithTag(EntityTag))
            if (en.gameObject.tag == enemyPrefab.tag)
                count += 1;

		if (count <= minEnemiesCount)
			for (int i = count; i < needEnemiesCount; i++)
			{
                GameObject enemy = enemyPrefab;
                enemy.GetComponent<Entity>().respawner = gameObject;
                Instantiate(enemy, SetPosition(), Quaternion.identity, enemyParant);

            }
    }

    private Vector3 SetPosition()
	{
        var rnd = new System.Random();
        return new Vector3(transform.position.x + rnd.Next(-10, 10), transform.position.y + rnd.Next(-10, 10), 0);
    }

    public List<GameObject> GetEnemies()
	{
        List<GameObject> enemies = new List<GameObject>();
		foreach (var en in GameObject.FindGameObjectsWithTag("Enemy"))
			if (en.gameObject.name == enemyPrefab.name)
				enemies.Add(en);

        return enemies;
	}
}
