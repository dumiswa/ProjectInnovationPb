using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private bool canUseAbility;

    public GameObject ownedItemInstance;
    public GameObject ownedItemPrefab;
    

    private void Start()
    {
        canUseAbility = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlackHoles") && ownedItemInstance == null)
        {
            canUseAbility = true;

            ownedItemPrefab = other.gameObject;

            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canUseAbility && ownedItemPrefab)
        {
            ownedItemInstance = Instantiate(ownedItemPrefab, transform.position, Quaternion.identity);
            Debug.Log("Ability used!");
            canUseAbility = false;
        }
    }


}
