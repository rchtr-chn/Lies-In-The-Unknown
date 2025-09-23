using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatformScript : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTimeAtPoints = 1f;
    Vector3 targetPosition;
    bool movingToB = true;
    Coroutine waitCoroutine;

    private void Update()
    {
        if(movingToB)
        {
            targetPosition = pointB.position;
        }
        else
        {
            targetPosition = pointA.position;
        }
        MoveToTarget();
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if(transform.position == targetPosition && waitCoroutine == null)
        {
            waitCoroutine = StartCoroutine(WaitAtPoint());
        }
    }

    IEnumerator WaitAtPoint()
    {
        yield return new WaitForSeconds(waitTimeAtPoints);
        movingToB = !movingToB; // Toggle direction
        waitCoroutine = null; // Reset coroutine reference
    }
}
