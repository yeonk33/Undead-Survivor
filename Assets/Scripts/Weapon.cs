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
				Speed = -150;   // ���̳ʽ����� �ð����
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
			bullet.parent = transform;  // ���Ӱ� ������ bullet�� Weapon �Ʒ��� �����..?

			Vector3 rotateVector = Vector3.forward * 360 * i / Count;	// 360���� ���� ������ŭ ������ ��ġ
			bullet.Rotate(rotateVector);
			bullet.Translate(bullet.up * 1.5f, Space.World);

			bullet.GetComponent<Bullet>().Init(Damage, -1);	// -1�� ���� ����
		}
	}

}
