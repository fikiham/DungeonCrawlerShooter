using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem Instance;

    AudioSource dioS;

    private void Awake()
    {
        Instance = this;
        dioS = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip)
    {
        dioS.PlayOneShot(clip);
    }

    public void PlayOneShotWithDelay(AudioClip clip, float del)
    {
        StartCoroutine(PlayDelay(del, clip));
    }
    IEnumerator PlayDelay(float delay, AudioClip clip)
    {
        yield return new WaitForSeconds(delay);
        dioS.PlayOneShot(clip);
    }
}
