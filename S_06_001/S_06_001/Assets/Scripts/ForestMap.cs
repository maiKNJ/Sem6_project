using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMap : MonoBehaviour
{
    public GameObject waterCam;
    public GameObject forestCam;
    public GameObject mainCam;
    public GameObject waterMap;
    public GameObject forestMap;
    public GameObject waterMap1;
    public GameObject forestMap1;
    public GameObject forestLight;
    public GameObject waterLight;

    bool timer;
    int time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            time++;
            if (time >= 500)
            {
                waterCam.SetActive(false);
                forestCam.SetActive(true);
                mainCam.SetActive(false);
                waterMap.SetActive(false);
                forestMap.SetActive(false);
                waterMap1.SetActive(false);
                forestMap1.SetActive(false);
                forestLight.SetActive(true);
                waterLight.SetActive(false);
                Reset();
            }
            Debug.Log(time + "forest");
        }
        //Debug.Log(timer);
    }



    private void Reset()
    {
        time = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "body" || collision.gameObject.tag == "body2")
        {
            timer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "body" || collision.gameObject.tag == "body2")
        {
            timer = false;
            Debug.Log("forest exit");
            Reset();
        }
    }
}
