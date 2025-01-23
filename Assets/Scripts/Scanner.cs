using UnityEngine;

public class Scanner : MonoBehaviour
{
	public float ScanRange;
	public LayerMask TargetLayer;
	public RaycastHit2D[] Targets;
	public Transform NearestTarget; // 가장 가까운 몹

	private void FixedUpdate()
	{
		// 원형으로 플레이어 주변을 검사하겠다
		Targets = Physics2D.CircleCastAll(transform.position, ScanRange, Vector2.zero, 0, TargetLayer);
		NearestTarget = GetNearest();
	}

	private Transform GetNearest()
	{
		Transform result = null;
		float diff = 100;

		foreach (RaycastHit2D target in Targets) {
			Vector3 playerPosition = transform.position;
			Vector3 targetPosition = target.transform.position;
			float currentDiff = Vector3.Distance(playerPosition, targetPosition);

			if (currentDiff < diff) {
				diff = currentDiff;
				result = target.transform;
			}
		}
		return result;
	}
}
