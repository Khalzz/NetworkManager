using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Movement : NetworkBehaviour
{
    [SerializeField] int playerSpeed;

    private Rigidbody2D rb;

    public Transform footPosition;
    public LayerMask whatIsFloor;
    public float footRadio;
    public bool itsInTheFloor;

    public float jumpForce;
    public float jumpTime; //0.2
    private float jumpCount;
    private bool itsJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Cursor.visible = false;

    }

    void Update()
    {
        if (!IsOwner) return;

        // walking
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(x, y);
        Walk(direction);

        // jumping
        itsInTheFloor = Physics2D.OverlapCircle(footPosition.position, footRadio, whatIsFloor);

        if (itsInTheFloor == true && Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            itsJumping = true;
            jumpCount = jumpTime;
        }

        if (Input.GetButton("Jump") && itsJumping == true)
        {
            if (jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount -= Time.deltaTime;
            }
            else
            {
                itsJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            itsJumping = false;
        }
    }


    private void Walk(Vector2 direction)
    {
        rb.velocity = (new Vector2(direction.x * playerSpeed, rb.velocity.y));
    }
}
