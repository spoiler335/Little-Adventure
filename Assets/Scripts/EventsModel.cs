using UnityEngine;
using UnityEngine.Events;

public static class EventsModel
{
    public static UnityAction<Vector3> PLAY_SLASH_VFX;
    public static UnityAction<Vector3, GameObject> PLAY_ENEMY_BEGIN_HIT_VFX;
    public static UnityAction<Vector3> ADD_IMPACT_ON_PLAYER;
    public static UnityAction<Vector3, GameObject> ADD_IMPACT_ON_ENEMY;
    public static UnityAction<EnemyController> ENEMY_DIED;
    public static UnityAction REGION_CLEARED;
    public static UnityAction ALL_REGIONS_CLEARED;
    public static UnityAction COINS_ECONOMY_CHANGED;
    public static UnityAction PLAYER_DEAD;

    // UI Events
    public static UnityAction<int> UPDATE_PLAYER_HEALTH;
    public static UnityAction ON_PAUSE_CLICKED;
    public static UnityAction ON_RESUME_CLICKED;

}