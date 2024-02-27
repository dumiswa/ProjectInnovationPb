using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform ShootPoint;
    public float StationaryBulletSpeed = 20f; // Speed of the bullet when the ship is not moving
    private Rigidbody shipRigidbody;

    void Start()
    {
        // Ensure the ship has a Rigidbody component
        shipRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameState.canShoot == true)
        {
            Shoot();
            Debug.Log("Shooting");
            GameState.canShoot = false;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, ShootPoint.position, ShootPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        Vector3 initialVelocity;
        if (shipRigidbody.velocity.magnitude > 0)
        {
            // If the ship is moving, double the ship's velocity and use it as the bullet's initial velocity
            initialVelocity = shipRigidbody.velocity * 2;
        }
        else
        {
            // If the ship is not moving, set a default velocity forward relative to the ship's orientation
            initialVelocity = ShootPoint.forward * StationaryBulletSpeed;
        }

        if (bulletRb != null)
        {
            bulletRb.velocity = initialVelocity;
        }
        else
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody component attached.");
        }
    }

}
