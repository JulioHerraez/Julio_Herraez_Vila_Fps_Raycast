using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private PlayerInput input;
    private Rigidbody rb;
    private Vector2 inputMovement;
    private Vector2 inputLook;
    private Vector3 direction;

    
    [SerializeField] private float speed = 15.0f; 
    [SerializeField] private float jumpForce = 7.0f;
    public LayerMask groundLayer;
    private bool isGrounded;

    
    [SerializeField] private Transform cameraTransform; 
    [SerializeField] private float sensitivity = 0.15f;
    private float xRotation = 0f;

    
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private LayerMask interactableLayer; 

    
    [SerializeField] private Transform startPositionReference;
    private static bool isFirstLoad = true;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ManejarPersistenciaPosicion();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        
        inputMovement = input.actions["Move"].ReadValue<Vector2>();
        inputLook = input.actions["Look"].ReadValue<Vector2>();

        
        ManejarRotacion();

        
        direction = transform.right * inputMovement.x + transform.forward * inputMovement.y;

        
        CheckGround();

        
        LanzarRaycastPuzzle();
    }

    void FixedUpdate()
    {
        
        if (isGrounded)
        {
            rb.AddForce(direction * speed, ForceMode.Force);
        }
    }

    void ManejarRotacion()
    {
        if (cameraTransform == null) return;

        float mouseX = inputLook.x * sensitivity;
        float mouseY = inputLook.y * sensitivity;

        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        
        transform.Rotate(Vector3.up * mouseX);
    }

    void LanzarRaycastPuzzle()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * rayDistance, Color.cyan);

        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            
            hit.collider.SendMessage("OnGaze", SendMessageOptions.DontRequireReceiver);
        }
    }

    void CheckGround()
    {
        
        isGrounded = Physics.SphereCast(
            transform.position + Vector3.up * 0.1f,
            0.5f,
            Vector3.down,
            out _,
            0.2f,
            groundLayer);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void ManejarPersistenciaPosicion()
    {
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
        else if (startPositionReference != null)
        {
            transform.position = startPositionReference.position;
        }
    }
}