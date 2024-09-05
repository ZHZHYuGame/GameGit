using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;//单例
    private AudioSource musicSource;//音乐
    private AudioSource[] soundSources;//音效数组
    public int availableSoundChannels = 3;//音效数组长度

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//跳场景不销毁
        }

        InitializeAudioSources();
    }

    private void InitializeAudioSources()
    {
        musicSource = gameObject.AddComponent<AudioSource>();//添加音乐组件
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

    //播放音乐
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    //播放音效
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
