using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHoleBombAbility : MonoBehaviour, IAbility
{
    public float shootingSpeed = 700f;
    private bool active = false;
    private GameObject owner;
    public GameObject ExplodedVisuals;

    /*public void Use(GameObject user)
    {
        Instantiate (blackHolePrefab, user.transform.position + user.transform.forward * 50, Quaternion.identity);
    }*/

    public void Use(GameObject user)
    {
        owner = user;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = user.transform.forward * shootingSpeed;
        }

        StartCoroutine(StopFlyingAfterDuration());
    }

    IEnumerator StopFlyingAfterDuration()
    {
        yield return new WaitForSeconds(1f);

        StopFlying();
    }

    public float absorbSpeed = 2.0f;
    public float growDuration = 5.0f;
    public float implodeDelay = 4.0f;

    public void StopFlying()
    {
        StopAllCoroutines();
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }

        active = true;
        ExplodedVisuals.SetActive(true);
        StartCoroutine(GrowAndImplode());
    }

    IEnumerator GrowAndImplode()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 maxScale = originalScale * 2f;

        for (float t = 0; t < 1; t += Time.deltaTime / growDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, maxScale, t);
            yield return null;
        }

        yield return new WaitForSeconds(implodeDelay);
        
        for (float t = 0; t < 1; t += Time.deltaTime / 0.1f)
        {
            transform.localScale = Vector3.Lerp(maxScale, Vector3.zero, t);
            yield return null;
        }
        
        Destroy(gameObject);
        Debug.Log("Destroyed Black Hole");
    }

    private void OnTriggerStay(Collider other)
    {
        if (!active)
            return;
        if (other.CompareTag("Player") || other.CompareTag("AI"))
        {
            PlayerAbilityController playerController = other.GetComponent<PlayerAbilityController>();

            if (playerController != null && !playerController.IsProtected() &&
                other.GetComponent<Prototype>().playerIndex != owner.GetComponent<Prototype>().playerIndex)
            {
                other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position,
                    absorbSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("AI"))
        {
            PlayerAbilityController playerController = other.GetComponent<PlayerAbilityController>();

            if (playerController != null && !playerController.IsProtected() &&
                other.GetComponent<Prototype>().playerIndex != owner.GetComponent<Prototype>().playerIndex)
            {
                StopFlying();
            }
        }
    }
}