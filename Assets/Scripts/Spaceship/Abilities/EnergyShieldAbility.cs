using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX.Utility;

public class EnergyShieldAbility : MonoBehaviour, IAbility
{
    public GameObject energyShieldPrefab;

    public void Use(GameObject User)
    {
        GameObject shield = Instantiate(energyShieldPrefab, User.transform.position, Quaternion.identity);
        shield.transform.SetParent(User.transform);
        shield.transform.localScale = new Vector3((float)1.4, (float)5.6, (float)2.2);
    }
}
