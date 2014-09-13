using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {

	public float charHeight;
	public float charWidth;
	public float moveSpeed = 5f;
	public float jumpSpeed = 4f;
	public float jumpHeight = 1.1f;
	public float gravity = 4f;
	public bool jumping;

	protected Animator animator;

	private float cH;
	private float cW;
	private bool canJump;
	private Vector2 xInput;
	private Vector2 yInput;
	private Vector2 yGravity;
	private bool canMoveLeft;
	private bool canMoveRight;
	private bool canMoveUp;
	private bool canMoveDown;

	private int layerMask = 1 << 8;

	void Start()
	{
		cH = (charHeight / 2);
		cW = (charWidth / 2);
		canMoveLeft = false;
		canMoveRight = false;
		canMoveUp = false;
		canMoveDown = false;
		jumping = false;
		canJump = false;

		animator = GetComponent<Animator>();

		layerMask = ~layerMask;
	}

	void FixedUpdate()
	{
		xInput = Input.GetAxis ("Horizontal") * Vector2.right * moveSpeed * Time.deltaTime;
		yInput = Input.GetAxisRaw ("Vertical") * Vector2.up * jumpSpeed * Time.deltaTime;
		yGravity = -Vector2.up * gravity * Time.deltaTime;

		if (canMoveLeft && xInput.x < 0)
		{
			moveLeft(xInput);
			animator.SetBool("Walking", true);
			animator.SetBool("Forward", false);
		}
		else if (canMoveRight && xInput.x > 0)
		{
			moveRight(xInput);
			animator.SetBool("Walking", true);
			animator.SetBool("Forward", true);
    	}
		else
			animator.SetBool("Walking", false);
    
		if (canMoveUp && yInput.y > 0 & canJump)
		{
			canJump = false;
			float startPos = transform.position.y;
			StartCoroutine(jump(yInput, startPos));
		}
		else if (canMoveDown && !jumping)
			enableGravity();

		checkPosition();
	}

	/**
	 * Raycatsing to check position
	 */
	void checkPosition()
	{
		Vector2 castTRU = new Vector2(transform.position.x + cW, transform.position.y + cH);
		Vector2 castTLU = new Vector2(transform.position.x - cW, transform.position.y + cH);
		Vector2 castTLL = new Vector2(transform.position.x - cW, transform.position.y + cH);
		Vector2 castBLL = new Vector2(transform.position.x - cW, transform.position.y - cH);
		Vector2 castBLD = new Vector2(transform.position.x - cW, transform.position.y - cH);
		Vector2 castBRD = new Vector2(transform.position.x + cW, transform.position.y - cH);
		Vector2 castBRR = new Vector2(transform.position.x + cW, transform.position.y - cH);
		Vector2 castTRR = new Vector2(transform.position.x + cW, transform.position.y + cH);

		RaycastHit2D hitTRU = Physics2D.Raycast(castTRU, Vector2.up, 0.1f, layerMask);
		RaycastHit2D hitTLU = Physics2D.Raycast(castTLU, Vector2.up, 0.1f, layerMask);
		RaycastHit2D hitTLL = Physics2D.Raycast(castTLL, -Vector2.right, 0.1f, layerMask);
		RaycastHit2D hitBLL = Physics2D.Raycast(castBLL, -Vector2.right, 0.1f, layerMask);
		RaycastHit2D hitBLD = Physics2D.Raycast(castBLD, -Vector2.up, 0.1f, layerMask);
		RaycastHit2D hitBRD = Physics2D.Raycast(castBRD, -Vector2.up, 0.1f, layerMask);
		RaycastHit2D hitBRR = Physics2D.Raycast(castBRR, Vector2.right, 0.1f, layerMask);
		RaycastHit2D hitTRR = Physics2D.Raycast(castTRR, Vector2.right, 0.1f, layerMask);

		if (hitTRU.collider == null && hitTLU.collider == null)
			canMoveUp = true;
		else
			canMoveUp = false;
		if (hitTLL.collider == null && hitBLL.collider == null)
			canMoveLeft = true;
		else
			canMoveLeft = false;
		if (hitBLD.collider == null && hitBRD.collider == null)
			canMoveDown = true;
		else
		{
			canMoveDown = false;
			canJump = true;
		}
		if (hitBRR.collider == null && hitTRR.collider == null)
			canMoveRight = true;
		else
			canMoveRight = false;
	}

	void moveLeft(Vector2 xInput)
	{
		transform.Translate (xInput);
	}

	void moveRight(Vector2 xInput)
	{
		transform.Translate (xInput);
	}

	IEnumerator jump(Vector2 yInput, float startPos)
	{
		while (true)
		{
			float height = transform.position.y - startPos;
			if (height >= jumpHeight)
			{
				jumping = false;
				StopAllCoroutines();
			}
			else if (height < jumpHeight)
			{
				jumping = true;
				transform.Translate(yInput);
			}
			yield return new WaitForFixedUpdate();
		}
	}

	void enableGravity()
	{
		transform.Translate(yGravity);
	}
}