using UnityEngine;
using UnityEngine.Rendering;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    private float MoveX;

    public float Speed;
    public float JumpForce;

    private bool isGround;

    public bool FaceRight;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if(isGround == false)
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isJump", false);
        }

        Jump();

        if(!FaceRight && MoveX < 0)
        {
            Flip();
        }
        else if(FaceRight && MoveX > 0)
        {
            Flip();
        }
    }

    public void FixedUpdate()
    {
        Run();
    }

    public void Run()
    {
        MoveX = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(MoveX * Speed, rb.linearVelocity.y);

        if(MoveX != 0)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            anim.SetBool("isJump", true);
            rb.AddForce(Vector2.up * JumpForce);
        }
        else
        {
            anim.SetBool("isJump", false);
        }
    }

    public void Flip()
    {
        FaceRight = !FaceRight;
        transform.Rotate(0, 180, 0);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }
}