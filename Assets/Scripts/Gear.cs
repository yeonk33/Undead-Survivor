using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
	public ItemData.ItemType Type;
	public float Rate;

	/// <summary>
	/// ��� ����
	/// </summary>
	/// <param name="data"></param>
	public void Init(ItemData data)
	{
		// �⺻ ����
		name = "Gear " + data.ItemId;
		transform.parent = GameManager.Instance.Player.transform;
		transform.localPosition = Vector3.zero;

		// �Ӽ� ����
		Type = data.ItemTypeValue;
		Rate = data.Damages[0];

		ApplyGear();
	}

	public void LevelUp(float rate)
	{
		this.Rate = rate;
		ApplyGear();
	}

	private void ApplyGear()
	{
		switch (Type) {
			case ItemData.ItemType.Melee:
				break;
			case ItemData.ItemType.Range:
				break;
			case ItemData.ItemType.Glove:
				RateUp();
				break;
			case ItemData.ItemType.Shoe:
				SpeedUp();
				break;
			case ItemData.ItemType.Heal:
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// ���� ���� �Լ� (�尩)
	/// </summary>
	private void RateUp()
	{
		Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

		foreach (var item in weapons) {
			switch (item.Id) {
				case 0:
					item.Speed = 150 + (150 * Rate);
					break;

				default:
					item.Speed = 0.5f * (1f - Rate);
					break;
					
			}
		}
	}

	/// <summary>
	/// �̼� ���� �Լ� (�Ź�)
	/// </summary>
	private void SpeedUp()
	{
		float speed = 3f;
		GameManager.Instance.Player.speed = speed + speed * Rate;

	}
}
