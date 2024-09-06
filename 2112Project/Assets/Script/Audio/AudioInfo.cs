using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInfo : MonoBehaviour
{
    public AudioSource Source;
    public float OriginalVolume;
    public float LastPercentageVolume;
    public float LastRandomVolume;

    public AudioInfo(AudioSource source, float originalVolume, float lastPercentageVolume, float lastRandomVolume)
    {
        Source = source;
        OriginalVolume = originalVolume;
        LastPercentageVolume = lastPercentageVolume;
        LastRandomVolume = lastRandomVolume;
    }
}
