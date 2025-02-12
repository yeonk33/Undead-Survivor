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
		// 1. ��� ������ ��Ȱ��ȭ
		foreach (var item in items) {
			item.gameObject.SetActive(false);
		}

		// 2. ���� 3�� ������ Ȱ��ȭ
		int[] ran = new int[3];
		while (true) {
			ran[0] = Random.Range(0, items.Length);
			ran[1] = Random.Range(0, items.Length);
			ran[2] = Random.Range(0, items.Length);

			if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2]) { // ��� �������� �ٸ��� break
				break;
			}
		}

		for (int i = 0; i < ran.Length; i++) {
			Item ranItem = items[ran[i]];

			// 3. ���� �������� �Һ� ���������� ��ü
			if (ranItem.Level == ranItem.Data.Damages.Length) {
				items[4].gameObject.SetActive(true);	// items[4]�� ü��ȸ��(��ȸ��) �������� 

			} else {
				ranItem.gameObject.SetActive(true);
			}
		}
	}
}
