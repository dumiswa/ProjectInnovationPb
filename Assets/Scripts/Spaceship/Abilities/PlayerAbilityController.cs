using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    private GameObject currentAbility = null;
    private Prototype playerController;

    private void Start()
    {
        playerController = GetComponent<Prototype>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AbilityPickup") && currentAbility == null)
        {
            currentAbility = other.GetComponent<PickUp>().AbilityInstance;
            other.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (currentAbility != null && playerController.useController
                ? InputManager.instance.GetInput(playerController.playerIndex).UsePowerUp
                : Input.GetMouseButtonDown(0))
        {
            var ability = Instantiate(currentAbility, transform.position, transform.rotation, null);
            ability.GetComponent<IAbility>().Use(gameObject);
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