using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SingleRegion : MonoBehaviour
{
    [SerializeField] private List<Gate> nextGates = new List<Gate>();
    [SerializeField] private List<EnemyController> enemyTypes = new List<EnemyController>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private int Id;

    private int enemyCount = 0;
    private bool hasSpawned = false;

    private void Awake()
    {
        SubscribeEvents();

        Assert.IsTrue(spawnPoints.Count > 0, "Add spawnPoints");
        Assert.IsTrue(enemyTypes.Count > 0, "Add spawnPoints");
    }

    private void SubscribeEvents()
    {
        EventsModel.ENEMY_DIED += OnEnemyDead;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.ENEMY_DIED -= OnEnemyDead;
    }

    private void OnEnemyDead(EnemyController enemy)
    {
        if (enemy.regionId != Id) return;

        --enemyCount;
        if (enemyCount <= 0)
        {
            EventsModel.REGION_CLEARED?.Invoke();
            if (nextGates.Count > 0)
                nextGates.ForEach((gate) => gate.OpenGate());
        }
    }

    private void SpawnChataters()
    {
        if (hasSpawned) return;

        foreach (var point in spawnPoints)
        {
            var spawnedEnemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Count)], point.position, Quaternion.identity);
            spawnedEnemy.regionId = Id;
        }


        hasSpawned = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) SpawnChataters();
    }

    private void OnDestroy() => UnsubscribeEvents();
}