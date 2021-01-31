using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float playerJump;
    public static bool playerCanMove = true;
    [SerializeField] private Animator animator;
    [SerializeField] private CapsuleCollider2D cc2D;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] string[] groundedLayersList;
    [SerializeField] float footStepsPlaySpeed = .5f;
    [SerializeField] AudioSource[] footStepsAudioSources;
    private int groundedLayers;
    private bool jumping = false;
    private bool postJumpGroundCheck = false;
    private bool pAFX_Running = false;
    //false: player has been instructed to jump but has not left ground yet
    //true: player ahs been instructed to jump and has now left the ground
    [SerializeField] private int restartSceneIndex;
    private void Start()
    {
        groundedLayers = LayerMask.GetMask(groundedLayersList);

        foreach (string joystickName in Input.GetJoystickNames())
        {
            print("playerMovement: controller connected with name " + joystickName.ToLower());
        }
    }

    private void Update()
    {

        // restart
        if (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.R))
        {
            //load
            SceneManager.LoadScene(restartSceneIndex);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (playerCanMove)
        {
            // move player
            float inputX = Input.GetAxisRaw("Horizontal");
            float velocity = inputX * playerSpeed;

            // face direction of movement
            if (inputX == -1f)
            {
                //print("Face Left.");
                transform.eulerAngles = new Vector3(transform.rotation.x,
                    -180f,
                    transform.rotation.y);
                transform.Translate(Vector2.left * velocity * Time.deltaTime);
            }
            else if (inputX == 1f)
            {
                //print("Face Right.");
                transform.eulerAngles = new Vector3(transform.rotation.x,
                    0f,
                    transform.rotation.y);
                transform.Translate(Vector2.right * velocity * Time.deltaTime);
            }

            // jump player
            if (Input.GetButtonDown("Jump") && IsGrounded())
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

            // play animations
            if (Mathf.Abs(inputX) > 0 && IsGrounded() && !jumping && !Input.GetButtonDown("Fire1"))
            {
                animator.SetBool("running", true);
                animator.SetBool("jumping", false);
                animator.SetBool("falling", false);
                animator.SetBool("throwing", false);
            }
            else if (Mathf.Abs(inputX) == 0 && IsGrounded() && !jumping && Input.GetButtonDown("Fire1"))
            {
                animator.SetBool("running", false);
                animator.SetBool("jumping", false);
                animator.SetBool("falling", false);
                animator.SetBool("throwing", true);
            }
            else if (!IsGrounded() && jumping)
            {
                animator.SetBool("running", false);
                animator.SetBool("jumping", true);
                animator.SetBool("falling", false);
                animator.SetBool("throwing", false);
            }
            else if (!IsGrounded() && !jumping)
            {
                animator.SetBool("running", false);
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
                animator.SetBool("throwing", false);
            }
            else
            {
                animator.SetBool("running", false);
                animator.SetBool("jumping", false);
                animator.SetBool("falling", false);
                animator.SetBool("throwing", false);
            }

            // play sounds
            if (!pAFX_Running)
            {
                StartCoroutine(PlayAudioFX());
            }
        }
        else
        {
            animator.SetBool("running", false);
            animator.SetBool("jumping", false);
            animator.SetBool("falling", false);
            animator.SetBool("throwing", false);
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D rch2D = Physics2D.BoxCast(cc2D.bounds.center, cc2D.bounds.size, 0f, Vector2.down, .9f, groundedLayers);
        return rch2D.collider != null;
    }

    IEnumerator PlayAudioFX() 
    {
        pAFX_Running = true;

        while (animator.GetBool("running")) {
            footStepsAudioSources[Random.Range(0, footStepsAudioSources.Length)].Play();
            yield return new WaitForSeconds(footStepsPlaySpeed);
        }

        pAFX_Running = false;
    }

}
