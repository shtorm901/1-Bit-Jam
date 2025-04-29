using UnityEngine;
using UnityEngine.Rendering;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;

    private float MoveX;

    public float Speed;
    public float JumpForce;

    private bool isGround;

    public bool FaceRight;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
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
    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector2.up * JumpForce);
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