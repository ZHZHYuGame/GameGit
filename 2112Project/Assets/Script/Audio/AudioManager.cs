using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum MusicType//音乐类型
{
    None,
    BackgroundMusic,
    FightMusic
}
public enum SourceEffectType//音效类型
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
    public static AudioManager instance;//单例
    private AudioSource musicSource;//音乐
    private AudioSource[] effectSources;//音效数组
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

    //播放音乐
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    //播放音效
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
    //设置总音量大小
    public void SetAllVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    //设置音乐音量
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
    //设置音效音量
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
