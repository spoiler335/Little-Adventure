using System.Collections.Generic;
using UnityEngine;

public class SingleRegion : MonoBehaviour
{
    [SerializeField] private List<Gate> nextGates = new List<Gate>();

    private int enemyCount = 0;

    private void Awake()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventsModel.ENEMIES_SPAWNED += OnEnemiesSpanwed;
        EventsModel.ENEMY_DIED += OnEnemyDead;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.ENEMIES_SPAWNED -= OnEnemiesSpanwed;
        EventsModel.ENEMY_DIED -= OnEnemyDead;
    }

    private void OnEnemyDead(EnemyController enemy)
    {
        --enemyCount;
        if (enemyCount <= 0)
        {
            EventsModel.REGION_CLEARED?.Invoke();
            if (nextGates.Count > 0)
                nextGates.ForEach((gate) => gate.OpenGate());
        }
    }

    private void OnEnemiesSpanwed(int Count) => enemyCount = Count;

    private void OnDestroy() => UnsubscribeEvents();
}