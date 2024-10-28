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
    }

    private IEnumerator OpenGateCor()
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

    public void OpenGate() => StartCoroutine(OpenGateCor());
}