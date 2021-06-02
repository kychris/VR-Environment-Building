using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    AudioSource source1;
    AudioSource source2;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        source1 = audioSources[0];
        source2 = audioSources[1];

    }
    public void PlayCheeringSounds()
    {
        source1.Play();
        source2.Play();
    }
}
