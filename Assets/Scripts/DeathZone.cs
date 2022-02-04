using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;
    private AudioSource _audioSource;
    public AudioClip gameOverSound;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
       _audioSource.Stop();
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Manager.GameOver();
        _audioSource.PlayOneShot(gameOverSound);
    }
}
