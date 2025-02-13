using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    public Vector2 inputVec;
    public Scanner Scanner;
    public Hand[] Hands;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Scanner = GetComponent<Scanner>();
        Hands = GetComponentsInChildren<Hand>(true); // true : 비활성화된 자식도 Get
    }

	private void FixedUpdate()
	{
        if (!GameManager.Instance.IsLive) return;
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
		rigid.MovePosition(rigid.position + nextVec);
	}

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

	private void LateUpdate()
	{
		if (!GameManager.Instance.IsLive) return;
		animator.SetFloat("Speed", inputVec.magnitude);

		if (inputVec.x != 0) {
			spriteRenderer.flipX = inputVec.x < 0;
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (!GameManager.Instance.IsLive) return;

        GameManager.Instance.Health -= Time.deltaTime * 10;

        // 사망
        if (GameManager.Instance.Health < 0) {
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild(i).gameObject.SetActive(false);
			}

            animator.SetTrigger("Dead");
            GameManager.Instance.GameOver();
		}
	}
}
