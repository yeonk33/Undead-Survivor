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

		Vector2 dirVector = Target.position - rigid.position;   // 방향
		Vector2 nextVector = dirVector.normalized * Speed * Time.fixedDeltaTime; // 다음에 가야할 위치
		
		// 현재 위치 + 다음 가야할 위치
		rigid.MovePosition(rigid.position + nextVector);
		rigid.velocity = Vector2.zero;	// 0이 아니면 밀려남
	}

	private void LateUpdate()
	{
		if (!isLive) return;
		spriteRenderer.flipX = Target.position.x < rigid.position.x;
	}
}
