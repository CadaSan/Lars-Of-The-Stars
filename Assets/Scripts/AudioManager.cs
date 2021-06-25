using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _powerUpSound, _explosionSound;

    
    public void PowerUpSound()
    {
        _powerUpSound.Play();
    }

    public void ExplosionSound()
    {
        _explosionSound.Play();
    }
}
