using UnityEngine;
using UnityEngine.Events;

public class PlayerController : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private AnimationHandler smokingHand = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private Rigidbody2D playerRb = null;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (playerRb == null)
            playerRb = GetComponent<Rigidbody2D>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x >= 0f) : (move.x < 0f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        //   animator.SetBool ("grounded", grounded);
        // animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
        if (targetVelocity == Vector2.zero)
            animator.SetBool("WalkOn", false);
        else
            animator.SetBool("WalkOn", true);
        playerRb.velocity = targetVelocity;
    }

    public void StopPlayer()
    {
        playerRb.velocity = Vector2.zero;
        animator.SetBool("WalkOn", false);
        this.enabled = false;
    }


}