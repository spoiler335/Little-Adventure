using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int regionsCleard = 0;

    private void Awake() => SubscribeEvents();

    private void SubscribeEvents()
    {
        EventsModel.REGION_CLEARED += OnRegionCleard;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.REGION_CLEARED -= OnRegionCleard;
    }

    private void OnRegionCleard()
    {
        ++regionsCleard;
        if (regionsCleard >= Constants.MAX_REGIONS)
        {
            EventsModel.ALL_REGIONS_CLEARED?.Invoke();
            Debug.Log($"All Regions are Regions Cleard");
        }
    }

    private void OnDestroy() => UnsubscribeEvents();
}
