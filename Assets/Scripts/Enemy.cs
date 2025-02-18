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
	private Collider2D coll; // 캡슐, 박스, 원 모양 상관 없음
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
		// 죽었거나 Hit 애니메이션 재생중이면 플레이어쪽으로 이동하지 않음
						// 넉백 애니메이션을 위한 것
		if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

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
		yield return null; // 1프레임 쉬기
		yield return new WaitForSeconds(2f); // 2초 쉬기
		*/

		yield return waitFixed; // 다음 하나의 물리 프레임 딜레이

		Vector3 playerPosition = GameManager.Instance.Player.transform.position;
		Vector3 dirVector = transform.position - playerPosition;
		rigid.AddForce(dirVector.normalized, ForceMode2D.Impulse); // normalized하면 크기가 1이 되고 방향만 남음
	}

	private void Dead()
	{
		// object pooling 사용중이므로 파괴X 비활성화만
		gameObject.SetActive(false);
	}
}
