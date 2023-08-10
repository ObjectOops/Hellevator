using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public static readonly string[] GROUP_NAMES = { "Music", "SFX", "Dialog" };
	private static readonly float scale = 20f;

	[SerializeField] private AudioName[] audioNames;
	[SerializeField] private AudioSource musicComponent, sfxComponent, dialogComponent;
	[SerializeField] private AudioMixer mixer;
	// [SerializeField] private float waitDelay;

	private readonly Dictionary<string, AudioClip> audioMap = new();

	private void Awake()
	{
		instance = this;
		for (int i = 0; i < audioNames.Length; ++i)
		{
			audioMap.Add(audioNames[i].name, audioNames[i].clip);
		}
	}

	public void Start() // May be called by the GameManager for initialization purposes.
	{
		AdjustVolume(GROUP_NAMES[0], GetVolume(GROUP_NAMES[0]));
		AdjustVolume(GROUP_NAMES[1], GetVolume(GROUP_NAMES[1]));
		AdjustVolume(GROUP_NAMES[2], GetVolume(GROUP_NAMES[2]));
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

	public IEnumerator PlayDialog(AudioClip voiceover, float volumeScale = 1)
	{
		dialogComponent.PlayOneShot(voiceover, volumeScale);
		// while (dialogComponent.isPlaying)
		// {
		// yield return new WaitForSeconds(waitDelay);
		// }
		float timeout = 0;
		yield return new WaitWhile(() => dialogComponent.isPlaying || (timeout += Time.deltaTime) > 10f);
	}

	public void AdjustVolume(string groupName, float volume)
	{
		// Convert linear values to decibels, which are on a logarithmic scale.
		mixer.SetFloat(groupName, Mathf.Log10(volume) * scale);
		PlayerPrefs.SetFloat(groupName, volume);
	}

	public float GetVolume(string groupName)
	{
		return PlayerPrefs.GetFloat(groupName, 1f);
	}

	public void StopMusic()
	{
		musicComponent.Stop();
	}

	public void StopDialog()
	{
		dialogComponent.Stop();
	}

	[System.Serializable] private struct AudioName
	{
		public string name;
		public AudioClip clip;
	}
}
