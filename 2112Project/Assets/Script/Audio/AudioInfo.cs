using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInfo : MonoBehaviour
{
    public AudioSource Source;
    public float AudioVolume;//������С
    public float LastVolume;//�ϴ�������С
    public MusicType musicType;//��������
    public SourceEffectType sourceEffectType;//��Ч����

    public AudioInfo(AudioSource source, float audioVolume, float lastVolume, MusicType musicType, SourceEffectType sourceEffectType)
    {
        Source = source;
        AudioVolume = audioVolume;
        LastVolume = lastVolume;
        this.musicType = musicType;
        this.sourceEffectType = sourceEffectType;
    }
}
