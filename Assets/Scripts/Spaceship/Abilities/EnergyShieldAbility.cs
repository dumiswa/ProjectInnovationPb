using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX.Utility;

public class EnergyShieldAbility : MonoBehaviour, IAbility
{
    public void Use(GameObject User)
    {
        transform.SetParent(User.transform);
        transform.localScale = new Vector3((float)9, (float)9, (float)9);
        if (playerController != null)
        {
            playerController.SetProtection(true);
        }

        StartCoroutine(DestroyShield());
    }
    
    public float duration = 5.0f;
    PlayerAbilityController playerController;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerAbilityController>();
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
