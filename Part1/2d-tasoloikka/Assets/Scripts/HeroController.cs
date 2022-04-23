using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
  public float movementSpeed = 5.0f;
  
  Rigidbody2D rbody;
  float movementX = 0;

  // Start is called before the first frame update
  void Start()
  {
    rbody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    movementX = Input.GetAxis("Horizontal");

    Vector2 velocity = rbody.velocity;
    velocity.x = movementX * movementSpeed;


    rbody.velocity = velocity;
  }
}
