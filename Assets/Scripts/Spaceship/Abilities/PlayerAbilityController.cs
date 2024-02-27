using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    private IAbility currentAbility;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AbilityPickup") && currentAbility == null)
        {
            IAbility abilityComponent = other.GetComponent<IAbility>();
            if (abilityComponent != null)
            {
                currentAbility = abilityComponent;
                other.gameObject.SetActive(false);
                Debug.Log("Ability picked up: " + abilityComponent.GetType().ToString());
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentAbility != null)
        {
            currentAbility.Use(gameObject);
            currentAbility = null;
        }
    }

    private bool isProtected = false;

    public void SetProtection(bool state)
    {
        isProtected = state;
        Debug.Log($"Player protection set to: {isProtected}");
    }

    public bool IsProtected()
    {
        return isProtected;
    }
}

