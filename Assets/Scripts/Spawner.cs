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
		// GetComponent"s"Children : s�� ������
		// Children�� �ڱ� �ڽŵ� �����̹Ƿ� �ڽĸ� �����Ϸ��� 0��°�� �����ؾ��� 
		SpawnPoints = GetComponentsInChildren<Transform>();
	}

	private void Update()
	{
		timer += Time.deltaTime;
		level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.GameTime / 10f), SpawnDatas.Length - 1);

		// level�� �´� SpawnTime�� ������ ��
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
