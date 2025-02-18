using UnityEngine;

public class Reposition : MonoBehaviour
{
	private Collider2D coll;
	
	private void Awake()
	{
		coll = GetComponent<Collider2D>();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.CompareTag("Area"))
			return;

		Vector3 playerPosition = GameManager.Instance.Player.transform.position;
		Vector3 myPosition = transform.position;
		
		switch (transform.tag) {
			case "Ground":
				float diffX = playerPosition.x - myPosition.x;
				float diffY = playerPosition.y - myPosition.y;
				float dirX = diffX < 0 ? -1 : 1;
				float dirY = diffY < 0 ? -1 : 1;
				diffX = Mathf.Abs(diffX);
				diffY = Mathf.Abs(diffY);
			
				if (diffX > diffY) {	// 수평이동
					transform.Translate(Vector3.right * dirX * 40);

				} else if (diffX < diffY) {	// 수직이동
					transform.Translate(Vector3.up * dirY * 40);
				}

				break;

			case "Enemy":
				if (coll.enabled) { // 몹이 살아있다
					transform.Translate(playerDir * 20 
						+ new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0)); // 너무 가까이 스폰되면 카메라에 보일수 있음
				}
				break;
			default:
				break;
		}
	}
}
