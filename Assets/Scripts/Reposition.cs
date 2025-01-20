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
				if (diffX > diffY) {	// 수평이동
					transform.Translate(Vector3.right * dirX * 40);

				} else if (diffX < diffY) {	// 수직이동
					transform.Translate(Vector3.up * dirY * 40);
				}

				break;

			case "Enemy":
				if (collider.enabled) { // 몹이 살아있다
					transform.Translate(playerDir * 20 
						+ new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0)); // 너무 가까이 스폰되면 카메라에 보일수 있음
				}
				break;
			default:
				break;
		}
	}
}
