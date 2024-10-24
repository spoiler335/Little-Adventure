using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<EnemyController> enemyTypes = new List<EnemyController>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private bool hasSpawned = false;
    private int enemyCount = 0;

    private void Awake()
    {
        Assert.IsTrue(enemyTypes.Count > 0, "Add Enemy Types");
        Assert.IsTrue(spawnPoints.Count > 0, "Add spawnPoints");

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventsModel.ENEMY_DIED += OnEnemyDead;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.ENEMY_DIED -= OnEnemyDead;
    }

    private void SpawnChataters()
    {
        if (hasSpawned) return;

        System.Random rand = new System.Random((int)Time.time);

        foreach (var point in spawnPoints)
        {
            var spawnedEnemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Count)], point.position, Quaternion.identity);
            ++enemyCount;
        }

        hasSpawned = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) SpawnChataters();
    }

    private void OnEnemyDead(EnemyController enemy)
    {
        --enemyCount;
        if (enemyCount <= 0)
        {
            EventsModel.ALL_ENEMIES_DEAD?.Invoke();
        }
    }

    private void OnDestroy() => UnsubscribeEvents();
}