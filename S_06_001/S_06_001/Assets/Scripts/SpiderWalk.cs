using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWalk : MonoBehaviour
{
    private float movementDuration = 10.0f;
    private float waitBeforeMoving = 2.0f;
    private bool hasArrived = false;

    private Animation anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    private void Update()
    {
        if (!hasArrived)
        {
            hasArrived = true;
            float randX = Random.Range(-35.0f, 50.0f);
            float randZ = Random.Range(15.0f, 59.0f);
            anim.Play("Walk");
            StartCoroutine(MoveToPoint(new Vector3(randX, 0.2f, randZ)));
        }
    }

    private IEnumerator MoveToPoint(Vector3 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = timer / movementDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        yield return new WaitForSeconds(waitBeforeMoving);
        hasArrived = false;
    }
}
