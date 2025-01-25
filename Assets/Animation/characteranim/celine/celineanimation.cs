using UnityEngine;
using System.Collections.Generic;
using System;
public class celineanimation : MonoBehaviour
{
    [SerializeField] bool last;
    Animator anim;
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

    int isleft()
    {
        last = a;
        if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.A))
        {
            Debug.Log("all");
            a = last;
        }else 
        
        if (Input.GetKey (KeyCode.A))
        {
            Debug.Log("left");
            a = true;
        }
        else if (Input.GetKey(KeyCode.D)) { a = false; Debug.Log("right"); }
        



        return b = a ? 0 : 1;
    }


}