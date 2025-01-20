using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D Target;

    private bool isLive = true;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void FixedUpdate()
	{
		if (!isLive) return;

		Vector2 dirVector = Target.position - rigid.position;   // ����
		Vector2 nextVector = dirVector.normalized * Speed * Time.fixedDeltaTime; // ������ ������ ��ġ
		
		// ���� ��ġ + ���� ������ ��ġ
		rigid.MovePosition(rigid.position + nextVector);
		rigid.velocity = Vector2.zero;	// 0�� �ƴϸ� �з���
	}

	private void LateUpdate()
	{
		if (!isLive) return;
		spriteRenderer.flipX = Target.position.x < rigid.position.x;
	}
}
