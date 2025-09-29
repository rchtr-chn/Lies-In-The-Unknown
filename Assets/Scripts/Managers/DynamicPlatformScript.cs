using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatformScript : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float Speed = 2f;
    public float WaitTimeAtPoints = 1f;
    private Vector3 _targetPosition;
    private bool _movingToB = true;
    private Coroutine _waitCoroutine;

    private void Update()
    {
        if(_movingToB)
        {
            _targetPosition = PointB.position;
        }
        else
        {
            _targetPosition = PointA.position;
        }
        MoveToTarget();
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed * Time.deltaTime);
        if(transform.position == _targetPosition && _waitCoroutine == null)
        {
            _waitCoroutine = StartCoroutine(WaitAtPoint());
        }
    }

    IEnumerator WaitAtPoint()
    {
        yield return new WaitForSeconds(WaitTimeAtPoints);
        _movingToB = !_movingToB; // Toggle direction
        _waitCoroutine = null; // Reset coroutine reference
    }
}
