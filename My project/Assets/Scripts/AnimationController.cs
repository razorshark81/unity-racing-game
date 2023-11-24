using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] CameraController cameraController;
    [SerializeField]AICarController[] aicarController;
    [SerializeField] PrometeoCarController prometeoCarController;
    int i = 0;
    int animationRepeat;


    private void Start()
    {
        animator.Play("CameraBeginning",0,0.0f);
        cameraController.enabled = false;
        animationRepeat = 0;
        prometeoCarController.enabled = false;    
        
        while (i < aicarController.Length)
        {
            aicarController[i].enabled = false;
            i++;
        }
        if (i == aicarController.Length  )
        {
        }
    }
    private void Update()
    {
        
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=0.95f && animationRepeat<=3)
        {

            animator.enabled = false;
            cameraController.enabled=true;
            prometeoCarController.enabled =true;

            
            while (i < aicarController.Length)
            {
                aicarController[i].enabled = true;
                i++;
            }
            if (i == aicarController.Length)
            {
                i = 0;
            }
            animationRepeat++;
        }
    }
}
