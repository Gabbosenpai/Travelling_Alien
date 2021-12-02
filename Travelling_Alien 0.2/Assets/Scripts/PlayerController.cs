using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float jumpVelocity = 20;
    public float groundedHeight = 0;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.22f;
    public float holdJumpTimer = 0.0f;

    public float jumpGroundThreshold = 3;

    public int maxEuphory = 100;
    public int minEuphory = 0;
    public int currentEuphory;
    public EuphoryBarComponent euphoryBar;

    private float time = 0.0f;
    public float interpolationPeriod = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        currentEuphory = maxEuphory;
        euphoryBar.SetMaxEuphory(maxEuphory);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime / 2;
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundedHeight);

        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }

        if (currentEuphory <= minEuphory) {
            Destroy(this.gameObject);
        }


        if (time >= interpolationPeriod)
        {
            time = 0.0f;
            Decrease(1);
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

            pos.y += velocity.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }
            if(pos.y <= groundedHeight)
            {
                pos.y = groundedHeight;
                isGrounded = true;
            }
        }
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Vitamin"))
        {
            if (currentEuphory < 50)
            {
                currentEuphory = 50;
            }
            else {
                currentEuphory = 100;
            }
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Improve(5);
        }

        if (other.gameObject.CompareTag("Danger"))
        {
            if (currentEuphory > 50)
            {
                currentEuphory = 50;
            }
            else
            {
                currentEuphory = 5;
            }
        }
    }

    private void Improve(int incremento)
    {
        currentEuphory += incremento;
        euphoryBar.SetEuphory(currentEuphory);
    }

    private void Decrease(int decremento)
    {
        currentEuphory -= decremento;
        euphoryBar.SetEuphory(currentEuphory);
    }
}
