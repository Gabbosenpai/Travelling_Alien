using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitamin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Destroyer" || other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
