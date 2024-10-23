using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<EnemyController> enemyTypes = new List<EnemyController>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private bool hasSpawned = false;

    private void Awake()
    {
        Assert.IsTrue(enemyTypes.Count > 0, "Add Enemy Types");
        Assert.IsTrue(spawnPoints.Count > 0, "Add spawnPoints");
    }

    private void SpawnChataters()
    {
        if (hasSpawned) return;

        foreach (var point in spawnPoints)
        {
            var spawnedEnemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Count - 1)], point.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SpawnChataters();
    }
}