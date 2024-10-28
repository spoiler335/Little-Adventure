using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    private float speed = 80f;

    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0, Space.World);
    }
}