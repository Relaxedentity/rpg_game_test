using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritProjectile : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;

    void Start()
    {
        
    }

    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))

        {
            Destroy(this.gameObject);
        }
    }

}
