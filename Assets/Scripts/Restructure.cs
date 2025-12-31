using UnityEngine;

public class Restructure : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool wasActive;

    void Awake()
    {
       
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        wasActive = gameObject.activeSelf;
    }

   
    public void RestaurarEstado()
    {
        
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        
        if (!wasActive)
        {
            
            gameObject.SetActive(true);
        }
    }
}