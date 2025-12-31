using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    private PlayerInput input;
    private Rigidbody rb;
    private Vector2 inputMovement;
    private Vector3 direction;

    bool isGrounded;

    
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce = 7.0f;

    
    private static bool isFirstLoad = true;

    
    [SerializeField] private Transform startPositionReference;

    public LayerMask groundLayer;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        
        if (isFirstLoad)
        {
            
            PlayerPrefs.DeleteKey("RespawnX");
            PlayerPrefs.DeleteKey("RespawnY");
            PlayerPrefs.DeleteKey("RespawnZ");
            PlayerPrefs.Save();

            isFirstLoad = false;
        }

        
        if (PlayerPrefs.HasKey("RespawnX"))
        {
            
            transform.position = new Vector3(
                PlayerPrefs.GetFloat("RespawnX"),
                PlayerPrefs.GetFloat("RespawnY"),
                PlayerPrefs.GetFloat("RespawnZ")
            );
        }
        else
        {
            
            if (startPositionReference != null)
            {
                transform.position = startPositionReference.position;
            }
        }

        
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        inputMovement = input.actions["Move"].ReadValue<Vector2>();
        direction = new Vector3(inputMovement.x, 0, inputMovement.y);

        CheckGround();
    }

    void FixedUpdate()
    {
        
        if (isGrounded)
        {
            rb.AddForce(direction * speed, ForceMode.Force);
        }
    }

    void CheckGround()
    {
        RaycastHit hit;

        
        if (Physics.SphereCast(
            transform.position + Vector3.up * 0.1f,
            0.5f,
            Vector3.down,
            out hit,
            0.2f,
            groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    /*void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * 0.5f, 0.12f);
    }*/
}