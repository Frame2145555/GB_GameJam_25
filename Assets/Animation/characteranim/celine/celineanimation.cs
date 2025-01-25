using UnityEngine;
using System.Collections.Generic;
using System;
public class celineanimation : MonoBehaviour
{
    [SerializeField] bool last;
    protected Animator anim;
    [SerializeField] bool a = true;
    [SerializeField] int b;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update() { anim.SetFloat("isleft", isleft()); }

    public void runanmation() { anim.SetBool("isrun", true); }
    public void idleanmation() { anim.SetBool("isrun", false); }
    public void jebleaw(){anim.SetFloat("chapter3", 1);}

    int isleft()
    {  float x =  Input.GetAxis("Horizontal");
    
        last = a;
        if (x==0)
        {
             
            a = last;
        }else 
        
        if (x<0)
        {
           
            a = true;
        }
        else if (x>0) { a = false;   }
        



        return b = a ? 0 : 1;
    }


}