using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A very simple Audio class, I've made variants of these for lots of small projects
/// </summary>
public class AudioManager : MonoBehaviour
{
	#region classVariables
	[SerializeField]
	private AudioClip m_SoundButtonSelected = null;
	[SerializeField]
	private AudioClip m_SoundButtonPressed = null;
	private AudioSource MusicSource;
	private AudioSource MusicSource2;
	private AudioSource SFXSource;
	private bool Initialized = false;

	public static AudioManager Instance = null;

	private enum MusicSourceIsPlaying
	{
		None = -1,
		First,
		Second,
	}
	private MusicSourceIsPlaying SourcePlaying = MusicSourceIsPlaying.None;

	private float MusicVolumeMAX = 1.0f;
	private float SFXVolumeMAX = 1.0f;
	private const string s_MUSIC_VOLUME_PREF_KEY = "MUSIC_VOLUME_PREF_KEY";
	private const string s_SFX_VOLUME_PREF_KEY = "SFX_VOLUME_PREF_KEY";
	#endregion
	private void Awake()
	{
		DoInitialization();
	}

	private void DoInitialization()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null)
		{
			Instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		MusicSource = this.gameObject.AddComponent<AudioSource>();
		MusicSource2 = this.gameObject.AddComponent<AudioSource>();
		SFXSource = this.gameObject.AddComponent<AudioSource>();
		MusicSource.loop = true;
		MusicSource2.loop = true;

		MusicVolumeMAX = GetMusicVolume();

		SFXVolumeMAX = GetSFXVolume();
		Initialized = true;
	}
	#region LoopingMusic
	public void PlayLoopingMusic(AudioClip music)
	{
		if (!Initialized)
		{
			DoInitialization();
		}
		AudioSource sourceToAddTo = GetPlayingLoopingAudioSource();
		if (sourceToAddTo.clip != music)
		{
			SourcePlaying = sourceToAddTo == MusicSource ? MusicSourceIsPlaying.First : MusicSourceIsPlaying.Second;
			sourceToAddTo.clip = music;
			sourceToAddTo.volume = Mathf.Clamp(1, 0.0f, MusicVolumeMAX);
			sourceToAddTo.Play();
		}
	}

	public void PlayLoopingMusicWithFadeOutThenIn(AudioClip musicToFadeTo, float transitionTime = 1.0f)
	{
		if (!Initialized)
		{
			DoInitialization();
		}
		AudioSource sourceToMakeFade = GetPlayingLoopingAudioSource();
		SourcePlaying = sourceToMakeFade == MusicSource ? MusicSourceIsPlaying.First : MusicSourceIsPlaying.Second;
		StartCoroutine(UpdateFadingOutInMusic(sourceToMakeFade, musicToFadeTo, transitionTime));
	}

	private IEnumerator UpdateFadingOutInMusic(AudioSource active, AudioClip newMusic, float transitionTime)
	{
		if (!active.isPlaying)
		{
			active.Play();
		}

		for (float fadeTimer = 0; fadeTimer < transitionTime; fadeTimer += Time.deltaTime)
		{
			active.volume = Mathf.Clamp((1 - (fadeTimer / transitionTime)), 0.0f, MusicVolumeMAX);
			yield return null;
		}
		active.Stop();
		active.clip = newMusic;
		active.Play();

		for (float fadeTimer = 0; fadeTimer < transitionTime; fadeTimer += Time.deltaTime)
		{
			active.volume = Mathf.Clamp((fadeTimer / transitionTime), 0.0f, MusicVolumeMAX);
			yield return null;
		}

		yield return null;
	}

	private void PlayLoopingMusicWithCrossFade(AudioClip musicToFadeTo, float transitionTime = 1.0f)
	{
		if (!Initialized)
		{
			DoInitialization();
		}
		AudioSource currentPlaying = GetPlayingLoopingAudioSource();
		AudioSource oppositePlaying = GetNonPlayingLoopingAudioSource();
		SourcePlaying = oppositePlaying == MusicSource2 ? MusicSourceIsPlaying.Second : MusicSourceIsPlaying.First;

		oppositePlaying.clip = musicToFadeTo;
		oppositePlaying.volume = 0.0f;
		oppositePlaying.Play();
		StartCoroutine(UpdateCrossFadingMusic(currentPlaying, oppositePlaying, transitionTime));

	}

	private IEnumerator UpdateCrossFadingMusic(AudioSource active, AudioSource newSource, float transitionTime)
	{
		for (float fadeTimer = 0; fadeTimer < transitionTime; fadeTimer += Time.deltaTime)
		{
			active.volume = Mathf.Clamp((1 - (fadeTimer / transitionTime)), 0.0f, MusicVolumeMAX);
			newSource.volume = Mathf.Clamp((fadeTimer / transitionTime), 0.0f, MusicVolumeMAX);
			yield return null;
		}
		active.Stop();
	}

	private AudioSource GetPlayingLoopingAudioSource()
	{
		AudioSource currentSource = MusicSource;
		switch (SourcePlaying)
		{
			case MusicSourceIsPlaying.Second:
				currentSource = MusicSource2;
				break;
			case MusicSourceIsPlaying.First:
			case MusicSourceIsPlaying.None:
				break;
		}
		return currentSource;
	}

	private AudioSource GetNonPlayingLoopingAudioSource()
	{
		AudioSource oppositeSource = MusicSource;
		switch (SourcePlaying)
		{
			case MusicSourceIsPlaying.First:
				oppositeSource = MusicSource2;
				break;
			case MusicSourceIsPlaying.None:
				break;
		}
		return oppositeSource;
	}

	public void StopAnyCurrentMusic()
	{
		if (!Initialized)
		{
			DoInitialization();
		}
		MusicSource.Stop();
		MusicSource2.Stop();
	}
	public void StopAndClearAnyCurrentMusic()
	{
		if (!Initialized)
		{
			DoInitialization();
		}
		MusicSource.Stop();
		MusicSource.clip = null;
		MusicSource2.Stop();
		MusicSource2.clip = null;
	}
	#endregion
	#region SFX
	public void PlaySFX(AudioClip sfx)
	{
		if (!Initialized)
		{
			DoInitialization();
		}
		SFXSource.PlayOneShot(sfx, SFXVolumeMAX);
	}

	public void PlaySFX(AudioClip sfx, float volume)
	{
		volume = Mathf.Clamp(volume, 0.0f, SFXVolumeMAX);
		SFXSource.PlayOneShot(sfx, volume);
	}

	public void PlayButtonSelectedSFX()
	{
		PlaySFX(m_SoundButtonSelected);
	}

	public void PlayButtonPressedSFX()
	{
		PlaySFX(m_SoundButtonPressed);
	}
	#endregion
	#region SetVolumes
	public void SetMusicVolume(float volume)
	{
		MusicVolumeMAX = volume;
		MusicSource.volume = MusicVolumeMAX;
		MusicSource.volume = MusicVolumeMAX;
		PlayerPrefs.SetFloat(s_MUSIC_VOLUME_PREF_KEY, volume);
	}

	public void SetSFXVolume(float volume)
	{
		SFXVolumeMAX = volume;
		SFXSource.volume = SFXVolumeMAX;
		PlayerPrefs.SetFloat(s_SFX_VOLUME_PREF_KEY, volume);
	}
	#endregion
	#region GetVolumeValues
	public float GetMusicVolume()
	{
		return PlayerPrefs.GetFloat(s_MUSIC_VOLUME_PREF_KEY, 1.0f);
	}

	public float GetSFXVolume()
	{
		return PlayerPrefs.GetFloat(s_SFX_VOLUME_PREF_KEY, 1.0f);
	}
	#endregion
}
