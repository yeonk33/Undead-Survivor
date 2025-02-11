using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	public bool isLeft;
	public SpriteRenderer sprite;

	private SpriteRenderer player;
	private Vector3 rightPosition = new Vector3(0.32f, -0.16f, 0);
	private Vector3 rightPositionReverse = new Vector3(0.032f, -0.195f, 0);
	private Quaternion leftRotation = Quaternion.Euler(0, 0, -35);
	private Quaternion leftRotationReverse = Quaternion.Euler(0, 0, -135);

	private void Awake()
	{
		player = GetComponentsInParent<SpriteRenderer>()[1];
	}

	private void LateUpdate()
	{
		bool isReverse = player.flipX;
		if (isLeft) {	// 근접무기
			transform.localRotation = isReverse ? leftRotationReverse : leftRotation;
			sprite.flipY = isReverse;
			sprite.sortingOrder = isReverse ? 4 : 6;
		} else {	// 원거리 무기
			transform.localPosition = isReverse ? rightPositionReverse : rightPosition;
			sprite.flipX = isReverse;
			sprite.sortingOrder = isReverse ? 6 : 4;
		}
	}
}
