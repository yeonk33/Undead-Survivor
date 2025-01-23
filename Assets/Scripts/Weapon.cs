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

		// test
		if (Input.GetButtonDown("Jump")) {
			LevelUp(20, 5);
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
			Transform bullet;

			if (i < transform.childCount) {		// 자식중에 있으면 있는거 쓰고
				bullet = transform.GetChild(i);
			} else {							// 없으면 오브젝트 풀링에서 가져온다
				bullet = GameManager.Instance.Pool.Get(PrefabId).transform;
				bullet.parent = transform;  // 새롭게 생성된 bullet이 Weapon 아래로 생긴다..?
			}

			bullet.localPosition = Vector3.zero;	// 현재 플레이어의 위치로 초기화 (Player의 자식으로 들어가있으니까)
			bullet.localRotation = Quaternion.identity;

			Vector3 rotateVector = Vector3.forward * 360 * i / Count;	// 360도를 무기 개수만큼 나눠서 배치
			bullet.Rotate(rotateVector);
			bullet.Translate(bullet.up * 1.5f, Space.World);

			bullet.GetComponent<Bullet>().Init(Damage, -1);	// -1은 무한 관통
		}
	}

	public void LevelUp(float dmg, int count)
	{
		this.Damage = dmg;
		this.Count += count;

		if (Id == 0) Batch();




	}
}
