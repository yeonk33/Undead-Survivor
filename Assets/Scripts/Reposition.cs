using UnityEngine;

public class Reposition : MonoBehaviour
{
	private Collider2D collider;
	
	private void Awake()
	{
		collider = GetComponent<Collider2D>();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.CompareTag("Area"))
			return;

		Vector3 playerPosition = GameManager.instance.player.transform.position;
		Vector3 myPosition = transform.position;
		float diffX = Mathf.Abs(playerPosition.x - myPosition.x);
		float diffY = Mathf.Abs(playerPosition.y - myPosition.y);

		Vector3 playerDir = GameManager.instance.player.inputVec;
		float dirX = playerDir.x < 0 ? -1 : 1;
		float dirY = playerDir.y < 0 ? -1 : 1;

		switch (transform.tag) {
			case "Ground":
				if (diffX > diffY) {	// �����̵�
					transform.Translate(Vector3.right * dirX * 40);

				} else if (diffX < diffY) {	// �����̵�
					transform.Translate(Vector3.up * dirY * 40);
				}

				break;

			case "Enemy":
				if (collider.enabled) { // ���� ����ִ�
					transform.Translate(playerDir * 20 
						+ new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0)); // �ʹ� ������ �����Ǹ� ī�޶� ���ϼ� ����
				}
				break;
			default:
				break;
		}
	}
}
