    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sound list")]
    public AudioSource clickSound;

    public void OnClickSound()
    {
        clickSound.Play(0);
    }
}
