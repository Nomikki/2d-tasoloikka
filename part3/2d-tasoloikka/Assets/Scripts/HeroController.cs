using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
  public float movementSpeed = 5.0f;
  public float jumpForce = 12.0f;
  public bool isGrounded;

  Rigidbody2D rbody;
  float movementX = 0;

  // Start is called before the first frame update
  void Start()
  {
    rbody = GetComponent<Rigidbody2D>();
  }

  void CheckGround() {
      isGrounded = false;

    RaycastHit2D hitInfo = Physics2D.Raycast(rbody.position, Vector2.down, 1.05f);
    if (hitInfo && hitInfo.collider) {
        isGrounded = true;
    }
  }

  // Update is called once per frame
  void Update()
  {
    Vector2 velocity = rbody.velocity;

    movementX = Input.GetAxis("Horizontal");

    CheckGround();

    if (Input.GetAxis("Jump") > 0 && isGrounded) {
        velocity.y = jumpForce;
    }


    
    velocity.x = movementX * movementSpeed;


    rbody.velocity = velocity;
  }
}
