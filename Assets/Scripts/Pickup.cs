using UnityEngine;

public static class GameState
{
    public static bool canShoot { get; set; }
}
public class Pickup : MonoBehaviour
{
    private void Start()
    {
        GameState.canShoot = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameState.canShoot = true;
            Destroy(this.gameObject);
            Debug.Log("item destroyed");
        }
    }
}
