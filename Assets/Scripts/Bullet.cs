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

		if (per > -1) {	// ���Ÿ� ���⿡�� �Ʒ� ����
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
