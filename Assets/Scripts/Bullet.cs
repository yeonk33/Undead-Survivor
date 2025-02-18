using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float Damage;
	public int Per;

	private Rigidbody2D rigid;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
	}

	/// <summary>
	/// 초기화함수
	/// </summary>
	/// <param name="dmg">데미지</param>
	/// <param name="per">관통, -1은 무한</param>
	/// <param name="dir">총알 방향</param>
	public void Init(float dmg, int per, Vector3 dir)
	{
		this.Damage = dmg;
		this.Per = per;

		if (per >= 0) {	// 원거리 무기에만 아래 적용
			rigid.velocity = dir * 15f;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Enemy") || Per == -100) return;

		Per--;
		if (Per <= 0) {
			rigid.velocity = Vector2.zero;
			gameObject.SetActive(false);	// object pooling
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.CompareTag("Area") || Per == -100) return;

		gameObject.SetActive(false);	// 플레이어의 Area 밖이면 총알 삭제
	}
}
