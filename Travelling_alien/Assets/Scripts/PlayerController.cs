using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity;
    public Vector2 movSpeed;
    public float jumpVelocity = 20;
    public float groundedHeight = 0;
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.22f;
    public float holdJumpTimer = 0.0f;
    public float jumpGroundThreshold = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundedHeight);
        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
             //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                movSpeed.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (!isGrounded)
        {
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if(holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }


            pos.y += movSpeed.y * Time.fixedDeltaTime;
            if (!isHoldingJump) 
            { 
                movSpeed.y += gravity * Time.fixedDeltaTime; 
            }
            

            if (pos.y <= groundedHeight)
            {
                pos.y = groundedHeight;
                isGrounded = true;
            }
        }

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
