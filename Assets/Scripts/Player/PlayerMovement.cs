using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Rigidbody2D PlayerRB;

    public SpriteRenderer PlayerSR;

    public float Speed;

    public float JumpStrength;

    public GameObject RespawnPoint;

    private bool canJump;

    private Animator anim;


    // Start is called before the first frame update
    void Start() {
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.D)) {
            MoveSideways(Vector2.right);
            PlayerSR.flipX = true;
            anim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.A)) {
            MoveSideways(Vector2.left);
            PlayerSR.flipX = false;
            anim.SetBool("isWalking", true);
        }
        else {
            MoveSideways(Vector2.zero);
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.W) && canJump) {
            PlayerRB.linearVelocity = new Vector2(PlayerRB.linearVelocity.x, JumpStrength);
            canJump = false;
            anim.SetBool("isJumping", true);
        } 
    }

    private void MoveSideways(Vector2 direction) {
        PlayerRB.linearVelocity = new Vector2(direction.x * Speed, PlayerRB.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameObject collider = collision.collider.gameObject;

        if (collider.CompareTag("ground") || collider.CompareTag("interactable")) {
            canJump = true;
            anim.SetBool("isJumping", false);
        }
    }
}
