using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody rb;
    public float targetMovingSpeed = 75f;
    public float targetJumpForce = 250f;
    public bool isOnLadder = false;
    public bool isGrounded = true;
    public Transform characterBottom;

    [SerializeField] private float climbingSpeed = 90f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        GroundChecker();
    }

    private void GroundChecker()
    {
        RaycastHit hit;
        if (Physics.Raycast(characterBottom.position, Vector3.down, out hit, groundCheckDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
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
        if (!isOnLadder)
        {
            rb.useGravity = true;
            Vector2 targetVelocity = new Vector2(axises[0] * targetMovingSpeed * Time.deltaTime, axises[1] * targetMovingSpeed * Time.deltaTime);
            rb.velocity = transform.rotation * new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.y);
        }
    }

    public void ClimbUp()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.up * climbingSpeed * Time.deltaTime;
    }

    public void ClimbDown()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.down * climbingSpeed * Time.deltaTime;
    }

    public void ClimbStop()
    {
        isOnLadder = false;
        rb.useGravity = true;
    }

    public void ClimbWait()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }

}
