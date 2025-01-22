using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	// 프리팹들을 보관할 변수	
	public GameObject[] Prefabs;

	// 풀 담당을 하는 리스트	
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

		// 선택한 풀의 비활성화된 게임오브젝트를 return해줄것임
		foreach (GameObject item in Pools[index]) {
			if (!item.activeSelf) {
				select = item;
				select.SetActive(true);
				break;
			}
		}

		// 모두 사용중이면 새롭게 생성하여 return
		if (select == null) {
			// transform : PoolManager 오브젝트의 자식으로 생성
			select = Instantiate(Prefabs[index], transform); 
			Pools[index].Add(select);
		}

		return select;
	}
}
