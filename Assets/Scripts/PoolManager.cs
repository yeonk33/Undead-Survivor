using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	// �����յ��� ������ ����	
	public GameObject[] Prefabs;

	// Ǯ ����� �ϴ� ����Ʈ	
	private List<GameObject>[] Pools;

	private void Awake()
	{
		Pools = new List<GameObject>[Prefabs.Length];

		for (int i = 0; i < Pools.Length; i++) {
			Pools[i] = new List<GameObject>();
		}
	}

	public GameObject Get(int index)
	{
		GameObject select = null;

		// ������ Ǯ�� ��Ȱ��ȭ�� ���ӿ�����Ʈ�� return���ٰ���
		foreach (GameObject item in Pools[index]) {
			if (!item.activeSelf) {
				select = item;
				select.SetActive(true);
				break;
			}
		}

		// ��� ������̸� ���Ӱ� �����Ͽ� return
		if (select == null) {
			// transform : PoolManager ������Ʈ�� �ڽ����� ����
			select = Instantiate(Prefabs[index], transform); 
			Pools[index].Add(select);
		}

		return select;
	}
}
