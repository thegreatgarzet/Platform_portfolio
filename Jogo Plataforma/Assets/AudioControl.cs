using System;
using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    public static AudioControl instance;
	public Sound[] sounds;
	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
		}
	}
	public void PlaySound(string soundname)
    {
		Sound s = Array.Find<Sound>(sounds, item => item.name == soundname);
		s.source.Play();
		//sounds[soundIndex].source.Play();
		
	}
	public void SoundStop(string soundname)
    {
		Sound s = Array.Find<Sound>(sounds, item => item.name == soundname);
		s.source.Stop();
	}
}
