using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleAnim : MonoBehaviour
{
    bool anim;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "waterSide")
        {
            anim = true;
            animator.SetBool("ripple", anim);
            anim = false;
            animator.SetBool("ripple", anim);
        }
        
    }
}
