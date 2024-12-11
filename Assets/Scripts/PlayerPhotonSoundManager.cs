using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhotonSoundManager : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip footstepSFX;

    public void PlayFootstepSFX()
    {
        footstepSource.clip = footstepSFX;
        //Pitch and volume
        footstepSource.Play();

    }
}
