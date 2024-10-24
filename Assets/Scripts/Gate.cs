using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject visual;

    private Collider gateCollider;
    private float openDuration = 2f;
    private float openTargetY = -2f;

    private void Awake()
    {
        gateCollider = GetComponent<Collider>();

        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        EventsModel.ALL_ENEMIES_DEAD += OnAllEnemiesDead;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.ALL_ENEMIES_DEAD -= OnAllEnemiesDead;
    }
    private IEnumerator OpenGate()
    {
        float currrentDuration = 0;

        Vector3 startPos = visual.transform.position;
        Vector3 targetPos = startPos + Vector3.up * openTargetY;

        while (currrentDuration < openDuration)
        {
            currrentDuration += Time.deltaTime;
            visual.transform.position = Vector3.Lerp(startPos, targetPos, currrentDuration / openDuration);

            yield return null;
        }

        gateCollider.enabled = false;
    }

    private void OnAllEnemiesDead()
    {
        StartCoroutine(OpenGate());
    }

    private void OnDestroy() => UnsubscribeEvents();
}