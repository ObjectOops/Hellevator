using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	[SerializeField] private List<AudioName> audioNames;
	[SerializeField] private AudioSource musicComponent, sfxComponent, dialogComponent;
	[SerializeField] private AudioMixer mixer;

	[HideInInspector] public Dictionary<string, AudioClip> audioMap = new();

	private void Start()
	{
		instance = this;
		for (int i = 0; i < audioNames.Count; ++i)
		{
			audioMap.Add(audioNames[i].name, audioNames[i].clip);
		}
		AdjustVolume("Music", PlayerPrefs.GetFloat("Music", 1f));
		AdjustVolume("SFX", PlayerPrefs.GetFloat("SFX", 1f));
		AdjustVolume("Dialog", PlayerPrefs.GetFloat("Dialog", 1f));
	}

	public void PlayMusic(string name)
    {
		musicComponent.Stop();
		musicComponent.clip = audioMap[name];
		musicComponent.Play();
    }

	public void PlaySFX(string name, float volumeScale = 1)
	{
		sfxComponent.PlayOneShot(audioMap[name], volumeScale);
	}

	public void PlayDialog(AudioClip voiceover, float volumeScale = 1)
    {
		dialogComponent.PlayOneShot(voiceover, volumeScale);
    }

	public void AdjustVolume(string groupName, float volume)
	{
		// Convert linear values to decibels, which are on a logarithmic scale.
		mixer.SetFloat(groupName, Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat(groupName, volume);
	}


	[System.Serializable] private struct AudioName
    {
		public string name;
		public AudioClip clip;
    }
}