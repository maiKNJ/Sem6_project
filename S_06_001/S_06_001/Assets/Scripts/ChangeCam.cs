using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public Camera mainCam;
    public Camera cam2;
    public Camera cam3;
    public GameObject mainLight;
    public GameObject light2;

    private float startTime;
    private float length;
    float speed = 0.03f;

    private bool col;
    // Start is called before the first frame update
    void Start()
    {
        mainCam.enabled = true;
        cam2.enabled = false;
        cam3.enabled = false;
        mainLight.SetActive(true);
        light2.SetActive(false);
        startTime = Time.time;
        length = Vector3.Distance(mainCam.transform.position, cam2.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / length;

        if (col)
        {
            mainLight.SetActive(false);
            light2.SetActive(true);
            Vector3 newPos = Vector3.Lerp(mainCam.transform.position, cam2.transform.position, fractionOfJourney);
            mainCam.transform.position = newPos;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "body")
        {
            col = true;
            
            
          
            Debug.Log("collision");
            
            //mainCam.enabled = !mainCam.enabled;
            //cam3.enabled = !cam3.enabled;
            
            //cam2.enabled = !cam2.enabled;
            Debug.Log("Change cam" + mainCam.enabled);


        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "body")
        {
            col = false;
        }
    }
}
