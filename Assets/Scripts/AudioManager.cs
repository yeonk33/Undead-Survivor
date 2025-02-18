using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;    // 어디서나 쓸 수 있다.

	[Header("#BGM")]
	public AudioClip BGMClip;
	public float BGMVolume;
	private AudioSource bgmPlayer;
	private AudioHighPassFilter bgmEffect;

	[Header("#SFX (효과음)")]
	public AudioClip[] SFXClip;
	public float SFXVolume;
	public int Channels;
	private AudioSource[] sfxPlayers;
	private int channelIndex;	// 맨 마지막에 실행된 효과음의 index

	// 유니티의 AudioManager의 SFX Clip과 순서 똑같이 맞추기
	public enum SFX { Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win }

	private void Awake()
	{
		Instance = this;
		Init();
	}

	private void Init()
	{
		// BGM Player 초기화
		GameObject bgmObject = new GameObject("BGM Player");
		bgmObject.transform.parent = transform;
		bgmPlayer = bgmObject.AddComponent<AudioSource>();
		bgmPlayer.playOnAwake = false;
		bgmPlayer.loop = true;
		bgmPlayer.volume = BGMVolume;
		bgmPlayer.clip = BGMClip;
		bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

		// 효과음 Palyer 초기화
		GameObject sfxObject = new GameObject("SFX Player");
		sfxObject.transform.parent = transform;
		sfxPlayers = new AudioSource[Channels];

		for (int i = 0; i < sfxPlayers.Length; i++) {
			sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
			sfxPlayers[i].playOnAwake = false;
			sfxPlayers[i].bypassListenerEffects = true;	// 필터 안먹게 
			sfxPlayers[i].volume = SFXVolume;
		}
	}

	public void PlayBGM(bool isPlay)
	{
		if (isPlay) {
			bgmPlayer.Play();
		} else {
			bgmPlayer.Stop();
		}
	}

	public void EffectBGM(bool isPlay)
	{
		bgmEffect.enabled = isPlay;
	}

	public void PlaySFX(SFX sfx)
	{
		for (int i = 0; i < sfxPlayers.Length; i++) {
			int loopIndex = (i + channelIndex) % sfxPlayers.Length; // 넘어가면 다시 0번부터 시작하도록

			if (sfxPlayers[loopIndex].isPlaying) {  // 지금 플레이중이면 냅둬야함
				continue;
			}

			int randIndex = 0;
			if (sfx == SFX.Hit || sfx == SFX.Melee) {
				randIndex = Random.Range(0, 2);
			}

			channelIndex = loopIndex;
			sfxPlayers[loopIndex].clip = SFXClip[(int)sfx];
			sfxPlayers[loopIndex].Play();
			break;
		}
	}
}
