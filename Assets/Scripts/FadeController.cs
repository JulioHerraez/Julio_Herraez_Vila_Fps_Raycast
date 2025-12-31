using UnityEngine;
using System.Collections;

public class FadeController : MonoBehaviour
{
    
    public GameObject animatorContainerObject; 

   
    public float fadeDuration = 1.0f;

    private Animator animator;

    void Awake()
    {
        
        if (animatorContainerObject != null)
        {
            animator = animatorContainerObject.GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("FadeController: No se encontró el Animator en el objeto enlazado. Revisa la asignación.");
        }
    }

    public float StartFadeIn()
    {
        
        if (animatorContainerObject != null)
        {
            animatorContainerObject.SetActive(true); 
        }

       
        if (animator != null)
        {
            animator.Play("FadeIn"); 
        }

        
        return fadeDuration;
    }
}