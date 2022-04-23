using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

  public float movementDirection = 1;
  public float movementSpeed = 2.5f;
  public float activeDistance = 20.0f;
  public bool isAlive = true;
  Rigidbody2D rbody;
  SpriteRenderer spriteRenderer;
  BoxCollider2D boxCollider;

  HeroController hero;
  Vector2 startPosition;
  float startDirection;


  // Start is called before the first frame update
  void Start()
  {
    rbody = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    boxCollider = GetComponent<BoxCollider2D>();
    startDirection = movementDirection;

    startPosition = rbody.position;
    hero = GameObject.Find("Hero").GetComponent<HeroController>();;

  }

  void CheckCollision()
  {

    if (rbody.position.y < hero.deathZoneY)
    {
      Kill();
      return;
    }

    Vector2 direction = new Vector2(movementDirection, 0);
    RaycastHit2D hitInfo = Physics2D.Raycast(rbody.position + (direction * 0.5f), direction, 0.05f);

    if (hitInfo && hitInfo.collider)
    {


      if (hitInfo.collider.tag == "Player")
      {
        hitInfo.collider.GetComponent<HeroController>().Kill();
      }

      if (hitInfo.collider.tag != "Enemy")
      {
        movementDirection = -movementDirection;
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (isAlive)
    {
      if (Vector2.Distance(hero.transform.position, rbody.position) > activeDistance)
      {
        rbody.velocity = Vector2.zero;
      }
      else
      {
        Vector2 velocity = rbody.velocity;
        CheckCollision();
        velocity.x = movementDirection * movementSpeed;
        rbody.velocity = velocity;
      }

    }
    else
    {
      if (Vector2.Distance(hero.transform.position, startPosition) > activeDistance)
      {
        Revive();
      }
    }
  }

  public void Kill()
  {
    if (isAlive)
    {
      rbody.velocity = Vector2.zero;
      isAlive = false;
      spriteRenderer.enabled = false;
      boxCollider.enabled = false;
    }
  }


  public void Revive()
  {
    isAlive = true;
    rbody.velocity = Vector2.zero;
    spriteRenderer.enabled = true;
    boxCollider.enabled = true;
    rbody.position = startPosition;
    movementDirection = startDirection;
  }

}
