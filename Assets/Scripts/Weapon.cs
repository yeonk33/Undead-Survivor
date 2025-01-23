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
			Transform bullet;

			if (i < transform.childCount) {		// �ڽ��߿� ������ �ִ°� ����
				bullet = transform.GetChild(i);
			} else {							// ������ ������Ʈ Ǯ������ �����´�
				bullet = GameManager.Instance.Pool.Get(PrefabId).transform;
				bullet.parent = transform;  // ���Ӱ� ������ bullet�� Weapon �Ʒ��� �����..?
			}

			bullet.localPosition = Vector3.zero;	// ���� �÷��̾��� ��ġ�� �ʱ�ȭ (Player�� �ڽ����� �������ϱ�)
			bullet.localRotation = Quaternion.identity;

			Vector3 rotateVector = Vector3.forward * 360 * i / Count;	// 360���� ���� ������ŭ ������ ��ġ
			bullet.Rotate(rotateVector);
			bullet.Translate(bullet.up * 1.5f, Space.World);

			bullet.GetComponent<Bullet>().Init(Damage, -1);	// -1�� ���� ����
		}
	}

	public void LevelUp(float dmg, int count)
	{
		this.Damage = dmg;
		this.Count += count;

		if (Id == 0) Batch();




	}
}
