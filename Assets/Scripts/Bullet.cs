using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float Damage;
	public int Per;

	/// <summary>
	/// �ʱ�ȭ�Լ�
	/// </summary>
	/// <param name="dmg">������</param>
	/// <param name="per">����, -1�� ����</param>
	public void Init(float dmg, int per)
	{
		this.Damage = dmg;
		this.Per = per;
	}
}
