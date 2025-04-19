using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour {
    public float moveSpeed;
    public float patrolDist;
    public SpriteRenderer enemySR;
    public Rigidbody2D enemyRB;
    public float idleTime;

    private Vector3 startingPosition;
    private int direction = 1;

    private float waitTimer = 0f;
    private bool isWaiting = false;

    protected Animator anim;

    void Start()
    {
        startingPosition = transform.position;
        anim = GetComponent<Animator>();
    }


        void FixedUpdate()
        {
            if (isWaiting)
            {
                waitTimer += Time.fixedDeltaTime;
                if (waitTimer >= idleTime)
                {
                    isWaiting = false;
                    waitTimer = 0f;
                    direction *= -1;
                    enemySR.flipX = direction < 0;
                    anim.SetBool("isWaiting", false);
                }
                return;
            }

            Vector2 newPos = enemyRB.position + Vector2.right * direction * moveSpeed * Time.fixedDeltaTime;
            enemyRB.MovePosition(newPos);

            if (Mathf.Abs(newPos.x - startingPosition.x) >= patrolDist)
            {
                isWaiting = true;
                anim.SetBool("isWaiting", true);
            }
        }

}