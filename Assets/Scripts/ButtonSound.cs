using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip buttonPressSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (buttonPressSound == null)
        {
            Debug.LogError("No sound assigned to the buttonPressSound variable!");
        }
        else
        {
            audioSource.clip = buttonPressSound;
        }
    }

    public void PlaySound()
    {
        if (audioSource != null && buttonPressSound != null)
        {
            audioSource.Play();
        }
    }
}
