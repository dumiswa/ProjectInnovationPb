using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private bool hasAnItem;
    private bool canPickUp;

    public GameObject ownedItemInstance;
    public GameObject ownedItemPrefab;
    private void Start()
    {
        hasAnItem = false;
        canPickUp = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlackHoles") && ownedItemInstance == null)
        {
            canPickUp = false;

            ownedItemPrefab = other.gameObject;

            Destroy(other.gameObject);
        }
    }

    private void update()
    {
        if (Input.GetMouseButtonDown(0) && canPickUp! && ownedItemPrefab != null) 
        {
            ownedItemInstance = Instantiate(ownedItemPrefab, transform.position, Quaternion.identity);
        }
    }


}
