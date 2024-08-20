using UnityEngine;

public class FadeInAudio : MonoBehaviour
{
    public float fadeInDuration = 1;
    AudioSource audioSource;
    float originalVolume;
    float startTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalVolume = audioSource.volume;
        startTime = Time.time;
    }

    void Update()
    {
        audioSource.volume = Mathf.Lerp(0, originalVolume, (Time.time - startTime) / fadeInDuration);
    }
}
