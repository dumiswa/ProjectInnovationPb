using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBombAbility : MonoBehaviour, IAbility
{
    public GameObject blackHolePrefab;
    public float shootingSpeed = 700f;

    /*public void Use(GameObject user)
    {
        Instantiate (blackHolePrefab, user.transform.position + user.transform.forward * 50, Quaternion.identity);
    }*/

    public void Use(GameObject user)
    {
        GameObject blackHole = Instantiate(blackHolePrefab, user.transform.position + user.transform.forward * 10, Quaternion.identity);
        Rigidbody rb = blackHole.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = user.transform.forward * shootingSpeed; 
        }

        StartCoroutine(StopFlyingAfterDuration(blackHole));
    }

    IEnumerator StopFlyingAfterDuration(GameObject blackHole)
    {
        yield return new WaitForSeconds(5f); 
        if (blackHole != null)
        {
            blackHole.GetComponent<BlackHoleBombBehaviour>().StopFlying();
        }
    }
}
