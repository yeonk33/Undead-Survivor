using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[Header("# Game Object")]
	public PoolManager Pool;
	public Player Player;

	[Header("# Game Control")]
	public float GameTime;
	public float MaxGameTime = 2 * 10f;

	[Header("# Player Info")]
	public int Level;
	public int Kill;
	public int EXP;
	public int[] NextEXP = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		GameTime += Time.deltaTime;

		if (GameTime > MaxGameTime) {
			GameTime = MaxGameTime;
		}
	}

	public void GetEXP()
	{
		EXP++;
		if (EXP == NextEXP[Level]) {
			Level++;
			EXP = 0;
		}
	}
}
