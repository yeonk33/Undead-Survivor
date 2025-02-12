using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[Header("# Game Object")]
	public PoolManager Pool;
	public Player Player;
	public LevelUp UILevelUp;

	[Header("# Game Control")]
	public float GameTime;
	public float MaxGameTime = 2 * 10f;
	public bool IsLive;

	[Header("# Player Info")]
	public int Health;
	public int MaxHealth = 100;
	public int Level;
	public int Kill;
	public int EXP;
	public int[] NextEXP = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		Health = MaxHealth;
		IsLive = true;
		// @@임시로 일단 근접무기 지급
		UILevelUp.Select(0);
	}

	private void Update()
	{
		if (!IsLive) return;

		GameTime += Time.deltaTime;

		if (GameTime > MaxGameTime) {
			GameTime = MaxGameTime;
		}
	}

	public void GetEXP()
	{
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
