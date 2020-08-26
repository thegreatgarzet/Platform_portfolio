using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicControl : MonoBehaviour
{
    public Sound[] soundlist;
    public List<AudioSource> sources;
    public bool switchmusic;
    public int actualmusic, nextmusic;
    private void Awake()
    {
        foreach (Sound sound in soundlist)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
            sound.source.outputAudioMixerGroup = sound.mixerGroup;
            sources.Add(sound.source);
        }
        foreach (AudioSource audiosources in sources)
        {
            audiosources.volume = 0;
            audiosources.Play();
        }
        sources[actualmusic].volume= 1;
        sources[actualmusic].Play();
    }
    private void Update()
    {
        if (switchmusic)
        {
            sources[actualmusic].volume -= 0.7f * Time.deltaTime;
            if(sources[actualmusic].volume <= 0.1f)
            {
                sources[nextmusic].volume += 0.7f * Time.deltaTime;
                if(sources[nextmusic].volume >= 0.95f)
                {
                    actualmusic = nextmusic;
                    switchmusic = false;
                }
            }
        }
    }
}
