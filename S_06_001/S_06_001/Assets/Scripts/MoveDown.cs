using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
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
    float speed = 0.03f;

    private bool col;
    // Start is called before the first frame update
    void Start()
    {
        mainCam.enabled = true;
        cam2.enabled = false;
        cam3.enabled = false;
     
        startTime = Time.time;
        length = Vector3.Distance(cam2.transform.position, cam3.transform.position);
        //light2.SetActive(true);
        //mainLight.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / length;

        if (col)
        {
           light2.SetActive(false);
           mainLight.SetActive(true);
            Vector3 newPos = Vector3.Lerp(mainCam.transform.position, cam3.transform.position,  fractionOfJourney);
            mainCam.transform.position = newPos;

            Debug.Log("something happend!");
            StartCoroutine(change());
        }
    }
    IEnumerator change()
    {

        yield return new WaitForSeconds(6);
        down.SetActive(false);
        up.SetActive(true);
        col = false;
        Debug.Log("See i did wait! part 2");

    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.tag == "body2")
            
        
        {
            col = true;



            Debug.Log("collision2");

            //mainCam.enabled = !mainCam.enabled;
            //cam3.enabled = !cam3.enabled;

            //cam2.enabled = !cam2.enabled;
            //Debug.Log("Change cam" + mainCam.enabled);


        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "body2")
        {
            col = false;
            Debug.Log("col2" + col);
        }
    }
}
