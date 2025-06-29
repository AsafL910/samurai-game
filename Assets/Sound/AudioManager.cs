using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds) 
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
        }
    }

    void Start() 
    {
        Play("Ambient Music - Mossy");
    }

    public void Play(string name) 
    {
        try
        {
            Array.Find(sounds, sound => sound.name.Equals(name)).source.Play();
            //Debug.Log("Playing " + name);
        }
        catch
        {
            Debug.LogWarning("Sound: " + name + "not found.");
            return;
        }
    }

	public void Stop(string name)
	{
        try
        {
            Array.Find(sounds, sound => sound.name.Equals(name)).source.Stop();
            //Debug.Log("Stopping " + name);
        }
        catch 
        {
            Debug.LogWarning("Sound: " + name + "not found.");
            return;
        }
    }

    public void ChangeVolume(string name, float volume) 
    {
        try
        {
             Array.Find(sounds, sound => sound.name.Equals(name)).source.volume = volume;
            //Debug.Log("Stopping " + name);
        }
        catch
        {
            Debug.LogWarning("Sound: " + name + "not found.");
            return;
        }
    }

    public bool isPlaying(string name)
    {
        try
        {
            return Array.Find(sounds, sound => sound.name.Equals(name)).source.isPlaying;
            //Debug.Log("Stopping " + name);
        }
        catch
        {
            Debug.LogWarning("Sound: " + name + "not found.");
            return false;
        }
    }
}

