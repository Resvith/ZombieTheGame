using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float targetMovingSpeed = 75f;
    public float targetJumpForce = 250f;
    public bool isOnLadder = false;
    public bool isGrounded = true;
    public Transform characterBottom;

    [SerializeField] private float _climbingSpeed = 90f;
    [SerializeField] private float _groundCheckDistance = 0.1f;

    private Rigidbody _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GroundChecker();
    }

    private void GroundChecker()
    {
        RaycastHit hit;
        if (Physics.Raycast(characterBottom.position, Vector3.down, out hit, _groundCheckDistance))
            isGrounded = true;
        else
            isGrounded = false;
    }

    public void Jump()
    {
        if(Mathf.Abs(_rb.velocity.y) <= 0.05f)
            _rb.AddForce(Vector3.up * targetJumpForce);
    }

    public void Move(Vector2 axises)
    {
        if (!isOnLadder)
        {
            _rb.useGravity = true;
            Vector2 targetVelocity = new Vector2(axises[0] * targetMovingSpeed * Time.deltaTime, axises[1] * targetMovingSpeed * Time.deltaTime);
            _rb.velocity = transform.rotation * new Vector3(targetVelocity.x, _rb.velocity.y, targetVelocity.y);
        }
    }

    public void ClimbUp()
    {
        _rb.useGravity = false;
        _rb.velocity = Vector3.up * _climbingSpeed * Time.deltaTime;
    }

    public void ClimbDown()
    {
        _rb.useGravity = false;
        _rb.velocity = Vector3.down * _climbingSpeed * Time.deltaTime;
    }

    public void ClimbStop()
    {
        isOnLadder = false;
        _rb.useGravity = true;
    }

    public void ClimbWait()
    {
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
    }

}
