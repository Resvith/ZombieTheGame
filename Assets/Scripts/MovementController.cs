using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody rb;
    public float targetMovingSpeed = 5f;
    public float targetJumpForce = 250f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        if(Mathf.Abs(rb.velocity.y) <= 0.05f)
        {
            rb.AddForce(Vector3.up * targetJumpForce);
        }
    }

    public void Move(Vector2 axises)
    {
        Vector2 targetVelocity = new Vector2(axises[0] * targetMovingSpeed, axises[1] * targetMovingSpeed);
        rb.velocity = transform.rotation * new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.y);
    }

}
