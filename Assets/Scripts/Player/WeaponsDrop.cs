using System.Collections.Generic;
using UnityEngine;

public class WeaponsDrop : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;

    public void DropWeapons()
    {
        foreach (var weapon in weapons)
        {
            weapon.AddComponent<Rigidbody>();
            weapon.AddComponent<BoxCollider>();
            weapon.transform.parent = null;
        }
    }
}