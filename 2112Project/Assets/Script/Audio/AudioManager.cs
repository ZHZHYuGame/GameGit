using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum MusicType//��������
{
    None,
    BackgroundMusic,
    FightMusic
}
public enum SourceEffectType//��Ч����
{
    None,
    Attack,
    Hit,
    Run,
    Die,
    Skill
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;//����
    private AudioSource musicSource;//����
    private AudioSource[] effectSources;//��Ч����
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
        effectSources = new AudioSource[availableSoundChannels];
        for (int i = 0; i < effectSources.Length; i++)
        {
            effectSources[i] = gameObject.AddComponent<AudioSource>();
        }

        musicSource.playOnAwake = false;
        foreach (var source in effectSources)
        {
            source.playOnAwake = false;
        }
    }
    private void Update()
    {

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
        for (int i = 0; i < effectSources.Length; i++)
        {
            if (!effectSources[i].isPlaying)
            {
                effectSources[i].clip = clip;
                effectSources[i].Play();
                return;
            }
        }
    }
    //������������С
    public void SetAllVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    //������������
    public void SetMusicVolume(AudioInfo audioInfo)
    {
        musicSource.clip = audioInfo.Source.clip;
        musicSource.volume = audioInfo.AudioVolume;
        audioInfo.LastVolume = musicSource.volume;
    }
    public void SetBackGroundVolume(float value)
    {
        musicSource.volume = value; 
    }
    //������Ч����
    public void SetSourceEffectVolume(AudioInfo audioInfo)
    {
        for(int i=0;i<effectSources.Length; i++)
        {
            if (effectSources[i]==audioInfo.Source)
            {
                effectSources[i].volume = audioInfo.AudioVolume;
                audioInfo.LastVolume = effectSources[i].volume;
            }
        }
    }
    public void SetAllEffectVolme(float value)
    {
        for (int i = 0; i < effectSources.Length; i++)
        {
            effectSources[i].volume = value;
        }
    }
}
