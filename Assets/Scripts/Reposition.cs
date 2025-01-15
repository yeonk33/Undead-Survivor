using UnityEngine;

public class Reposition : MonoBehaviour
{
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
			default:
				break;
		}
	}
}
