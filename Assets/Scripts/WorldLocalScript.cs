using UnityEngine;
using UnityEngine.InputSystem;

public class WorldLocalScript : MonoBehaviour
{
    
    PlayerInput input;
   
    Vector2 movement;
    
    Vector3 direction;
    Vector3 directionLocalWorld;

    
    [SerializeField] private float speed = 50.0f;
    void Awake()
    {
        
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        movement = input.actions["Move"].ReadValue<Vector2>();

        
        direction = new Vector3(movement.x, 0f, movement.y);

        
        directionLocalWorld = transform.TransformDirection(direction);
        transform.position += directionLocalWorld * speed * Time.deltaTime;
        Debug.Log(movement);
    }
}
