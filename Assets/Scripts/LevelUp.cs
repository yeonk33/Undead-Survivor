using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
	RectTransform rect;
	Item[] items;

	private void Awake()
	{
		rect = GetComponent<RectTransform>();
		items = GetComponentsInChildren<Item>(true);
	}

	public void Show()
	{
		Next();
		rect.localScale = Vector3.one;
		GameManager.Instance.Stop();
	}

	public void Hide()
	{
		rect.localScale = Vector3.zero;
		GameManager.Instance.Resume();
	}

	public void Select(int index)
	{
		items[index].OnClick();
	}

	private void Next()
	{
		// 1. 모든 아이템 비활성화
		foreach (var item in items) {
			item.gameObject.SetActive(false);
		}

		// 2. 랜덤 3개 아이템 활성화
		int[] ran = new int[3];
		while (true) {
			ran[0] = Random.Range(0, items.Length);
			ran[1] = Random.Range(0, items.Length);
			ran[2] = Random.Range(0, items.Length);

			if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2]) { // 모든 아이템이 다르면 break
				break;
			}
		}

		for (int i = 0; i < ran.Length; i++) {
			Item ranItem = items[ran[i]];

			// 3. 만렙 아이템은 소비 아이템으로 대체
			if (ranItem.Level == ranItem.Data.Damages.Length) {
				items[4].gameObject.SetActive(true);	// items[4]가 체력회복(일회성) 아이템임 

			} else {
				ranItem.gameObject.SetActive(true);
			}
		}
	}
}
