using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplePlayerMovement : MonoBehaviour
{
    public float playerSpeed = 2f;
    public float playerJump = 5f;
    [SerializeField] private CircleCollider2D cc2D;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] string[] groundedLayersList;
    private int groundedLayers;
    private bool jumping = false;
    private bool postJumpGroundCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        groundedLayers = LayerMask.GetMask(groundedLayersList);
    }

    // Update is called once per frame
    void Update()
    {
        // move player
        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * playerSpeed;
        transform.Translate(Vector2.right * velocity * Time.deltaTime);

        // jump player
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb2D.velocity = Vector2.up * playerJump;
            jumping = true;
            postJumpGroundCheck = false;
        }
        else if (jumping && !IsGrounded() && !postJumpGroundCheck)
        {
            postJumpGroundCheck = true;
        }
        else if (jumping && IsGrounded() && postJumpGroundCheck)
        {
            jumping = false;
            postJumpGroundCheck = false;
        }
        // else just let it do what it is doing

    }
    bool IsGrounded()
    {
        RaycastHit2D rch2D = Physics2D.BoxCast(cc2D.bounds.center, cc2D.bounds.size, 0f, Vector2.down, .5f, groundedLayers);
        return rch2D.collider != null;
    }
}
