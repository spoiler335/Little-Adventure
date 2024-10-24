using UnityEngine;
using UnityEngine.Events;

public static class EventsModel
{
    public static UnityAction<Vector3> PLAY_SLASH_VFX;
    public static UnityAction<Vector3, GameObject> PLAY_ENEMY_BEGIN_HIT_VFX;
    public static UnityAction<Vector3> ADD_IMPACT_ON_PLAYER;

    public static UnityAction COINS_ECONOMY_CHANGED;
}