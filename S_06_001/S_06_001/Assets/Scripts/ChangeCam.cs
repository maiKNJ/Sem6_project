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
    public GameObject up;
    public GameObject down;

    private float startTime;
    private float length;
    float speed = 0.01f;
    Vector3 velocity = Vector3.forward;

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
        length = Vector3.Distance(cam3.transform.position, cam2.transform.position);
        down.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = 0.2f;
        
        float fractionOfJourney = distCovered / length;

        if (col)
        {
            mainLight.SetActive(false);
            light2.SetActive(true);
            Debug.Log("number time: " + fractionOfJourney);
           // Vector3 newPos = Vector3.Slerp(mainCam.transform.position, cam2.transform.position, 0.005f);
            mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position, cam2.transform.position, ref velocity, 0.2f, 20f );
            //  down.SetActive(true);
            // up.SetActive(false);
            StartCoroutine(change());


        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "body")
        {
            col = true;



          //  Debug.Log("collision");

            //mainCam.enabled = !mainCam.enabled;
            //cam3.enabled = !cam3.enabled;

            //cam2.enabled = !cam2.enabled;
           // Debug.Log("Change cam" + mainCam.enabled);


        }
    }

   IEnumerator change()
    {
        
        yield return new WaitForSeconds(2);
        up.SetActive(false);
        down.SetActive(true);
        col = false;
      //  Debug.Log("See i did wait!");

    }
    

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "body")
        {
            col = false;
           // Debug.Log("col" + col);


        }
    }
    

}
