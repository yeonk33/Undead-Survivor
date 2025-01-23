using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
	public float Health;
	public float MaxHealth;
	public RuntimeAnimatorController[] AnimControllers;
    public Rigidbody2D Target;

    private bool isLive;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
	private Animator animator;

	private void OnEnable()
	{
		Target = GameManager.Instance.Player.GetComponent<Rigidbody2D>();
		isLive = true;
		Health = MaxHealth;
	}

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
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

	public void Init(SpawnData data)
	{
		animator.runtimeAnimatorController = AnimControllers[data.SpriteType];
		Speed = data.Speed;
		MaxHealth = data.Health;
		Health = data.Health;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Bullet")) return;

		Health -= collision.GetComponent<Bullet>().Damage;

		if (Health > 0) {
			// Hit action

		} else {
			Dead();
		}

	}

	private void Dead()
	{
		// object pooling ������̹Ƿ� �ı�X ��Ȱ��ȭ��
		gameObject.SetActive(false);
	}
}
