using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public int Id;
	public int PrefabId;
	public float Damage;
	public int Count;
	public float Speed;

	private float timer;
	private Player player;

	private void Awake()
	{
		player = GameManager.Instance.Player;
	}

	private void Update()
	{
		switch (Id) {
			case 0:
				transform.Rotate(Vector3.forward * Speed * Time.deltaTime);	// (0,0,1)
				break;

			default:
				timer += Time.deltaTime;
				if (timer > Speed) {
					timer = 0;
					Fire();
				}
				break;
		}

		// test
		if (Input.GetButtonDown("Jump")) {
			LevelUp(20, 1);
		}
	}


	public void Init(ItemData data)
	{
		// Basic setting
		name = "Weapon " + data.ItemId;
		transform.parent = player.transform;	// Player�� �ڽ� ������Ʈ�� ���
		transform.localPosition = Vector3.zero; // �÷��̾� �Ʒ��� ��ġ���� (0,0,0)���� ����

		// Property setting
		Id = data.ItemId;
		Damage = data.BaseDamage;
		Count = data.BaseCount;

		for (int i = 0; i < GameManager.Instance.Pool.Prefabs.Length; i++) {
			if (data.Projectile == GameManager.Instance.Pool.Prefabs[i]) {
				PrefabId = i;
				break;
			}
		}

		switch (Id) {
			case 0:
				Speed = -150;   // ���̳ʽ����� �ð����
				Batch();
				break;

			default:
				Speed = 0.3f;	// ����ӵ� (0.3f�ʸ��� �߻�)
				break;
		}

		player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);   // ��� �ڽ� ������Ʈ���� �ش� �Լ� ���� ���
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

			bullet.GetComponent<Bullet>().Init(Damage, -1, Vector3.zero);	// -1�� ���� ����
		}
	}

	public void LevelUp(float dmg, int count)
	{
		this.Damage = dmg;
		this.Count += count;

		if (Id == 0) Batch();

		player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);	// ��� �ڽ� ������Ʈ���� �ش� �Լ� ���� ���
	}

	private void Fire()
	{
		if (!player.Scanner.NearestTarget) {    // null�̶��
			return;
		}

		Vector3 targetPosition = player.Scanner.NearestTarget.position;
		Vector3 dir = (targetPosition - transform.position).normalized;

		Transform bullet = GameManager.Instance.Pool.Get(PrefabId).transform;
		bullet.position = transform.position;
		bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
		bullet.GetComponent<Bullet>().Init(Damage, Count, dir);
	}
}
