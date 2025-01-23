using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float Damage;
	public int Per;

	/// <summary>
	/// 초기화함수
	/// </summary>
	/// <param name="dmg">데미지</param>
	/// <param name="per">관통, -1은 무한</param>
	public void Init(float dmg, int per)
	{
		this.Damage = dmg;
		this.Per = per;
	}
}
