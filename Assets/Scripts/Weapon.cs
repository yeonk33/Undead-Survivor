using UnityEngine;

public class Weapon : MonoBehaviour
{
	public int Id;
	public int PrefabId;
	public float Damage;
	public int Count;
	public float Speed;

	private void Start()
	{
		Init();
	}

	private void Update()
	{
		switch (Id) {
			case 0:
				transform.Rotate(Vector3.forward * Speed * Time.deltaTime);	// (0,0,1)
				break;

			default:
				break;
		}
	}

	public void Init()
	{
		switch (Id) {
			case 0:
				Speed = -150;   // 마이너스여야 시계방향
				Batch();
				break;

			default:
				break;
		}
	}

	private void Batch()
	{
		for (int i = 0; i < Count; i++) {
			Transform bullet = GameManager.Instance.Pool.Get(PrefabId).transform;
			bullet.parent = transform;  // 새롭게 생성된 bullet이 Weapon 아래로 생긴다..?

			Vector3 rotateVector = Vector3.forward * 360 * i / Count;	// 360도를 무기 개수만큼 나눠서 배치
			bullet.Rotate(rotateVector);
			bullet.Translate(bullet.up * 1.5f, Space.World);

			bullet.GetComponent<Bullet>().Init(Damage, -1);	// -1은 무한 관통
		}
	}

}
