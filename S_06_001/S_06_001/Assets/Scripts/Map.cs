using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject water;
    public GameObject forest;
    public GameObject water1;
    public GameObject forest1;
    // Start is called before the first frame update
    void Start()
    {
        water.SetActive(false);
        forest.SetActive(false);
        water1.SetActive(false);
        forest1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "body" )
        {
            water.SetActive(true);
            forest.SetActive(true);
           
            Debug.Log("Collision detected");
        }
        if(collision.gameObject.tag == "body2")
        {
            water1.SetActive(true);
            forest1.SetActive(true);
            Debug.Log("collsion with body2");
        }
    }
}
