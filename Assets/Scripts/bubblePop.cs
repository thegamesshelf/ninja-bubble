using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubblePop : MonoBehaviour
{
    [SerializeField] GameObject bubblePopPS;
    [SerializeField] GameObject bubble;
    [SerializeField] AudioSource[] bubblePopAudioSources;
    private AudioSource soundFX;
    private bool popped = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ThrowingStar" && popped == false)
        {
            popped = true;
            //print(collision.tag);
            //hide bubble image
            bubble.SetActive(false);
            // play  a random sound from audio sources
            StartCoroutine(PlayAudioFX());
            // udpate current bubble count
            bubbleManager.currentBubbleCount-=1;
            // ignore the hit force from the star
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), bubble.GetComponent<Collider2D>());
            // get the particle system duration
            ParticleSystem parts = bubblePopPS.GetComponent<ParticleSystem>();
            //play particle system
            bubblePopPS.SetActive(true);
            //destroy after the particle system is done
            Destroy(gameObject, parts.main.duration+(parts.main.duration /3));
        }
    }
    IEnumerator PlayAudioFX()
    {
        soundFX = bubblePopAudioSources[Random.Range(0, bubblePopAudioSources.Length)];
        soundFX.Play();
        yield return new WaitForSeconds(soundFX.clip.length);
    }

}
