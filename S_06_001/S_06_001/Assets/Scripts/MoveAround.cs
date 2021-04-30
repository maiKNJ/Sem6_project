using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    public Vector3 forrest;
    public Vector3 water;
    public Camera mainCam;
    public GameObject mainLight;
    public GameObject light2;


    public float duration = 5;

    bool col = false;
    void Start()
    {
        mainLight.SetActive(true);
        light2.SetActive(false);
    }
    private void Update()
    {
        if (col && mainCam.transform.position == forrest )
        {
            Debug.Log("changing now");
            StartCoroutine(LerpPositionWater(water, duration));
        }

        if (col && mainCam.transform.position == water)
        {
            StartCoroutine(LerpPositionForrest(forrest, duration));
        }

    }


    IEnumerator LerpPositionWater(Vector3 water, float duration)
    {
        float time = 0;

        Vector3 forrest = mainCam.transform.position;

        while (time < duration)
        {
            mainCam.transform.position = Vector3.Lerp(forrest, water, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        mainCam.transform.position = water;
        col = false;
        mainLight.SetActive(false);
        light2.SetActive(true);



    }


    IEnumerator LerpPositionForrest(Vector3 forrest, float duration)
    {
        float time = 0;

        Vector3 water = mainCam.transform.position;

        while (time < duration)
        {
            mainCam.transform.position = Vector3.Lerp(water, forrest, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        mainCam.transform.position = forrest;
        col = false;
        light2.SetActive(false);
        mainLight.SetActive(true);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "body")
        {
            col= true;
            Debug.Log("colliding");
        }
    }
}
