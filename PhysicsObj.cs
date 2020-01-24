using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObj : MonoBehaviour
{
    [SerializeField] private float baseGravity;
    [SerializeField] private float raycastBuffer;

    private Rigidbody2D rb;
    private Vector2 velocity;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        calculateVelocityY(1);
        rb.MovePosition(rb.position + velocity);
        Debug.Log(velocity);
    }

    void FixedUpdate()
    {
        
    }

    private void calculateVelocityY(float gravModifier)
    {
        Vector2 gravity = new Vector2();


        float raycastDownDistance = velocity.magnitude + raycastBuffer;
        RaycastHit2D groundHit = Physics2D.Raycast(rb.position, Vector2.down, raycastDownDistance);

        if (groundHit.collider != null)
        {
            if(groundHit.distance)
            velocity.y = groundHit.distance;
        }
        else
        {
            velocity.y -= baseGravity * gravModifier;
        }
    }
}
