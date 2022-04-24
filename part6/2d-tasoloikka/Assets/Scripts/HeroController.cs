using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
  public float movementSpeed = 5.0f;
  public float jumpForce = 12.0f;
  public bool isGrounded;
  public bool isJumping;
  public bool isAlive = true;

  private Animator anim;
  Rigidbody2D rbody;
  SpriteRenderer spriteRenderer;

  public float deathZoneY = -7;

  float movementX = 0;

  public float coyoteTimerDefault = 1.0f;
  public float coyoteTimer = 0.0f;


  // Start is called before the first frame update
  void Start()
  {
    rbody = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();

    coyoteTimer = coyoteTimerDefault;

  }

  void CheckGround()
  {
    if (coyoteTimer < 0)
      isGrounded = false;

    if (rbody.position.y < deathZoneY)
    {
      Kill();
      return;
    }

    float collisionWidth = 0.4f;

    RaycastHit2D hitInfo = Physics2D.Raycast(rbody.position + (new Vector2(-collisionWidth / 2, -1.05f)), Vector2.right, collisionWidth);



    if (hitInfo && hitInfo.collider)
    {
      isGrounded = true;
      coyoteTimer = coyoteTimerDefault;

      if (hitInfo.collider.tag == "Enemy")
      {
        hitInfo.collider.GetComponent<EnemyController>().Kill();
        isJumping = true;
      }
    }
  }

  void ProcessInputs()
  {
    movementX = Input.GetAxis("Horizontal");

    if (Input.GetAxis("Jump") > 0)
    {
      if (isGrounded)
        isJumping = true;

    }
  }

  void ProcessMovement()
  {
    Vector2 velocity = rbody.velocity;
    CheckGround();


    if (isGrounded && isJumping)
    {
      velocity.y = jumpForce;
      isJumping = false;
      isGrounded = false;
    }

    velocity.x = movementX * movementSpeed;
    rbody.velocity = velocity;


  }

  void ProcessAnimation()
  {

    if (movementX != 0)
      spriteRenderer.flipX = movementX > 0 ? false : true;

    anim.SetBool("isAlive", isAlive);
    anim.SetBool("isJumping", !isGrounded);

    if (movementX != 0)
      anim.SetBool("isWalking", true);
    else
      anim.SetBool("isWalking", false);
  }

  // Update is called once per frame
  void Update()
  {
    if (isAlive)
      ProcessInputs();

    ProcessAnimation();

    if (coyoteTimer > 0)
      coyoteTimer -= Time.deltaTime;

  }

  void FixedUpdate()
  {
    ProcessMovement();
  }

  public void Kill()
  {
    if (isAlive)
    {
      isAlive = false;
      rbody.velocity = Vector2.zero;
      movementX = 0;
    }
  }


}
