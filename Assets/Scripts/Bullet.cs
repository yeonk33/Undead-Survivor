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
	/// �ʱ�ȭ�Լ�
	/// </summary>
	/// <param name="dmg">������</param>
	/// <param name="per">����, -1�� ����</param>
	/// <param name="dir">�Ѿ� ����</param>
	public void Init(float dmg, int per, Vector3 dir)
	{
		this.Damage = dmg;
		this.Per = per;

		if (per >= 0) {	// ���Ÿ� ���⿡�� �Ʒ� ����
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

		gameObject.SetActive(false);	// �÷��̾��� Area ���̸� �Ѿ� ����
	}
}
