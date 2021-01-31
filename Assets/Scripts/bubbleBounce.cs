using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleBounce : MonoBehaviour
{
    [SerializeField] AudioSource[] bubbleBounceAudioSource;
    private AudioSource soundFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Ground" || collision.tag == "Bubble")
        {
            // play  a random sound from audio sources
            StartCoroutine(PlayAudioFX());
        }
    }
    IEnumerator PlayAudioFX()
    {
        soundFX = bubbleBounceAudioSource[Random.Range(0, bubbleBounceAudioSource.Length)];
        soundFX.Play();
        yield return new WaitForSeconds(soundFX.clip.length);
    }
}
