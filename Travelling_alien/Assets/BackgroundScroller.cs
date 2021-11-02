using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public BoxCollider2D collider;

    public Rigidbody2D rBody;
    private float width;
    private float scrollSpeed = -15f;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rBody = GetComponent<Rigidbody2D>();

        width = collider.size.x;
        collider.enabled = false;

        rBody.velocity = new Vector2(scrollSpeed, 0);
        resetObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -width)
        {
            Vector2 resetPosition = new Vector2(width * 2f, 0);
            transform.position = (Vector2)transform.position + resetPosition;
            resetObstacle();
        }
    }
    void resetObstacle()
    {
        transform.GetChild(0).localPosition = new Vector3(Random.Range(-1,1), Random.Range(-2.5f, 3.0f), 0);   
    }
}
