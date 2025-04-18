using System.Collections;
using Unity.Cinemachine;
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
    public CinemachinePositionComposer cameraPos;



    private Animator anim;
    public float fallThreshold;
    private bool isFalling = false;
    private bool resetJumpNeeded = false;
    private bool canJump = true;
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

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && IsGrounded() == true && canJump) {
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
        UnityEngine.Vector3 offsetPositionRight = transform.position + new UnityEngine.Vector3(0.34f, 0f, 0f);
        RaycastHit2D hitinfoL = Physics2D.Raycast(offsetPositionLeft , UnityEngine.Vector2.down, 0.65f, 1 << 3);
        RaycastHit2D hitinfoR = Physics2D.Raycast(offsetPositionRight, UnityEngine.Vector2.down, 0.65f, 1 << 3);
        Debug.DrawRay(transform.position, UnityEngine.Vector2.down, Color.green);
        Debug.DrawRay(offsetPositionLeft, UnityEngine.Vector2.down * 0.65f, Color.red);
        Debug.DrawRay(offsetPositionRight, UnityEngine.Vector2.down * 0.65f, Color.blue);
        if(hitinfoL.collider != null)
        {
            // Debug.Log("Hit: " + hitinfoL.collider.name);
            anim.SetBool("IsGrounded", true);
            // Debug.Log("Grounded Left");
            if (resetJumpNeeded == false) 
            return true;
        }
        else if(hitinfoR.collider != null)
        {
            // Debug.Log("Hit: " + hitinfoR.collider.name);
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
    IEnumerator ResetJump(float delay)
    {
        resetJumpNeeded = true;
        yield return new WaitForSeconds(delay);
        resetJumpNeeded = false; 
    }


    private void MoveSideways(UnityEngine.Vector2 direction) {
        PlayerRB.linearVelocity = new UnityEngine.Vector2(direction.x * Speed, PlayerRB.linearVelocity.y);
    }

    //Processes the Players falling distance and Splat Animation
    private IEnumerator OnCollisionEnter2D(Collision2D collision) {
        GameObject collider = collision.collider.gameObject;
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (collider.CompareTag("danger") || collider.CompareTag("interactableDanger"))
        {
            anim.SetTrigger("hasFallen");
            StartCoroutine(DisableControlForSeconds(0.8f, true, true));
            yield return new WaitForSeconds(0.2f);

            cameraPos.CameraDistance += 15;
            PlayerSR.enabled = false;
            yield return new WaitForSeconds(0.2f);
            transform.position = RespawnPoint.transform.position;
            yield return new WaitForSeconds(0.2f);
            PlayerSR.enabled = true;
            cameraPos.CameraDistance -= 15;
            yield break;
        }

        if (isFalling && IsGrounded()) {
            float fallDistance = maxHeightBeforeFall - transform.position.y;

            isFalling = false;
            if (fallDistance > fallThreshold) {
                Debug.Log("Player fell! Fall distance: " + fallDistance);
                
                anim.SetTrigger("hasFallen");
                StartCoroutine(DisableControlForSeconds(0.8f, true, true));
            }
        }

        if (collider.CompareTag("interactable"))
        {
            canJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("respawnZone"))
        {
            RespawnPoint = collider.gameObject;
        }

        if (collider.CompareTag("interactable") && !IsGrounded())
        {
            canJump = false;
        }
    }

    private IEnumerator DisableControlForSeconds(float delay, bool control, bool stopVelocity) {
        if (control) {
            canControl = false;
        }

        if (stopVelocity) {
            PlayerRB.linearVelocity = new UnityEngine.Vector2(0, PlayerRB.linearVelocity.y);
        }
        canJump = false;
        yield return new WaitForSeconds(delay); 
        canJump = true; 

        if (control) {
            canControl = true;
        }
    }
}
