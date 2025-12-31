using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
   
    [SerializeField] private float destroyDelay = 5f;

    
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material warningMaterial;

   
    [SerializeField] private float blinkSpeed = 0.4f;

    private bool activated = false;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        if (normalMaterial != null)
            rend.material = normalMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !activated)
        {
            activated = true;
            StartCoroutine(BlinkAndDestroy());
        }
    }

    IEnumerator BlinkAndDestroy()
    {
        float timer = 0f;
        bool showWarning = false;

        while (timer < destroyDelay)
        {
            rend.material = showWarning ? warningMaterial : normalMaterial;
            showWarning = !showWarning;

            timer += blinkSpeed;
            yield return new WaitForSeconds(blinkSpeed);
        }

        Destroy(gameObject);
    }
}