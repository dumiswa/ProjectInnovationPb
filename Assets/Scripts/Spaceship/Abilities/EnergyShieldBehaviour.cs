using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldBehaviour : MonoBehaviour
{
    public float duration = 5.0f;
    PlayerAbilityController playerController;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerAbilityController>();
        if (playerController != null)
        {
            playerController.SetProtection(true);
        }

        StartCoroutine(DestroyShield());
    }

    private IEnumerator DestroyShield()
    {
        yield return new WaitForSeconds(duration);

        if (playerController != null)
        {
            playerController.SetProtection(false);
        }

        Destroy(gameObject);
    }
}
