using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;

    private bool isJumping;
    private bool isGrounded;
    public LayerMask collisionLayers;

    public Transform groundCheck;
    private float horizontalmove;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.25f, collisionLayers);
        horizontalmove = Input.GetAxis("Horizontal") * 400 * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer(horizontalmove);
    }
    void MovePlayer (float _horizontalmove)
    {
        Vector3 targetVelocity = new Vector2(_horizontalmove, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, 250));
            isJumping = false;
        }
    }
}
