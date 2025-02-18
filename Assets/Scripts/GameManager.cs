using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[Header("# Game Object")]
	public PoolManager Pool;
	public Player Player;
	public LevelUp UILevelUp;
	public Result UIResult;
	public GameObject EnemyCleaner;

	[Header("# Game Control")]
	public float GameTime;
	public float MaxGameTime = 2 * 10f;
	public bool IsLive;

	[Header("# Player Info")]
	public int PlayerId;
	public float Health;
	public float MaxHealth = 100;
	public int Level;
	public int Kill;
	public int EXP;
	public int[] NextEXP = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

	private void Awake()
	{
		Instance = this;
	}

	public void GameStart(int id)
	{
		PlayerId = id;
		Health = MaxHealth;		
		Player.gameObject.SetActive(true);

		// @@임시로 일단 플레이어 아이디의 무기 지급
		UILevelUp.Select(PlayerId % 2);
		Resume();
	}

	public void GameOver()
	{
		StartCoroutine(GameOverRoutine());
	}

	IEnumerator GameOverRoutine()
	{
		IsLive = false;

		yield return new WaitForSeconds(0.5f);

		UIResult.gameObject.SetActive(true);
		UIResult.Lose();
		Stop();
	}

	public void GameVictory()
	{
		StartCoroutine(GameVictoryRoutine());
	}

	IEnumerator GameVictoryRoutine()
	{
		IsLive = false;
		EnemyCleaner.SetActive(true);	// 모든 enemy kill

		yield return new WaitForSeconds(0.5f);

		UIResult.gameObject.SetActive(true);
		UIResult.Win();
		Stop();
	}

	public void GameRetry()
	{
		SceneManager.LoadScene(0);
	}

	private void Update()
	{
		if (!IsLive) return;

		GameTime += Time.deltaTime;

		if (GameTime > MaxGameTime) {
			GameTime = MaxGameTime;
			GameVictory();
		}
	}

	public void GetEXP()
	{
		if (!IsLive) return;

		EXP++;
		if (EXP == NextEXP[Mathf.Min(Level, NextEXP.Length-1)]) {	// 레벨업
			Level++;
			EXP = 0;
			UILevelUp.Show();
		}
	}

	public void Stop()
	{
		IsLive = false;
		Time.timeScale = 0;	// 유니티의 시간
	}

	public void Resume()
	{
		IsLive = true;
		Time.timeScale = 1; // 유니티의 시간
	}
}
