using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Mono.Cecil;
using NUnit.Framework;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Rigidbody2D PlayerRB;

    public SpriteRenderer PlayerSR;

    public float Speed;

    public float JumpStrength;

    public GameObject RespawnPoint;



    private Animator anim;
    public float fallThreshold;
    private bool isFalling = false;
    private bool resetJumpNeeded = false;
    private float maxHeightBeforeFall;
    private bool canControl = true;


    // Start is called before the first frame update
    void Start() {
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (!canControl) return;

        if (Input.GetKey(KeyCode.D)) {
            MoveSideways(UnityEngine.Vector2.right);
            PlayerSR.flipX = true;
            anim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.A)) {
            MoveSideways(UnityEngine.Vector2.left);
            PlayerSR.flipX = false;
            anim.SetBool("isWalking", true);
        }
        else {
            MoveSideways(UnityEngine.Vector2.zero);
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded() == true) {
            PlayerRB.linearVelocity = new UnityEngine.Vector2(PlayerRB.linearVelocity.x, JumpStrength);
            anim.SetBool("IsGrounded", false);

        } 

        if (IsGrounded() == true)
        {
            anim.SetBool("IsGrounded", true);
        }

        if (PlayerRB.linearVelocity.y < -0.1f) {
            if (!isFalling) {
                isFalling = true;
                maxHeightBeforeFall = transform.position.y;
            }
        }
    }

    //Determines if the Cat is on a Ground Tile or not
    private bool IsGrounded()
    {
        UnityEngine.Vector3 offsetPositionLeft = transform.position + new UnityEngine.Vector3(-0.29f, 0f, 0f);
        UnityEngine.Vector3 offsetPositionRight = transform.position + new UnityEngine.Vector3(0.29f, 0f, 0f);
        RaycastHit2D hitinfoL = Physics2D.Raycast(offsetPositionLeft , UnityEngine.Vector2.down, 0.65f, 1 << 3);
        RaycastHit2D hitinfoR = Physics2D.Raycast(offsetPositionRight, UnityEngine.Vector2.down, 0.65f, 1 << 3);
        Debug.DrawRay(transform.position, UnityEngine.Vector2.down, Color.green);
        if(hitinfoL.collider != null)
        {
            Debug.Log("Hit: " + hitinfoL.collider.name);
            anim.SetBool("IsGrounded", true);
            // Debug.Log("Grounded Left");
            if (resetJumpNeeded == false)
            return true;
        }
        else if(hitinfoR.collider != null)
        {
            Debug.Log("Hit: " + hitinfoR.collider.name);
            anim.SetBool("IsGrounded", true);
            // Debug.Log("Grounded Right");
            if (resetJumpNeeded == false)
            return true;
        }
        anim.SetBool("IsGrounded", false);
        // Debug.Log("In the Air");
        return false;
    }

    //Prevents Ray length from being abused by Double Jumps
    IEnumerator ResetJump()
    {
        resetJumpNeeded = true;
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false; 
    }


    private void MoveSideways(UnityEngine.Vector2 direction) {
        PlayerRB.linearVelocity = new UnityEngine.Vector2(direction.x * Speed, PlayerRB.linearVelocity.y);
    }

    //Processes the Players falling distance and Splat Animation
    void OnCollisionEnter2D(Collision2D collision) {
        GameObject collider = collision.collider.gameObject;
        if (isFalling) {
            float fallDistance = maxHeightBeforeFall - transform.position.y;
            if (fallDistance > fallThreshold) {
                Debug.Log("Player fell! Fall distance: " + fallDistance);
                anim.SetTrigger("hasFallen");
                StartCoroutine(DisableControlForSeconds(0.8f, true, true));
            }
            else {
                anim.SetTrigger("hasLanded");
                if (PlayerRB.linearVelocity.x == 0) {
                    StartCoroutine(DisableControlForSeconds(0.2f, false, false));
                }
            }
            isFalling = false;
        }
    }

    private IEnumerator DisableControlForSeconds(float delay, bool control, bool stopVelocity) {
        if (control) {
            canControl = false;
        }

        if (stopVelocity) {
            PlayerRB.linearVelocity = new UnityEngine.Vector2(0, PlayerRB.linearVelocity.y);
        }
        yield return new WaitForSeconds(delay); 

        if (control) {
            canControl = true;
        }
    }
}
