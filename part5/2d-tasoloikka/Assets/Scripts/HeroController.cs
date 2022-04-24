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


  // Start is called before the first frame update
  void Start()
  {
    rbody = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();

  }

  void CheckGround()
  {
    isGrounded = false;

    if (rbody.position.y < deathZoneY)
    {
      Kill();
      return;
    }

    RaycastHit2D hitInfo = Physics2D.Raycast(rbody.position + (Vector2.down * 1.05f), Vector2.down, 0.05f);
    if (hitInfo && hitInfo.collider)
    {
      isGrounded = true;

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

    if (movementSpeed != 0)
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
