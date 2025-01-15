using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 3.0f;

    private Vector2 inputVec;
    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

	private void FixedUpdate()
	{
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
		rigid.MovePosition(rigid.position + nextVec);
	}

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
