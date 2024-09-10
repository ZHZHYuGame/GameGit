using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInfo : MonoBehaviour
{
    public AudioSource Source;
    public float AudioVolume;//音量大小
    public float LastVolume;//上次音量大小
    public MusicType musicType;//音乐类型
    public SourceEffectType sourceEffectType;//音效类型

    public AudioInfo(AudioSource source, float audioVolume, float lastVolume, MusicType musicType, SourceEffectType sourceEffectType)
    {
        Source = source;
        AudioVolume = audioVolume;
        LastVolume = lastVolume;
        this.musicType = musicType;
        this.sourceEffectType = sourceEffectType;
    }
}
