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

		if (per > -1) {	// 원거리 무기에만 아래 적용
			rigid.velocity = dir;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Enemy") || Per == -1) return;

		Per--;
		if (Per == -1) {
			rigid.velocity = Vector2.zero;
			gameObject.SetActive(false);	// object pooling
		}
	}
}
