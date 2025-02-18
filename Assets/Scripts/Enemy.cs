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
	private Collider2D coll; // ĸ��, �ڽ�, �� ��� ��� ����
    private SpriteRenderer spriteRenderer;
	private Animator animator;
	private WaitForFixedUpdate waitFixed;

	private void OnEnable()
	{
		Target = GameManager.Instance.Player.GetComponent<Rigidbody2D>();
		isLive = true;
		coll.enabled = true;
		rigid.simulated = true;
		spriteRenderer.sortingOrder += 1;
		animator.SetBool("Dead", false);
		Health = MaxHealth;
		waitFixed = new WaitForFixedUpdate();
	}

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		// �׾��ų� Hit �ִϸ��̼� ������̸� �÷��̾������� �̵����� ����
						// �˹� �ִϸ��̼��� ���� ��
		if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

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
		if (!collision.CompareTag("Bullet") || !isLive) return;

		Health -= collision.GetComponent<Bullet>().Damage;
		StartCoroutine(KnockBack());

		if (Health > 0) {
			// Hit action
			animator.SetTrigger("Hit");
			AudioManager.Instance.PlaySFX(AudioManager.SFX.Hit);

		} else {
			isLive = false;
			coll.enabled = false;
			rigid.simulated = false;
			spriteRenderer.sortingOrder = 1;
			animator.SetBool("Dead", true);
			GameManager.Instance.Kill++;
			GameManager.Instance.GetEXP();

			if (GameManager.Instance.IsLive) {
				AudioManager.Instance.PlaySFX(AudioManager.SFX.Dead);
			}
		}
	}

	private IEnumerator KnockBack()
	{
		/*
		yield return null; // 1������ ����
		yield return new WaitForSeconds(2f); // 2�� ����
		*/

		yield return waitFixed; // ���� �ϳ��� ���� ������ ������

		Vector3 playerPosition = GameManager.Instance.Player.transform.position;
		Vector3 dirVector = transform.position - playerPosition;
		rigid.AddForce(dirVector.normalized, ForceMode2D.Impulse); // normalized�ϸ� ũ�Ⱑ 1�� �ǰ� ���⸸ ����
	}

	private void Dead()
	{
		// object pooling ������̹Ƿ� �ı�X ��Ȱ��ȭ��
		gameObject.SetActive(false);
	}
}
