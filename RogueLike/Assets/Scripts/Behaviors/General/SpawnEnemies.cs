using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    public int xPos;
    public int yPos;
    public int enemyCount;
    public int enemyLimit;
    public float respawnWaitTimer;
    public float respawnTimerInterval;
    void Start()
    {
        respawnWaitTimer = respawnTimerInterval;
        StartCoroutine(EnemySpawn());
    }
     void Update()
    {
        respawnWaitTimer -= Time.deltaTime;
       if(respawnWaitTimer <= 0)
        {
            enemyCount = 0;
            StartCoroutine(EnemySpawn());
            respawnWaitTimer = respawnTimerInterval;
        }
    }

    IEnumerator EnemySpawn()
    {
        while (enemyCount < enemyLimit)
        {
            xPos = Random.Range(-20, 1);
            yPos = Random.Range(-10, 11);
            Instantiate(enemy, new Vector3(xPos, yPos), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            enemyCount++;
        }
    }

}
