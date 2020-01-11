using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float moveSpeed;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;

    private Vector2 movement;
    private Vector2 mousePos;
    private Collider2D heldObj;

    private bool grabbing;
    private bool holding;

    // Start is called before the first frame update
    void Start()
    {
        grabbing = false;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetAxisRaw("Fire1") == 1)
            grabbing = true;
        else
            grabbing = false;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


        if(holding && grabbing)
        {
            drop();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90f;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && grabbing)
        {
            grab(collision);
            grabbing = false;
            holding = true;
        }
    }

    private void grab(Collider2D collision)
    {
        heldObj = collision;
        heldObj.transform.parent = rb.transform;
    }
    private void drop()
    {
        heldObj.transform.parent = null;
    }
}
