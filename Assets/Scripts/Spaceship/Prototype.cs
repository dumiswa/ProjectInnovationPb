using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prototype : MonoBehaviour
{
    public float speed = 100.0f;
    public float maxSpeed = 500.0f;
    public float mouseSensitivity = 100f;
    public float liftSpeed = 50f;
    public float brakeSpeed = 0.05f;

    public Camera spaceshipCamera;
    public float normalFOV = 60f;
    public float maxFOV = 90f;
    public float fovLerpTime = 2f;

    public List<ParticleSystem> flameParticles;
    //public AudioSource flameAudioSource;

    //public AudioSource shootingAudioSource;
    //public AudioClip shootingAudioClip;

    private Rigidbody spaceshipRigidbody;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private Coroutine fovCoroutine;

    void Start()
    {
        spaceshipRigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        LimitSpeed();

        if (Input.GetMouseButtonDown(0))
        {
            //PlayShootingSound();
        }
    }

    // Handle spaceship movements and actions
    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            spaceshipRigidbody.AddForce(transform.forward * speed);
            //StartFlameEffects();
        }
        else
        {
            //StopFlameEffects();
        }

        if (Input.GetKey(KeyCode.S))
        {
            spaceshipRigidbody.velocity = Vector3.Lerp(spaceshipRigidbody.velocity, Vector3.zero, brakeSpeed);
            ChangeFOV(normalFOV);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            ChangeFOV(maxFOV);
        }

        if (Input.GetKey(KeyCode.F))
        {
            spaceshipRigidbody.AddForce(transform.up * liftSpeed);
        }
    }

    // Handle spaceship rotation
    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // To prevent flipping
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    // Start flame effects
   /* void StartFlameEffects()
    {
        foreach (ParticleSystem flame in flameParticles)
        {
            if (!flame.isPlaying) flame.Play();
        }
        if (!flameAudioSource.isPlaying)
        {
            flameAudioSource.Play();
        }
    }*/

    // Stop flame effects
    /*void StopFlameEffects()
    {
        foreach (ParticleSystem flame in flameParticles)
        {
            if (flame.isPlaying) flame.Stop();
        }
        if (flameAudioSource.isPlaying) flameAudioSource.Stop();
    }

    void PlayShootingSound()
    {
        if (shootingAudioSource != null && shootingAudioClip != null)
        {
            shootingAudioSource.PlayOneShot(shootingAudioClip);
        }
    }*/

    // Change FOV smoothly
    void ChangeFOV(float targetFOV)
    {
        if (fovCoroutine != null) StopCoroutine(fovCoroutine);
        fovCoroutine = StartCoroutine(LerpFOV(spaceshipCamera.fieldOfView, targetFOV));
    }

    IEnumerator LerpFOV(float startFOV, float endFOV)
    {
        float elapsedTime = 0;

        while (elapsedTime < fovLerpTime)
        {
            elapsedTime += Time.deltaTime;
            float newFOV = Mathf.Lerp(startFOV, endFOV, (elapsedTime / fovLerpTime));
            spaceshipCamera.fieldOfView = newFOV;
            yield return null;
        }
    }

    // Limit the speed to the max speed
    void LimitSpeed()
    {
        if (spaceshipRigidbody.velocity.magnitude > maxSpeed)
        {
            spaceshipRigidbody.velocity = spaceshipRigidbody.velocity.normalized * maxSpeed;
        }
    }
}
