using UnityEngine;

public class SpeedPB : MonoBehaviour
{
    public float boostAmount = 200f; // Amount to boost
    public float boostDuration = 2f; // Duration of the boost

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your spaceship has the tag "Spaceship"
        {
            Prototype spaceship = other.GetComponent<Prototype>();
            if (spaceship != null)
            {
                spaceship.ActivateSpeedBoost(boostAmount, boostDuration);
            }
            Destroy(gameObject); // Destroy the pickup item
        }
    }
}
