using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimingScript : MonoBehaviour
{
    Camera mainCamera;

    private void Start()
    {
        if (!mainCamera)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        // Optional: If you want to limit the rotation to a specific range
        // float clampedAngle = Mathf.Clamp(angle, -45f, 45f);
        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, clampedAngle));
    }
}
