using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBombBehaviour : MonoBehaviour
{
    public float absorbSpeed = 2.0f;
    public float growDuration = 5.0f;
    public float implodeDelay = 2.0f;

    private void Start()
    {
        StartCoroutine(GrowAndImplode());
    }

    IEnumerator GrowAndImplode()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 maxScale = originalScale * 2f;

        for ( float  t = 0; t < 1; t += Time.deltaTime / growDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, maxScale, t);
            yield return null; 
        }

        yield return new WaitForSeconds(implodeDelay);
        
        Destroy(gameObject);
        Debug.Log("Destroyed Black Hole");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           PlayerAbilityController playerController = other.GetComponent<PlayerAbilityController>();

            if (playerController != null && !playerController.IsProtected())
            {
                other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, absorbSpeed * Time.deltaTime);
            }
        }
    }
}
