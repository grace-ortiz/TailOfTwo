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
    public float fallThreshold;
    private bool isFalling = false;
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

        if (PlayerRB.linearVelocity.y < -0.1f) {
            if (!isFalling) {
                isFalling = true;
                maxHeightBeforeFall = transform.position.y;
            }
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

        if (isFalling) {
            float fallDistance = maxHeightBeforeFall - transform.position.y;
            if (fallDistance > fallThreshold) {
                Debug.Log("Player fell! Fall distance: " + fallDistance);
                anim.SetTrigger("hasFallen");
                StartCoroutine(DisableControlForSeconds(0.8f, true, true));
            }
            else {
                anim.SetTrigger("hasLanded");
                StartCoroutine(DisableControlForSeconds(0.2f, false, false));
            }
            isFalling = false;
        }
    }

    private IEnumerator DisableControlForSeconds(float delay, bool control, bool stopVelocity) {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (control) {
            canControl = false;
        } else if (!stateInfo.IsName("isWalking")) {
            canJump = false;
        }

        if (stopVelocity) {
            PlayerRB.linearVelocity = new Vector2(0, PlayerRB.linearVelocity.y);
        }
        yield return new WaitForSeconds(delay); 

        if (control) {
            canControl = true;
        } else {
            canJump = true;
        }
    }
}
