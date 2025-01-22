using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public Transform[] SpawnPoints;
	public SpawnData[] SpawnDatas;

	private float timer;
	private int level;

	private void Awake()
	{
		// GetComponent"s"Children : s는 여러개
		// Children은 자기 자신도 포함이므로 자식만 접근하려면 0번째는 제외해야함 
		SpawnPoints = GetComponentsInChildren<Transform>();
	}

	private void Update()
	{
		timer += Time.deltaTime;
		level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.GameTime / 10f), SpawnDatas.Length - 1);

		// level에 맞는 SpawnTime을 가져다 씀
		if (timer > SpawnDatas[level].SpawnTime) {
			timer = 0f;
			Spawn();
		}
	}

	private void Spawn()
	{
		GameObject enemy = GameManager.Instance.Pool.Get(0);
		enemy.transform.position = SpawnPoints[Random.Range(1, SpawnPoints.Length)].position;
		enemy.GetComponent<Enemy>().Init(SpawnDatas[level]);
	}
}

[System.Serializable]
public class SpawnData
{
	public float SpawnTime;
	public int SpriteType;
	public int Health;
	public float Speed;
}
