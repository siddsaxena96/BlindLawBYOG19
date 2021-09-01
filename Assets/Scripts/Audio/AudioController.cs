using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : ComponentPool<AudioSource>
{
    public static AudioController Instance { get; private set; }
    [SerializeField] private AudioSource backgroundAudioSource = null;
    [SerializeField] private AudioClip aJazzyNightByApoorvBharadwaj = null;
    [SerializeField] private AudioClip blindLawByVisheshSharma = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        PlayBgMusic(aJazzyNightByApoorvBharadwaj);
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioSource source = null;
        if (clip)
        {
            source = Get();
            if (source)
            {
                PlayAudio(source, clip);
            }
        }        
    }

    public void PlayBgMusic(AudioClip clip)
    {
        backgroundAudioSource.clip = clip;
        backgroundAudioSource.Play();
    }

    private void PlayAudio(AudioSource component, AudioClip clip)
    {
        component.clip = clip;
        component.Play();
        StartCoroutine(DelayedDeinitialize(clip.length, () =>
        {
            ReturnToPool(component);
        }
        ));
    }

    private IEnumerator DelayedDeinitialize(float time, System.Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
}
