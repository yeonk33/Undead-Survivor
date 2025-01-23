using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    public Vector2 inputVec;
    public Scanner Scanner;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Scanner = GetComponent<Scanner>();
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

	private void LateUpdate()
	{
        animator.SetFloat("Speed", inputVec.magnitude);

		if (inputVec.x != 0) {
			spriteRenderer.flipX = inputVec.x < 0;
		}
	}
}
