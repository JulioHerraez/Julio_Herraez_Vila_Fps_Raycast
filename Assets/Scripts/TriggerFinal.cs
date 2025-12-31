using UnityEngine;
using System.Collections; 

public class TriggerFinal : MonoBehaviour
{
    
    public GameObject winCanvas; 

   
    public GameObject fadeCanvasObject; 

   
    public Animator fadeAnimator;

    private bool isPlayerInside = false; 

    void Start()
    {
       
        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }
        else
        {
            
        }

        if (fadeCanvasObject == null)
        {
            
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Player") && !isPlayerInside)
        {
            isPlayerInside = true;
           

            
            StartCoroutine(VictorySequence());
        }
    }

        IEnumerator VictorySequence()
        {

            fadeAnimator.gameObject.SetActive(true);
            fadeAnimator.SetTrigger("StartFadeIn");


            yield return new WaitForSeconds(3f);


            winCanvas.SetActive(true);

            Time.timeScale = 0f;
        }
    }