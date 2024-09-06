using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;//����
    private AudioSource musicSource;//����
    private AudioSource[] soundSources;//��Ч����
    public int availableSoundChannels = 3;//��Ч���鳤��

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//������������
        }

        InitializeAudioSources();
    }

    private void InitializeAudioSources()
    {
        musicSource = gameObject.AddComponent<AudioSource>();//����������
        soundSources = new AudioSource[availableSoundChannels];
        for (int i = 0; i < soundSources.Length; i++)
        {
            soundSources[i] = gameObject.AddComponent<AudioSource>();
        }

        musicSource.playOnAwake = false;
        foreach (var source in soundSources)
        {
            source.playOnAwake = false;
        }
    }

    //��������
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    //������Ч
    public void PlaySound(AudioClip clip)
    {
        for (int i = 0; i < soundSources.Length; i++)
        {
            if (!soundSources[i].isPlaying)
            {
                soundSources[i].clip = clip;
                soundSources[i].Play();
                return;
            }
        }
    }
}
