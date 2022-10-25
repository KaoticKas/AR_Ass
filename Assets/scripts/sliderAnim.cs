using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderAnim : MonoBehaviour
{

    public GameObject menuPanel;

    public void menuState()
    {
        if(menuPanel !=null)
        {
            Animator animator = menuPanel.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
