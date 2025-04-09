using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D PlayerRB;

    public SpriteRenderer PlayerSR;

    public float Speed;

    public float JumpStrength;

    public GameObject RespawnPoint;

    private bool canJump;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey(KeyCode.D)) 
        {
            MoveSideways(Vector2.right);
            PlayerSR.flipX = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MoveSideways(Vector2.left);
            PlayerSR.flipX = false;
        }
        else 
        {
            MoveSideways(Vector2.zero);
        }

        if (Input.GetKeyDown(KeyCode.W) && canJump) 
        {
            PlayerRB.linearVelocity = new Vector2(PlayerRB.linearVelocity.x, JumpStrength);
            canJump = false;
        }
    }

    private void MoveSideways(Vector2 direction) 
    {
        PlayerRB.linearVelocity = new Vector2(direction.x * Speed, PlayerRB.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "ground")
        {
            canJump = true;
        }
        else if (collision.collider.gameObject.tag == "danger")
        {
            transform.position = RespawnPoint.transform.position;
            PlayerRB.linearVelocity = Vector2.zero;
        }
    }
}
