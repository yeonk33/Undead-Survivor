using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;    // ��𼭳� �� �� �ִ�.

	[Header("#BGM")]
	public AudioClip BGMClip;
	public float BGMVolume;
	private AudioSource bgmPlayer;
	private AudioHighPassFilter bgmEffect;

	[Header("#SFX (ȿ����)")]
	public AudioClip[] SFXClip;
	public float SFXVolume;
	public int Channels;
	private AudioSource[] sfxPlayers;
	private int channelIndex;	// �� �������� ����� ȿ������ index

	// ����Ƽ�� AudioManager�� SFX Clip�� ���� �Ȱ��� ���߱�
	public enum SFX { Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win }

	private void Awake()
	{
		Instance = this;
		Init();
	}

	private void Init()
	{
		// BGM Player �ʱ�ȭ
		GameObject bgmObject = new GameObject("BGM Player");
		bgmObject.transform.parent = transform;
		bgmPlayer = bgmObject.AddComponent<AudioSource>();
		bgmPlayer.playOnAwake = false;
		bgmPlayer.loop = true;
		bgmPlayer.volume = BGMVolume;
		bgmPlayer.clip = BGMClip;
		bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

		// ȿ���� Palyer �ʱ�ȭ
		GameObject sfxObject = new GameObject("SFX Player");
		sfxObject.transform.parent = transform;
		sfxPlayers = new AudioSource[Channels];

		for (int i = 0; i < sfxPlayers.Length; i++) {
			sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
			sfxPlayers[i].playOnAwake = false;
			sfxPlayers[i].bypassListenerEffects = true;	// ���� �ȸ԰� 
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
			int loopIndex = (i + channelIndex) % sfxPlayers.Length; // �Ѿ�� �ٽ� 0������ �����ϵ���

			if (sfxPlayers[loopIndex].isPlaying) {  // ���� �÷������̸� ���־���
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
