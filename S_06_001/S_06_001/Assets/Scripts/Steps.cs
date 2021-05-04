using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    public AudioSource Left;
    public AudioSource right;
    public AudioClip rightClip;
    public AudioClip leftClip;
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
        if (collision.transform.tag == "RightFoot")
        {
            right.PlayOneShot(rightClip);
        }

        if (collision.transform.tag == "LeftFoot")
        {
            Left.PlayOneShot(leftClip);
        }
    }
}
