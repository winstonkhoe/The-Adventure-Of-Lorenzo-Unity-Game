using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound currentlyPlaying = null;


    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = Option.volume;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        currentlyPlaying = null;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        currentlyPlaying = s;
        s.source.Play();
    }

    public void SFXPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }

    public void InterceptSong(string name)
    {
        if(currentlyPlaying.source != null)
        {
            currentlyPlaying.source.Stop();
        }
        Play(name);
    }

    public void clearSong()
    {
        if(currentlyPlaying.source != null)
        {
            currentlyPlaying.source.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentlyPlaying != null && !currentlyPlaying.source.isPlaying)
            currentlyPlaying = null;

        if (currentlyPlaying != null)
        {
            currentlyPlaying.source.volume = Option.volume;
        }
    }
}
