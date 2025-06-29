using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject enemy;
    private float xPos;
    private float yPos;
    private int enemyCounter;
    public int maxEnemyCount;
    public float timeBetweenSpawns;
    public float spawnRadius;
    void Start()
    {
        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        while (enemyCounter < maxEnemyCount)
        {
            xPos = Random.Range(-spawnRadius, spawnRadius);
            yPos = Random.Range(-spawnRadius, spawnRadius);
            Instantiate(enemy, new Vector3(xPos + transform.position.x, yPos + transform.position.y, 0), Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawns);
            enemyCounter++;
        }
    }

	private void OnDrawGizmosSelected()
	{
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, spawnRadius);
	}
}
