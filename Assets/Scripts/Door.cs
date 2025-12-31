using UnityEngine;

public class Door : MonoBehaviour
{
    
    public Transform objetoPuerta;
    public Transform puntoA; 
    public Transform puntoB; 

   
    public float velocidad = 2f;

    private bool estaSiendoMirado = false;

    void Update()
    {
        
        if (objetoPuerta == null || puntoA == null || puntoB == null) return;

        
        Vector3 objetivo = estaSiendoMirado ? puntoB.position : puntoA.position;

       
        objetoPuerta.position = Vector3.MoveTowards(objetoPuerta.position, objetivo, velocidad * Time.deltaTime);

        
        estaSiendoMirado = false;
    }

    public void OnGaze()
    {
        estaSiendoMirado = true;
    }
}