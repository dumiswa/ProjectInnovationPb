using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBombAbility : MonoBehaviour, IAbility
{
    public GameObject blackHolePrefab;

    public void Use(GameObject user)
    {
        Instantiate (blackHolePrefab, user.transform.position + user.transform.forward * 50, Quaternion.identity);
    }
}
